namespace NursingLibrary.Entity
{
    public class TestSubGroup
    {
        public TestSubGroup(int id, string assetCode, string productCode)
        {
            Id = id;
            AssetCode = assetCode;
            ProductCode = productCode;
        }

        public int Id { get; set; }

        public string AssetCode { get; set; }

        public string ProductCode { get; set; }
    }
}
