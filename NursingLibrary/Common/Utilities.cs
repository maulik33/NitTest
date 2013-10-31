using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NursingLibrary
{
    public class CustomCacheCollection
    {
        private Dictionary<string, CustomCacheItem> cacheCollection;

        public int Length
        {
            get { return cacheCollection.Count; }
        }

        public CustomCacheCollection()
        {
            // fill the array with objects
            Initialize();
        }

        // used to prime the entire cache tree
        private void Initialize()
        {
            // Clear any items from the current collection
            if (cacheCollection != null)
                Release();

            // Get the list of current categories from database
            string[] categories = DBUtilities.GetCategories();
            cacheCollection = new Dictionary<string, CustomCacheItem>();

            for (int i = 0; i < categories.Length; i++)
            {
                CustomCacheItem item = new CustomCacheItem(categories[i]);
                Attach(item);
            }
        }

        public void Release()
        {
            cacheCollection = null;
        }

        public void Attach(CustomCacheItem item)
        {
            // attach the cache item to the collection
            if (!String.IsNullOrEmpty(item.Name))
                cacheCollection.Add(item.Name, item);
        }

        public void Update(string item)
        {
            // remove and update if required
            if (cacheCollection.ContainsKey(item))
                cacheCollection.Remove(item);

            CustomCacheItem cacheItem = new CustomCacheItem(item);
            Attach(cacheItem);
        }

        public void Remove(string item)
        {
            // remove the cache item from the collection
            if (cacheCollection.ContainsKey(item))
                cacheCollection.Remove(item);
        }

        public string Retrieve(string item, int index)
        {
            // retrieve value from underlying object
            if (cacheCollection.ContainsKey(item))
            {
                CustomCacheItem cacheItem = cacheCollection[item];
                return cacheItem.Retrieve(item, index);
            }

            return null;
        }
    }

    public class CustomCacheItem
    {
        // Private members
        private Dictionary<int, string> cacheItem;
        private string name;

        public string Name
        {
            get { return name; }
        }

        public CustomCacheItem(string category)
        {
            // load object from database
            Load(category);
        }

        // used to load data into the cache item
        private void Load(string category)
        {
            // get the data from the database for the entity
            if (!String.IsNullOrEmpty(category))
            {
                name = category;
                cacheItem = DBUtilities.BuildCategory(category);
            }
        }

        // used to retrieve data from cache item
        public string Retrieve(string item, int index)
        {
            if (cacheItem.ContainsKey(index))
                return cacheItem[index];
            else
                return null;
        }
    }

    public static class DBUtilities
    {
        // used to get all categories
        public static string[] GetCategories()
        {
            List<string> categories = new List<string>();
            string dbConn = System.Configuration.ConfigurationManager.ConnectionStrings["Nursing"].ToString();

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                // Get the categories from database
                using (SqlCommand cmd = new SqlCommand("usrGetCategories", conn))
                {
                    conn.Open();
                    SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (myReader.Read())
                    {
                        categories.Add(myReader["TableName"].ToString().Trim());
                    }
                }
            }

            return categories.ToArray();
        }

        // used to get specific category
        public static Dictionary<int, string> BuildCategory(string catEntity)
        {
            string dbConn = System.Configuration.ConfigurationManager.ConnectionStrings["Nursing"].ToString();
            Dictionary<int, string> cacheItem = new Dictionary<int, string>();

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                // Get the category from database
                using (SqlCommand cmd = new SqlCommand("usrGet" + catEntity + "Category", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                        cacheItem.Add((int)reader[0], reader[1].ToString());
                }
            }

            return cacheItem;
        }

        // used to get the content items from database for cache mgr
        //public static ContentItem LoadContentItem(object contentId)
        //{
        //    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Nursing"].ToString()))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("usrLoadContentItem", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            SqlParameter parameterContentId = new SqlParameter("@contentId", SqlDbType.Int, 4);
        //            parameterContentId.Value = contentId;
        //            cmd.Parameters.Add(parameterContentId);

        //            conn.Open();
        //            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
        //            if (reader != null)
        //            {
        //                reader.Read();
        //                ContentItem contentItem = new ContentItem(reader[0] as string, (int)reader[2],
        //                                                          (int)reader[1], reader[3] as string);
        //                reader.Close();
        //                return contentItem;
        //            }
        //            return null;
        //        }
        //    }
        //}
    }
}