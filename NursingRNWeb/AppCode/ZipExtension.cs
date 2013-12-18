using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace NursingRNWeb.AppCode
{
    public class ZipExtension : IDisposable
    {
        //// Default filename encoder
        private static Encoding DefaultEncoding = Encoding.GetEncoding(437);

        private bool EncodeUTF8 = false;

        private List<ZipFileEntry> Files = new List<ZipFileEntry>();
        //// Filename of storage file
        private string FileName;
        //// Stream object of storage file
        private Stream ZipFileStream;
        //// General comment
        private string Comment = string.Empty;
        //// Central dir image
        private byte[] CentralDirImage = null;
        //// Existing files in zip
        private ushort ExistingFiles = 0;
        //// File access for Open method
        private FileAccess Access;

        public enum Compression : ushort
        {
            /// <summary>Uncompressed storage</summary> 
            Store = 0,

            /// <summary>Deflate compression method</summary>
            Deflate = 8
        }

        #region Public methods

        /// <summary>
        /// Method to open an existing storage file
        /// </summary>
        /// <param name="_filename">Full path of Zip file to open</param>
        /// <param name="_access">File access mode as used in FileStream constructor</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipExtension Open(string _filename, FileAccess _access)
        {
            Stream stream = (Stream)new FileStream(_filename, FileMode.Open, _access == FileAccess.Read ? FileAccess.Read : FileAccess.ReadWrite);

            ZipExtension zip = Open(stream, _access);
            zip.FileName = _filename;
            return zip;
        }

        /// <summary>
        /// Method to open an existing storage from stream
        /// </summary>
        /// <param name="_stream">Already opened stream with zip contents</param>
        /// <param name="_access">File access mode for stream operations</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipExtension Open(Stream _stream, FileAccess _access)
        {
            if (!_stream.CanSeek && _access != FileAccess.Read)
            {
                throw new InvalidOperationException("Stream cannot seek");
            }

            ZipExtension zip = new ZipExtension();
            zip.ZipFileStream = _stream;
            zip.Access = _access;

            if (zip.ReadFileInfo())
            {
                return zip;
            }

            throw new System.IO.InvalidDataException();
        }

        /// <summary>
        /// Updates central directory (if pertinent) and close the Zip storage
        /// </summary>
        /// <remarks>This is a required step, unless automatic dispose is used</remarks>
        public void Close()
        {
            if (Access != FileAccess.Read)
            {
                uint centralOffset = (uint)ZipFileStream.Position;
                uint centralSize = 0;

                if (CentralDirImage != null)
                {
                    ZipFileStream.Write(CentralDirImage, 0, CentralDirImage.Length);
                }

                for (int i = 0; i < Files.Count; i++)
                {
                    long pos = ZipFileStream.Position;
                    WriteCentralDirRecord(Files[i]);
                    centralSize += (uint)(ZipFileStream.Position - pos);
                }

                if (CentralDirImage != null)
                {
                    WriteEndRecord(centralSize + (uint)CentralDirImage.Length, centralOffset);
                }
                else
                {
                    WriteEndRecord(centralSize, centralOffset);
                }
            }

            if (ZipFileStream != null)
            {
                ZipFileStream.Flush();
                ZipFileStream.Dispose();
                ZipFileStream = null;
            }
        }

        /// <summary>
        /// Read all the file records in the central directory 
        /// </summary>
        /// <returns>List of all entries in directory</returns>
        public List<ZipFileEntry> ReadCentralDir()
        {
            if (CentralDirImage == null)
            {
                throw new InvalidOperationException("Central directory currently does not exist");
            }

            List<ZipFileEntry> result = new List<ZipFileEntry>();

            for (int pointer = 0; pointer < CentralDirImage.Length;)
            {
                uint signature = BitConverter.ToUInt32(CentralDirImage, pointer);
                if (signature != 0x02014b50)
                {
                    break;
                }

                bool encodeUTF8 = (BitConverter.ToUInt16(CentralDirImage, pointer + 8) & 0x0800) != 0;
                ushort method = BitConverter.ToUInt16(CentralDirImage, pointer + 10);
                uint modifyTime = BitConverter.ToUInt32(CentralDirImage, pointer + 12);
                uint crc32 = BitConverter.ToUInt32(CentralDirImage, pointer + 16);
                uint comprSize = BitConverter.ToUInt32(CentralDirImage, pointer + 20);
                uint fileSize = BitConverter.ToUInt32(CentralDirImage, pointer + 24);
                ushort filenameSize = BitConverter.ToUInt16(CentralDirImage, pointer + 28);
                ushort extraSize = BitConverter.ToUInt16(CentralDirImage, pointer + 30);
                ushort commentSize = BitConverter.ToUInt16(CentralDirImage, pointer + 32);
                uint headerOffset = BitConverter.ToUInt32(CentralDirImage, pointer + 42);
                uint headerSize = (uint)(46 + filenameSize + extraSize + commentSize);

                Encoding encoder = encodeUTF8 ? Encoding.UTF8 : DefaultEncoding;

                ZipFileEntry zfe = new ZipFileEntry();
                zfe.Method = (Compression)method;
                zfe.FilenameInZip = encoder.GetString(CentralDirImage, pointer + 46, filenameSize);
                zfe.FileOffset = GetFileOffset(headerOffset);
                zfe.FileSize = fileSize;
                zfe.CompressedSize = comprSize;
                zfe.HeaderOffset = headerOffset;
                zfe.HeaderSize = headerSize;
                zfe.Crc32 = crc32;
                zfe.ModifyTime = DosTimeToDateTime(modifyTime);
                if (commentSize > 0)
                {
                    zfe.Comment = encoder.GetString(CentralDirImage, pointer + 46 + filenameSize + extraSize, commentSize);
                }

                if (!IsFolderName(zfe))
                {
                    result.Add(zfe);
                }

                pointer += 46 + filenameSize + extraSize + commentSize;
            }

            return result;
        }

        /// <summary>
        /// Copy the contents of a stored file into a physical file
        /// </summary>
        /// <param name="_zfe">Entry information of file to extract</param>
        /// <param name="_filename">Name of file to store uncompressed data</param>
        /// <returns>True if success, false if not.</returns>
        /// <remarks>Unique compression methods are Store and Deflate</remarks>
        public bool ExtractFile(ZipFileEntry _zfe, string _filename)
        {
            //// Make sure the parent directory exist
            string path = System.IO.Path.GetDirectoryName(_filename);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //// Check it is directory. If so, do nothing
            if (Directory.Exists(_filename))
            {
                return true;
            }

            Stream output = new FileStream(_filename, FileMode.Create, FileAccess.Write);
            bool result = ExtractFile(_zfe, output);
            if (result)
            {
                output.Close();
            }

            File.SetCreationTime(_filename, _zfe.ModifyTime);
            File.SetLastWriteTime(_filename, _zfe.ModifyTime);

            return result;
        }

        /// <summary>
        /// Copy the contents of a stored file into an opened stream
        /// </summary>
        /// <param name="_zfe">Entry information of file to extract</param>
        /// <param name="_stream">Stream to store the uncompressed data</param>
        /// <returns>True if success, false if not.</returns>
        /// <remarks>Unique compression methods are Store and Deflate</remarks>
        public bool ExtractFile(ZipFileEntry _zfe, Stream _stream)
        {
            if (!_stream.CanWrite)
            {
                throw new InvalidOperationException("Stream cannot be written");
            }
            //// check signature
            byte[] signature = new byte[4];
            ZipFileStream.Seek(_zfe.HeaderOffset, SeekOrigin.Begin);
            ZipFileStream.Read(signature, 0, 4);
            if (BitConverter.ToUInt32(signature, 0) != 0x04034b50)
            {
                return false;
            }

            //// Select input stream for inflating or just reading
            Stream inStream;
            if (_zfe.Method == Compression.Store)
            {
                inStream = ZipFileStream;
            }
            else if (_zfe.Method == Compression.Deflate)
            {
                inStream = new DeflateStream(ZipFileStream, CompressionMode.Decompress, true);
            }
            else
            {
                return false;
            }
            //// Buffered copy
            byte[] buffer = new byte[16384];
            ZipFileStream.Seek(_zfe.FileOffset, SeekOrigin.Begin);
            uint bytesPending = _zfe.FileSize;
            while (bytesPending > 0)
            {
                int bytesRead = inStream.Read(buffer, 0, (int)Math.Min(bytesPending, buffer.Length));
                _stream.Write(buffer, 0, bytesRead);
                bytesPending -= (uint)bytesRead;
            }

            _stream.Flush();

            if (_zfe.Method == Compression.Deflate)
            {
                inStream.Dispose();
            }

            return true;
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Closes the Zip file stream
        /// </summary>
        public void Dispose()
        {
            Close();
        }
        #endregion

        #region Private methods

        private bool IsFolderName(ZipFileEntry zfe)
        {
            bool isFolderName = false;
            string[] values = zfe.FilenameInZip.Replace("/", "\\").Split('\\');
            int lastString = values.Length - 1;
            string filename = values[lastString];
            if (string.IsNullOrEmpty(filename))
            {
                isFolderName = true;
            }

            return isFolderName;
        }

        //// Calculate the file offset by reading the corresponding local header
        private uint GetFileOffset(uint _headerOffset)
        {
            byte[] buffer = new byte[2];

            ZipFileStream.Seek(_headerOffset + 26, SeekOrigin.Begin);
            ZipFileStream.Read(buffer, 0, 2);
            ushort filenameSize = BitConverter.ToUInt16(buffer, 0);
            ZipFileStream.Read(buffer, 0, 2);
            ushort extraSize = BitConverter.ToUInt16(buffer, 0);

            return (uint)(30 + filenameSize + extraSize + _headerOffset);
        }

        /* Central directory's File header:
            central file header signature   4 bytes  (0x02014b50)
            version made by                 2 bytes
            version needed to extract       2 bytes
            general purpose bit flag        2 bytes
            compression method              2 bytes
            last mod file time              2 bytes
            last mod file date              2 bytes
            crc-32                          4 bytes
            compressed size                 4 bytes
            uncompressed size               4 bytes
            filename length                 2 bytes
            extra field length              2 bytes
            file comment length             2 bytes
            disk number start               2 bytes
            internal file attributes        2 bytes
            external file attributes        4 bytes
            relative offset of local header 4 bytes

            filename (variable size)
            extra field (variable size)
            file comment (variable size)
        */
        private void WriteCentralDirRecord(ZipFileEntry _zfe)
        {
            Encoding encoder = _zfe.EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding;
            byte[] encodedFilename = encoder.GetBytes(_zfe.FilenameInZip);
            byte[] encodedComment = encoder.GetBytes(_zfe.Comment);

            ZipFileStream.Write(new byte[] { 80, 75, 1, 2, 23, 0xB, 20, 0 }, 0, 8);
            ZipFileStream.Write(BitConverter.GetBytes((ushort)(_zfe.EncodeUTF8 ? 0x0800 : 0)), 0, 2); // filename and comment encoding 
            ZipFileStream.Write(BitConverter.GetBytes((ushort)_zfe.Method), 0, 2);  // zipping method
            ZipFileStream.Write(BitConverter.GetBytes(DateTimeToDosTime(_zfe.ModifyTime)), 0, 4);  // zipping date and time
            ZipFileStream.Write(BitConverter.GetBytes(_zfe.Crc32), 0, 4); // file CRC
            ZipFileStream.Write(BitConverter.GetBytes(_zfe.CompressedSize), 0, 4); // compressed file size
            ZipFileStream.Write(BitConverter.GetBytes(_zfe.FileSize), 0, 4); // uncompressed file size
            ZipFileStream.Write(BitConverter.GetBytes((ushort)encodedFilename.Length), 0, 2); // Filename in zip
            ZipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // extra length
            ZipFileStream.Write(BitConverter.GetBytes((ushort)encodedComment.Length), 0, 2);

            ZipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // disk=0
            ZipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // file type: binary
            ZipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // Internal file attributes
            ZipFileStream.Write(BitConverter.GetBytes((ushort)0x8100), 0, 2); // External file attributes (normal/readable)
            ZipFileStream.Write(BitConverter.GetBytes(_zfe.HeaderOffset), 0, 4);  // Offset of header

            ZipFileStream.Write(encodedFilename, 0, encodedFilename.Length);
            ZipFileStream.Write(encodedComment, 0, encodedComment.Length);
        }

        /* End of central dir record:
            end of central dir signature    4 bytes  (0x06054b50)
            number of this disk             2 bytes
            number of the disk with the
            start of the central directory  2 bytes
            total number of entries in
            the central dir on this disk    2 bytes
            total number of entries in
            the central dir                 2 bytes
            size of the central directory   4 bytes
            offset of start of central
            directory with respect to
            the starting disk number        4 bytes
            zipfile comment length          2 bytes
            zipfile comment (variable size)
        */

        private void WriteEndRecord(uint _size, uint _offset)
        {
            Encoding encoder = EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding;
            byte[] encodedComment = encoder.GetBytes(Comment);

            ZipFileStream.Write(new byte[] { 80, 75, 5, 6, 0, 0, 0, 0 }, 0, 8);
            ZipFileStream.Write(BitConverter.GetBytes((ushort)Files.Count + ExistingFiles), 0, 2);
            ZipFileStream.Write(BitConverter.GetBytes((ushort)Files.Count + ExistingFiles), 0, 2);
            ZipFileStream.Write(BitConverter.GetBytes(_size), 0, 4);
            ZipFileStream.Write(BitConverter.GetBytes(_offset), 0, 4);
            ZipFileStream.Write(BitConverter.GetBytes((ushort)encodedComment.Length), 0, 2);
            ZipFileStream.Write(encodedComment, 0, encodedComment.Length);
        }

        /* DOS Date and time:
            MS-DOS date. The date is a packed value with the following format. Bits Description 
                0-4 Day of the month (1–31) 
                5-8 Month (1 = January, 2 = February, and so on) 
                9-15 Year offset from 1980 (add 1980 to get actual year) 
            MS-DOS time. The time is a packed value with the following format. Bits Description 
                0-4 Second divided by 2 
                5-10 Minute (0–59) 
                11-15 Hour (0–23 on a 24-hour clock) 
        */
        private uint DateTimeToDosTime(DateTime _dt)
        {
            return (uint)(
                (_dt.Second / 2) | (_dt.Minute << 5) | (_dt.Hour << 11) |
                (_dt.Day << 16) | (_dt.Month << 21) | ((_dt.Year - 1980) << 25));
        }

        private DateTime DosTimeToDateTime(uint _dt)
        {
            return new DateTime(
                (int)(_dt >> 25) + 1980,
                (int)(_dt >> 21) & 15,
                (int)(_dt >> 16) & 31,
                (int)(_dt >> 11) & 31,
                (int)(_dt >> 5) & 63,
                (int)(_dt & 31) * 2);
        }

        // Reads the end-of-central-directory record
        private bool ReadFileInfo()
        {
            if (ZipFileStream.Length < 22)
            {
                return false;
            }

            try
            {
                ZipFileStream.Seek(-17, SeekOrigin.End);
                BinaryReader br = new BinaryReader(ZipFileStream);
                do
                {
                    ZipFileStream.Seek(-5, SeekOrigin.Current);
                    UInt32 sig = br.ReadUInt32();
                    if (sig == 0x06054b50)
                    {
                        ZipFileStream.Seek(6, SeekOrigin.Current);

                        UInt16 entries = br.ReadUInt16();
                        Int32 centralSize = br.ReadInt32();
                        UInt32 centralDirOffset = br.ReadUInt32();
                        UInt16 commentSize = br.ReadUInt16();

                        //// check if comment field is the very last data in file
                        if (ZipFileStream.Position + commentSize != ZipFileStream.Length)
                        {
                            return false;
                        }

                        //// Copy entire central directory to a memory buffer
                        ExistingFiles = entries;
                        CentralDirImage = new byte[centralSize];
                        ZipFileStream.Seek(centralDirOffset, SeekOrigin.Begin);
                        ZipFileStream.Read(CentralDirImage, 0, centralSize);

                        //// Leave the pointer at the begining of central dir, to append new files
                        ZipFileStream.Seek(centralDirOffset, SeekOrigin.Begin);
                        return true;
                    }
                }
                while (ZipFileStream.Position > 0);
            }
            catch
            {
                ////Nothing to do 
            }

            return false;
        }
        #endregion

        /// <summary>
        /// Represents an entry in Zip file directory
        /// </summary>
        public struct ZipFileEntry
        {
            /// <summary>Compression method</summary>
            public Compression Method;

            /// <summary>Full path and filename as stored in Zip</summary>
            public string FilenameInZip;

            /// <summary>Original file size</summary>
            public uint FileSize;

            /// <summary>Compressed file size</summary>
            public uint CompressedSize;

            /// <summary>Offset of header information inside Zip storage</summary>
            public uint HeaderOffset;

            /// <summary>Offset of file inside Zip storage</summary>
            public uint FileOffset;

            /// <summary>Size of header information</summary>
            public uint HeaderSize;

            /// <summary>32-bit checksum of entire file</summary>
            public uint Crc32;

            /// <summary>Last modification time of file</summary>
            public DateTime ModifyTime;

            /// <summary>User comment for file</summary>
            public string Comment;

            /// <summary>True if UTF8 encoding for filename and comments, false if default (CP 437)</summary>
            public bool EncodeUTF8;

            /// <summary>Overriden method</summary>
            /// <returns>Filename in Zip</returns>
            public override string ToString()
            {
                return FilenameInZip;
            }
        }
    }
}