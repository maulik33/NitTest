using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;
using NursingRNWeb;

public partial class LogAdmin : System.Web.UI.Page
{
    public List<LogData> Data { get; set; }

    private int Days
    {
        get
        {
            int days;
            if (false == Int32.TryParse(daysTextBox.Text, out days))
            {
                days = 3;
            }

            return days;
        }
    }

    public List<LogData> ReadFile(string filename, string delimiter)
    {
        string viewstateKey = filename;

        filename = Path.Combine(KTPApp.LogsFolder, filename);

        var logDatas = new List<LogData>();

        ////  Check file       
        if (!File.Exists(filename))
        {
            throw new FileNotFoundException("File not found", filename);
        }

        string line = string.Empty;
        try
        {
            IFormatProvider culture = new CultureInfo("en-US", true);

            using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    ////  Read the file     

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] columns = line.Split(new[] { delimiter }, StringSplitOptions.None);
                        if (columns.Count() < 10)
                        {
                            continue;
                        }

                        if (columns.Count() >= 10 && columns.Count() < 13)
                        {
                            var extraCols = new List<string>(columns);

                            for (var i = columns.Count() - 1; i < 12; i++)
                            {
                                extraCols.Add(string.Empty);
                            }

                            columns = extraCols.ToArray();
                        }

                        logDatas.Add(new LogData
                        {
                            UserHostAddress = columns[0],
                            UserAgent = columns[1],
                            URL = columns[2],
                            SessionId = columns[3],
                            UserName = columns[4],
                            ClassName = columns[5],
                            MethodName = columns[6],
                            ProcessedType = columns[7],
                            DateLogged = DateTime.Parse(columns[8], culture, DateTimeStyles.NoCurrentDateDefault),
                            ExecutionTimeInMilliS = columns[9],
                            Exception = columns[10],
                            InputParameters = columns[11],
                            OutPutParameters = columns[12],
                        });
                    }
                }
            }
        }
        catch (Exception exception)
        {
            Response.Write(string.Format("Problem in reading line - {0} and exception is {1}", line, exception));
        }

        var data = logDatas.OrderByDescending(key => key.DateLogged).ThenBy(key => key.URL).ThenBy(key => key.UserName).ToList();
        ViewState[viewstateKey] = data;
        Data = data;
        return data;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayTraceFilesSummary();
            RefreshFileList();
            TraceDateFrom.Text = DateTime.Now.AddDays(-200).ToShortDateString();
            TraceDateTo.Text = DateTime.Now.ToShortDateString();
            TraceEnabledLabel.Text = KTPApp.TraceEnabled.ToString();
        }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        var loggerName = ((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository()).Root.Level.Name;
        LogLevel.Text = @"Logger Level : " + loggerName;
        ChangeToDebug.Text = string.Format("Change To {0}", ((loggerName == @"DEBUG" || loggerName == @"ALL") ? "ERROR" : "DEBUG"));

        var fileCount = Directory.GetFiles(KTPApp.LogsFolder, "Nursing*.log.*").Where(f => File.GetCreationTime(f).AddDays(Days) >= DateTime.Now).Count();
        OldLogFilesCount.Text = string.Format(@"Total Log Files within last {0} Days : {1}", Days, fileCount);
        ClearOldLog.Enabled = fileCount > 0;
    }

    protected void ChangeToDebug_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsPassCodeValid())
            {
                var loggerName = ((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository()).Root.Level.Name;
                TurnOnLogging(((loggerName == @"DEBUG" || loggerName == @"ALL") ? "ERROR" : "DEBUG"));
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ExceptionHelper.Handle(ex), true);
        }
    }

    protected void ClearOldLog_Click(object sender, EventArgs e)
    {
        if (false == IsPassCodeValid())
        {
            return;
        }

        foreach (string file in Directory.GetFiles(KTPApp.LogsFolder, "Nursing*.log.*"))
        {
            if (DateTime.Now.Subtract(File.GetCreationTime(file)).Days > Days)
            {
                File.Delete(file);
            }
        }

        foreach (string file in Directory.GetFiles(KTPApp.TraceFolder, "Nursing*.log.*"))
        {
            if (File.GetCreationTime(file).AddDays(Days) < DateTime.Now)
            {
                File.Delete(file);
            }
        }
    }

    protected void GetLogLog_Click(object sender, EventArgs e)
    {
        if (LogFiles.SelectedIndex == 0)
        {
            GridView1.DataSource = new List<LogData>();
            GridView1.DataBind();
        }
        else
        {
            GridView1.PageIndex = 0;
            BindGrid();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        var data = GridView1.DataSource as List<LogData> ?? ViewState[LogFiles.SelectedValue] as List<LogData>;
        if (data != null)
        {
            var newData = new List<LogData>();

            switch (e.SortExpression)
            {
                case "UserHostAddress":
                    {
                        newData = e.SortDirection == SortDirection.Ascending
                                                ? data.OrderBy(key => key.UserHostAddress).ToList()
                                                : data.OrderByDescending(key => key.UserHostAddress).ToList();
                    }

                    break;
                case "URL":
                    {
                        newData = e.SortDirection == SortDirection.Ascending
                                                ? data.OrderBy(key => key.URL).ToList()
                                                : data.OrderByDescending(key => key.URL).ToList();
                    }

                    break;
                case "ClassName":
                    {
                        newData = e.SortDirection == SortDirection.Ascending
                                                ? data.OrderBy(key => key.ClassName).ToList()
                                                : data.OrderByDescending(key => key.ClassName).ToList();
                    }

                    break;
                case "MethodName":
                    {
                        newData = e.SortDirection == SortDirection.Ascending
                                                ? data.OrderBy(key => key.MethodName).ToList()
                                                : data.OrderByDescending(key => key.MethodName).ToList();
                    }

                    break;
                case "ProcessedType":
                    {
                        newData = e.SortDirection == SortDirection.Ascending
                                                ? data.OrderBy(key => key.ProcessedType).ToList()
                                                : data.OrderByDescending(key => key.ProcessedType).ToList();
                    }

                    break;
                case "DateLogged":
                    {
                        newData = e.SortDirection == SortDirection.Ascending
                                                ? data.OrderBy(key => key.DateLogged).ToList()
                                                : data.OrderByDescending(key => key.DateLogged).ToList();
                    }

                    break;
                case "UserName":
                    {
                        newData = e.SortDirection == SortDirection.Ascending
                                                ? data.OrderBy(key => key.UserName).ToList()
                                                : data.OrderByDescending(key => key.UserName).ToList();
                    }

                    break;
                case "SessionId":
                    {
                        newData = e.SortDirection == SortDirection.Ascending
                                                ? data.OrderBy(key => key.SessionId).ToList()
                                                : data.OrderByDescending(key => key.SessionId).ToList();
                    }

                    break;
            }

            ViewState[LogFiles.SelectedValue] = newData;
            BindGrid();
        }
    }

    protected void BtnViewDetails_Click(object sender, EventArgs e)
    {
        // get the gridviewrow from the sender so we can get the datakey we need
        var btnDetails = sender as LinkButton;
        lblDetail.Text = btnDetails.CommandArgument.Replace(";", Environment.NewLine);

        updPnlDetail.Update();

        // show the modal popup
        mdlPopup.Show();
    }

    protected void RefreshFileListButton_Click(object sender, EventArgs e)
    {
        RefreshFileList();
    }

    protected void TraceFileList_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            if (e.Node.ChildNodes.Count != 0)
            {
                return;
            }

            switch (e.Node.Depth)
            {
                case 1:
                    {
                        PopulateTraceData(2, e.Node);
                        break;
                    }

                case 2:
                    {
                        PopulateTraceData(3, e.Node);
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            ShowError(ExceptionHelper.Handle(ex));
        }
    }

    protected void TraceRefresh_Click(object sender, EventArgs e)
    {
        ////MainTabControl.ActiveTabIndex = 1;
        TraceFileList.Nodes[0].ChildNodes.Clear();
        PopulateTraceData(1, TraceFileList.Nodes[0]);
    }

    protected void TraceFileList_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (TraceFileList.SelectedNode == null)
        {
            return;
        }

        TraceValuePath valuePath = new TraceValuePath(TraceFileList.SelectedNode);
        if (string.IsNullOrEmpty(valuePath.SessionId))
        {
            return;
        }

        var filteredFiles = GetTraceFilesList().Where(f => f.CreationTime.ToShortDateString() == valuePath.DateString);

        List<TraceInfo> traceSessions = new List<TraceInfo>();
        StringBuilder strBuilder = new StringBuilder();
        foreach (var file in filteredFiles)
        {
            try
            {
                ParseTraceFile(file,
                    new List<TraceType>() { TraceType.Init, TraceType.End, TraceType.Event, TraceType.Error }, ref traceSessions);
            }
            catch (Exception ex)
            {
                strBuilder.Append(ex.Message);
            }
        }

        if (strBuilder.ToString().Length > 0)
        {
            ShowError(strBuilder.ToString());
        }

        TraceInfo initSession = traceSessions.Where(t => t.Type == TraceType.Init && t.Id.ToString() == valuePath.SessionId).FirstOrDefault();
        TraceUserName.Text = string.Format("{0} ({1})", initSession.UserName, initSession.UserId);
        TraceEnvironment.Text = initSession.Environment;

        TraceSessionContainer.DataSource = traceSessions.Where(t => t.Type != TraceType.Init
            && t.Id.ToString() == valuePath.SessionId).ToList();
        TraceSessionContainer.DataBind();
    }

    protected void TraceEnabledToggleButton_Click(object sender, EventArgs e)
    {
        if (IsPassCodeValid())
        {
            KTPApp.TraceEnabled = !KTPApp.TraceEnabled;
            KTPApp.SaveAppSettings(DIHelper.GetServiceHelperInstance());
            UpdateAppSettingsDisplay();
        }
    }

    protected void TraceSessionContainer_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType != ListViewItemType.DataItem)
        {
            return;
        }

        TraceInfo info = e.Item.DataItem as TraceInfo;
        if (info != null
            && info.Type == TraceType.Error)
        {
            HtmlTableRow row = e.Item.FindControl("TraceDataRow") as HtmlTableRow;
            if (row != null)
            {
                row.Style.Add("background-color", "red");
            }
        }
    }

    protected void RefreshAppSettings_Click(object sender, EventArgs e)
    {
        KTPApp.RefreshAppSettings(DIHelper.GetServiceHelperInstance());
        UpdateAppSettingsDisplay();
    }

    private static bool IsLocalIP(IPAddress IP)
    {
        var localIps = GetAllLocalIpAddresses();
        return localIps.Any(localIp => GetNetworkAddress(localIp.Address, localIp.IPv4Mask).Equals(GetNetworkAddress(IP, localIp.IPv4Mask)));
    }

    private static IEnumerable<UnicastIPAddressInformation> GetAllLocalIpAddresses()
    {
        var localAddresses = new List<UnicastIPAddressInformation>();

        var query = NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback).SelectMany(
                       nic => nic.GetIPProperties().UnicastAddresses, (nic, nicAddress) => new { nic, nicAddress }).Where(
                           t => t.nicAddress.Address.AddressFamily == AddressFamily.InterNetwork).Select(t => t.nicAddress);

        localAddresses.AddRange(query);

        return localAddresses;
    }

    // http://www.experts-exchange.com/Programming/Languages/C_Sharp/Q_26679395.html
    private static IPAddress GetNetworkAddress(IPAddress address, IPAddress netmask)
    {
        if (address == null || netmask == null)
        {
            throw new ArgumentNullException();
        }

        byte[] addressBytes = address.GetAddressBytes();
        byte[] netmaskBytes = netmask.GetAddressBytes();
        var network = new byte[4];

        // Bitwise AND'ing of an IP address with it's subnet mask 
        // will get you the network number (i.e. the first address in the range, like 192.168.1.0) 
        for (int i = 0; i < 4; i++)
        {
            network[i] = (byte)(addressBytes[i] & netmaskBytes[i]);
        }

        return new IPAddress(network);
    }

    private static void TurnOnLogging(string loggerLevel)
    {
        log4net.Repository.ILoggerRepository[] repositories = log4net.LogManager.GetAllRepositories();

        ////Configure all loggers to be at the debug level.
        foreach (log4net.Repository.ILoggerRepository repository in repositories)
        {
            repository.Threshold = repository.LevelMap[loggerLevel];
            var hier = (log4net.Repository.Hierarchy.Hierarchy)repository;
            log4net.Core.ILogger[] loggers = hier.GetCurrentLoggers();
            foreach (log4net.Core.ILogger logger in loggers)
            {
                ((log4net.Repository.Hierarchy.Logger)logger).Level = hier.LevelMap[loggerLevel];
            }
        }

        ////Configure the root logger.
        var h = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
        log4net.Repository.Hierarchy.Logger rootLogger = h.Root;
        rootLogger.Level = h.LevelMap[loggerLevel];
    }

    private void UpdateAppSettingsDisplay()
    {
        TraceEnabledLabel.Text = KTPApp.TraceEnabled.ToString();
    }

    private bool IsPassCodeValid()
    {
        if (string.Compare(PassCodeText.Text, "Kaplan@123") != 0)
        {
            ShowMessage("Passcode did not match. Operation failed.", true);
            return false;
        }
        else
        {
            return true;
        }
    }

    private void BindGrid()
    {
        var result = (ViewState[LogFiles.SelectedValue] as List<LogData>) ?? ReadFile(LogFiles.SelectedValue, "|");

        GridView1.DataSource = result;
        GridView1.DataBind();

        try
        {
            if (result == null)
            {
                return;
            }

            /*
            var summary = from s in result
                          group s by s.ClassName into clsGroup
                          select new
                          {
                              GroupName = g.Key,
                              methodGroup = from s in clsGroup
                                            group s by s.MethodName into mthGroup
                                            select new { MethodName = mthGroup.Key, Count = mthGroup.Count() }
                          };
            */

            if (chkGroupByException.Checked)
            {
                var summary = from s in result
                              group s by string.Format("{0}.{1}", s.ClassName, s.MethodName + ((s.Exception.Length > 300) ? s.Exception.Substring(1, 300) : s.Exception)) into g
                              select new
                              {
                                  GroupName = g.Key,
                                  Count = g.Count()
                              };
                SummaryGrid.DataSource = summary.ToList();
                SummaryGrid.DataBind();
            }
            else
            {
                var summary = from s in result
                              group s by string.Format("{0}.{1}", s.ClassName, s.MethodName) into g
                              select new
                              {
                                  GroupName = g.Key,
                                  Count = g.Count()
                              };
                SummaryGrid.DataSource = summary.ToList();
                SummaryGrid.DataBind();
            }

            long id = DateTime.Now.Ticks;
        }
        catch
        {
        }
    }

    private void RefreshFileList()
    {
        var files =
            Directory.GetFiles(KTPApp.LogsFolder, "Nursing*.log.*").Where(
                fl => (DateTime.Now.Subtract(File.GetCreationTime(fl)).Days < Days)).Select(fl => Path.GetFileName(fl)).ToList();
        files.Insert(0, "Select");
        LogFiles.DataSource = files;
        LogFiles.DataBind();
    }

    private List<FileInfo> GetTraceFilesList()
    {
        DateTime fromDate, toDate;

        if (false == DateTime.TryParse(TraceDateFrom.Text, out fromDate))
        {
            throw new ApplicationException("Invalid From Date specified in Trace Date Range.");
        }

        if (false == DateTime.TryParse(TraceDateTo.Text, out toDate))
        {
            throw new ApplicationException("Invalid To Date specified in Trace Date Range.");
        }

        toDate = toDate.AddDays(1);
        var files = Directory.GetFiles(KTPApp.TraceFolder, "Nursing*.log.*")
            .Select(p => new FileInfo(p))
            .Where(f => f.CreationTime >= fromDate && f.CreationTime <= toDate);
        return files.ToList();
    }

    private void ShowError(string message)
    {
        ShowMessage(message, true);
    }

    private void ShowMessage(string message, bool isError)
    {
        MsgLabel.Text = message;
        MsgLabel.ForeColor = isError ? Color.Red : Color.Green;
    }

    private void ParseTraceFile(FileInfo file, IEnumerable<TraceType> filter, ref List<TraceInfo> traceSessions)
    {
        using (FileStream fs = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (StreamReader rdr = new StreamReader(fs))
            {
                bool isEntityComplete = true;
                TraceInfo data = null;
                while (false == rdr.EndOfStream)
                {
                    string line = rdr.ReadLine();
                    try
                    {
                        if (false == isEntityComplete
                            && data != null)
                        {
                            if (TraceDataParser.IsErrorData(line))
                            {
                                data.Message = string.Format("{0}{1}", data.Message, line);
                                continue;
                            }

                            isEntityComplete = true;
                        }

                        data = TraceDataParser.ToObject(line);
                        if (data != null)
                        {
                            if (filter == null || filter.Contains(data.Type))
                            {
                                traceSessions.Add(data);
                            }

                            //// If error is logged for this session, mark it in session header.
                            if (data.Type == TraceType.Error)
                            {
                                isEntityComplete = false;
                                TraceInfo traceBeginRow = traceSessions.Where(p => p.Id == data.Id).FirstOrDefault();
                                if (traceBeginRow != null)
                                {
                                    traceBeginRow.HasError = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //////// Helps while debugging
                        throw ex;
                    }
                }

                rdr.Close();
            }

            fs.Close();
        }
    }

    private void PopulateTraceData(int groupByValue, TreeNode parentNode)
    {
        IEnumerable<KeyValuePair<string, string>> traceData = null;
        string imageUrl = "Images/base_exclamationmark_16.png";
        switch (groupByValue)
        {
            case 1:
                {
                    imageUrl = "Images/book_reportHS.png";
                    traceData = GetTraceData();
                    break;
                }

            case 2:
                {
                    imageUrl = "Images/User.png";
                    traceData = GetTraceDataByDate(new TraceValuePath(parentNode));
                    break;
                }

            case 3:
                {
                    imageUrl = "Images/DocumentHS.png";
                    traceData = GetTraceDataByUser(new TraceValuePath(parentNode));
                    break;
                }
        }

        foreach (var item in traceData)
        {
            TreeNode node = new TreeNode(item.Value, item.Key, imageUrl);
            if (groupByValue == 3)
            {
                node.PopulateOnDemand = false;
                node.ToolTip = item.Key;
            }
            else
            {
                node.PopulateOnDemand = true;
            }

            parentNode.ChildNodes.Add(node);
        }
    }

    private IEnumerable<KeyValuePair<string, string>> GetTraceData()
    {
        var filteredFiles = from f in GetTraceFilesList()
                            group f by f.CreationTime.ToShortDateString() into g
                            select new KeyValuePair<string, string>(g.Key, string.Format("{0} ({1})", g.Key, g.Count()));

        return filteredFiles.ToList();
    }

    private IEnumerable<KeyValuePair<string, string>> GetTraceDataByDate(TraceValuePath valuePath)
    {
        var filteredFiles = GetTraceFilesList().Where(f => f.CreationTime.ToShortDateString() == valuePath.DateString);

        List<TraceInfo> traceSessions = new List<TraceInfo>();
        StringBuilder strBuilder = new StringBuilder();
        foreach (var file in filteredFiles)
        {
            try
            {
                ParseTraceFile(file,
                    new List<TraceType>() { TraceType.Init }, ref traceSessions);
            }
            catch (Exception ex)
            {
                strBuilder.Append(ex.Message);
            }
        }

        if (strBuilder.ToString().Length > 0)
        {
            ShowError(strBuilder.ToString());
        }

        var userTrace = from t in traceSessions
                        where (TraceShowOnlyErrors.Checked == false
                        || (TraceShowOnlyErrors.Checked && t.HasError))
                        group t by TraceValuePath.GetUserKey(t) into g
                        select new KeyValuePair<string, string>(g.Key,
                            string.Format("{0} - {1} Session(s)", g.Key, g.Count()));

        List<KeyValuePair<string, string>> xx = userTrace.ToList();

        return xx;
    }

    private IEnumerable<KeyValuePair<string, string>> GetTraceDataByUser(TraceValuePath valuePath)
    {
        var filteredFiles = GetTraceFilesList().Where(f => f.CreationTime.ToShortDateString() == valuePath.DateString);

        List<TraceInfo> traceSessions = new List<TraceInfo>();
        StringBuilder strBuilder = new StringBuilder();
        foreach (var file in filteredFiles)
        {
            try
            {
                ParseTraceFile(file,
                    new List<TraceType>() { TraceType.Init, TraceType.Error }, ref traceSessions);
            }
            catch (Exception ex)
            {
                strBuilder.Append(ex.Message);
            }
        }

        if (strBuilder.ToString().Length > 0)
        {
            ShowError(strBuilder.ToString());
        }

        var userTrace = from t in traceSessions
                        where t.UserId.ToString() == valuePath.UserId
                        && (TraceShowOnlyErrors.Checked == false
                        || (TraceShowOnlyErrors.Checked && t.HasError))
                        select new KeyValuePair<string, string>(t.Id.ToString(),
                            t.Time);
        return userTrace.ToList();
    }

    private void DisplayTraceFilesSummary()
    {
        StringBuilder builder = new StringBuilder();

        var files = Directory.GetFiles(KTPApp.TraceFolder, "Nursing*.log.*")
            .Select(p => new FileInfo(p))
            .Where(f => f.CreationTime.AddDays(Days) >= DateTime.Now);

        builder.AppendLine(string.Format("Total Files within Specified Period - {0}", files.Count()));
        builder.AppendLine(string.Format("Total Files in Trace folder - {0}", new DirectoryInfo(KTPApp.TraceFolder).GetFiles().Count()));

        DirectoryInfo traceFolder = new DirectoryInfo(KTPApp.TraceFolder);

        DriveInfo logFileDrive = (from d in DriveInfo.GetDrives()
                                  where d.Name.IsEqual(traceFolder.Root.FullName)
                                  select d).FirstOrDefault();

        if (logFileDrive != null)
        {
            builder.AppendLine(string.Format("Total Drive Size - {0} MB", Utilities.GetSize(logFileDrive.TotalSize)));
            builder.AppendLine(string.Format("Available Free Space - {0} MB", Utilities.GetSize(logFileDrive.AvailableFreeSpace)));
        }

        TraceFilesSummary.Text = builder.ToString().Replace("\r\n", "<BR>");
    }

    protected void btnSubmit_Click(Object sender, EventArgs e)
    {
        var isValidUser = Utilities.IsValidDomainUser(txtUserName.Text.Trim(), txtPassword.Text.Trim());
        if (isValidUser)
        {
            displayLogDiv.Visible = true;
            errMsgLbl.Visible = false;
            btnSubmit.Enabled = false;
        }
        else
            errMsgLbl.Visible = true;
    }
}