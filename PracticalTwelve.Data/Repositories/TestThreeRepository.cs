using PracticalTwelve.Data.Interfaces;
using PracticalTwelve.Domain.Entities;
using PracticalTwelve.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PracticalTwelve.Data.Repositories
{
    public class TestThreeRepository : ITestThreeRepository
    {
        public const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True";

        public readonly SqlConnection _connection;

        public TestThreeRepository()
        {
            _connection = new SqlConnection(ConnectionString);
        }

        public async Task<IEnumerable<CountOfEmployeeByDesginationId>> GetEmployeeCountsByDesignationAsync()
        {
            List<CountOfEmployeeByDesginationId> list = new List<CountOfEmployeeByDesginationId>();
            try
            {
                await _connection.OpenAsync();
                var query = $@"SELECT d.[Designation], COUNT(e.[Id]) AS EmployeeCount FROM [dbo].[EmployeeInfo] e
                            JOIN [dbo].[Designation] d ON e.[DesignationId] = d.[Id]
                            GROUP BY d.[Designation] ORDER BY EmployeeCount DESC";

                SqlCommand sqlCommand = new SqlCommand(query, _connection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    CountOfEmployeeByDesginationId data = new CountOfEmployeeByDesginationId();
                    data.Designation = reader["Designation"].ToString();
                    data.EmployeeCount = Convert.ToInt32(reader["EmployeeCount"]);
                    list.Add(data);
                }

                await _connection.CloseAsync();
            }
            catch (Exception)
            {
                await _connection.CloseAsync();
            }
            return list;
        }

        public async Task<IEnumerable<EmployeeDesignationDetails>> GetEmployeeDesignationDetailsAsync()
        {
            List<EmployeeDesignationDetails> list = new List<EmployeeDesignationDetails>();
            try
            {
                await _connection.OpenAsync();
                var query = $@"SELECT e.[FirstName],e.[MiddleName],e.[LastName], d.[Designation] 
                            FROM [dbo].[EmployeeInfo] e
                            JOIN [dbo].[Designation] d ON e.[DesignationId] = d.[Id]";

                SqlCommand sqlCommand = new SqlCommand(query, _connection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeDesignationDetails data = new EmployeeDesignationDetails();
                    data.FirstName = reader["FirstName"].ToString();
                    data.MiddleName = reader["MiddleName"].ToString();
                    data.LastName = reader["LastName"].ToString();
                    data.Designation = reader["Designation"].ToString();
                    list.Add(data);
                }

                await _connection.CloseAsync();
            }
            catch (Exception)
            {
                await _connection.CloseAsync();
            }
            return list;
        }

        public async Task<List<string>> GetDesignationThatHaveMoreThanOneEmployeeAsync()
        {
            List<string> list = new List<string>();
            try
            {
                await _connection.OpenAsync();
                var query = $@"WITH UserCountByDesignation AS (
	                            SELECT d.[Designation], COUNT(e.[Id]) AS EmployeeCount FROM [dbo].[EmployeeInfo] e
	                            JOIN [dbo].[Designation] d ON e.[DesignationId] = d.[Id]
	                            GROUP BY d.[Designation]
                            )
                            SELECT Designation 
                            FROM UserCountByDesignation 
                            WHERE EmployeeCount > 1";

                SqlCommand sqlCommand = new SqlCommand(query, _connection);
                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                while (reader.Read()) list.Add(reader["Designation"].ToString());
                await _connection.CloseAsync();
            }
            catch (Exception)
            {
                await _connection.CloseAsync();
            }
            return list;
        }

        public async Task<EmployeeInfo> GetEmployeeHavingMaxSalaryAsync()
        {
            EmployeeInfo employee = new EmployeeInfo();
            try
            {
                await _connection.OpenAsync();
                var query = $@"SELECT TOP 1 * FROM [dbo].[EmployeeInfo] ORDER BY SALARY DESC";

                SqlCommand sqlCommand = new SqlCommand(query, _connection);
                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                await reader.ReadAsync();
                employee.Id = Convert.ToInt32(reader["Id"]);
                employee.FirstName = reader["FirstName"].ToString();
                employee.MiddleName = reader["MiddleName"].ToString();
                employee.LastName = reader["LastName"].ToString();
                employee.DOB = Convert.ToDateTime(reader["DOB"]).Date;
                employee.MobileNumber = reader["MobileNumber"].ToString();
                employee.Address = reader["Address"].ToString();
                employee.DesignationId = Convert.ToInt32(reader["DesignationId"]);
                await _connection.CloseAsync();
            }
            catch (Exception)
            {
                _connection.Close();
            }
            return employee;
        }

        public async Task<int> InsertDesignationAsync(string designation)
        {
            try
            {
                await _connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("usp_InsertDesignation", _connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Designation", designation));
                int inesrtedRow = await cmd.ExecuteNonQueryAsync();
                await _connection.CloseAsync();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                return inesrtedRow;
            }
            catch (Exception)
            {
                await _connection.CloseAsync();
            }
            return 0;
        }

        public async Task<int> InsertEmployeeInfoAsync(EmployeeInfo emp)
        {
            try
            {
                await _connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("usp_InsertEmployeesInfo", _connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FirstName", emp.FirstName));
                cmd.Parameters.Add(new SqlParameter("@MiddleName", emp.MiddleName));
                cmd.Parameters.Add(new SqlParameter("@LastName", emp.LastName));
                cmd.Parameters.Add(new SqlParameter("@DOB", emp.DOB));
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", emp.MobileNumber));
                cmd.Parameters.Add(new SqlParameter("@Address", emp.Address));
                cmd.Parameters.Add(new SqlParameter("@Salary", emp.Salary));
                cmd.Parameters.Add(new SqlParameter("@DesignationId", emp.DesignationId));
                int inesrtedRow = await cmd.ExecuteNonQueryAsync();
                await _connection.CloseAsync();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                return inesrtedRow;
            }
            catch (Exception)
            {
                _connection.Close();
            }
            return 0;
        }
    }
}
