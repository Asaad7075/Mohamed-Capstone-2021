using DataAccessInterfaces;
using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ServiceCategoriesAccessor : IServiceCategoriesAccessor
    {
        public List<ServiceCategories> SelectAllServiceCategories()
        {
            List<ServiceCategories> categories = new List<ServiceCategories>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_categories", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var category = new ServiceCategories()
                        {
                            ServiceCategoryName = reader.GetString(0),
                            ServiceCategoryDescription = reader.GetString(1)
                        };
                        categories.Add(category);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return categories;
        }
    }
}
