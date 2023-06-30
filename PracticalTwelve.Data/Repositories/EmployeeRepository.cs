using PracticalTwelve.Data.Interfaces;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PracticalTwelve.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private const string CONNECTION_STRING = "Data Source=.\\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True";
        public async Task<int> DeleteAllData()
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Employees]");
                int inesrtedRow = await cmd.ExecuteNonQueryAsync();
                return inesrtedRow;
            }
        }

        public async Task<int> DeleteHavingLessValueThanId(int id)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Employees] WHERE Id < 2");
                int updatedRow = await cmd.ExecuteNonQueryAsync();
                return updatedRow;
            }
        }

        public Task<int> InsertMultipleRecord()
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertSingleRecordAsync()
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Employees] VALUES('Bhavin' ,'Rajeshbhai' ,'Kareliya' ,'2002-02-09' ,'1231231231' ,'Rajkot')");
                int inesrtedRow = await cmd.ExecuteNonQueryAsync();
                return inesrtedRow;
            }
        }

        public async Task<int> UpdateFirstRecordAsync()
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Employees] SET FirstName = 'Updated First Name' WHERE Id = (SELECT TOP(1) * FROM [dbo].[Employees] ORDER BY Id)");
                int updatedRow = await cmd.ExecuteNonQueryAsync();
                return updatedRow;
            }
        }

        public async Task<int> UpdateMiddleNameAsync()
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Employees] SET MiddleName = 'Updated Middle  Name'");
                int updatedRow = await cmd.ExecuteNonQueryAsync();
                return updatedRow;
            }
        }
    }
}
