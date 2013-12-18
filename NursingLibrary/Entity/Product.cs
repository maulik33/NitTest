namespace NursingLibrary.Entity
{
    public struct SimpleProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Product
    {
        public Product()
        {
        }
        
        public Product(int productId)
        {
            // using(IDataReader reader = Core.GetProductInfo(productId))
            // {
            //    if (reader == null)
            //    {
            //        Logger.LogError("Student constructor Error: Data reader null.");
            //        throw new ApplicationException("Product data could not be retrieved.");
            //    }

            // while(reader.Read())
            //    {
            //        _productId = productId;
            //        _productName = reader.GetString(reader.GetOrdinalOrThrow("ProductName"));
            //        _productType = reader.GetString(reader.GetOrdinalOrThrow("ProductType"));
            //        _multiUseTest = reader.GetInt32(reader.GetOrdinalOrThrow("MultiUseTest"));
            //        _testType = reader.GetString(reader.GetOrdinalOrThrow("TestType"));
            //        _scramble = reader.GetInt32(reader.GetOrdinalOrThrow("Scramble"));
            //        _remediation = reader.GetInt32(reader.GetOrdinalOrThrow("Remediation"));
            //        _bundle = reader.GetInt32(reader.GetOrdinalOrThrow("Bundle"));
            //    }
            // }
        }

                public int ProductId { get; set; }

        public string ProductName { get; set; }
        
        public string ProductType { get; set; }
        
        public int MultiUseTest { get; set; }
        
        public string TestType { get; set; }
        
        public int Scramble { get; set; }
        
        public int Remediation { get; set; }
        
        public int Bundle { get; set; }
    }
}
