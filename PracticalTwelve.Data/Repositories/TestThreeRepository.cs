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
<<<<<<< Updated upstream
        public const string ConnectionString = "Data Source=.;Initial Catalog=EmployeeDB;Persist Security Info=True;User ID=CTO;Password=CTO@123";
=======
        public const string ConnectionString = "Data Source=.;Initial Catalog=EmployeeDB;Persist Security Info=True;User ID=CTO;Password=CTO@123;TrustServerCertificate=True";
>>>>>>> Stashed changes

        public readonly SqlConnection _connection;

        public TestThreeRepository()
        {
            _connection = new SqlConnection(ConnectionString);
        }

        // Write a query to count the number of records by designation name
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

        // Write a query to display First Name, Middle Name, Last Name & Designation name
        public async Task<IEnumerable<EmployeeWithDesignation>> GetEmployeeDesignationDetailsAsync()
        {
            List<EmployeeWithDesignation> list = new List<EmployeeWithDesignation>();
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
                    EmployeeWithDesignation data = new EmployeeWithDesignation();
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
        
        // Create a database view that outputs Employee Id, First Name, Middle Name, Last Name, Designation, DOB, Mobile Number, Address & Salary
        public async Task<IEnumerable<EmployeeDetails>> GetEmployeeDesignationDetailsUsingViewAsync()
        {
            List<EmployeeDetails> list = new List<EmployeeDetails>();
            try
            {
                await _connection.OpenAsync();
                var query = $"SELECT * FROM [EmployeeDB].[dbo].[vw_EmployeeDetails]";

                SqlCommand sqlCommand = new SqlCommand(query, _connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeDetails data = new EmployeeDetails();
                    data.EmployeeInfo.Id = Convert.ToInt32(reader["Id"]);
                    data.EmployeeInfo.FirstName = reader["FirstName"].ToString();
                    data.EmployeeInfo.MiddleName = reader["MiddleName"].ToString();
                    data.EmployeeInfo.LastName = reader["LastName"].ToString();
                    data.EmployeeInfo.Salary = Convert.ToDecimal(reader["Salary"]);
                    data.EmployeeInfo.DOB = Convert.ToDateTime(reader["FirstName"]).Date;
                    data.EmployeeInfo.Address = reader["Address"].ToString();
                    data.Designation = reader["Designation"].ToString();
                    list.Add(data);
                }
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return list;
        }

        // Create a stored procedure to insert data into the Designation table with required parameters
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

        // Create a stored procedure to insert data into the Employee table with required parameters
        public async Task<int> InsertEmployeeInfoAsync(EmployeeInfo employeeInfo)
        {
            try
            {
                await _connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("usp_InsertEmployeesInfo", _connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FirstName", employeeInfo.FirstName));
                cmd.Parameters.Add(new SqlParameter("@MiddleName", employeeInfo.MiddleName));
                cmd.Parameters.Add(new SqlParameter("@LastName", employeeInfo.LastName));
                cmd.Parameters.Add(new SqlParameter("@DOB", employeeInfo.DOB));
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", employeeInfo.MobileNumber));
                cmd.Parameters.Add(new SqlParameter("@Address", employeeInfo.Address));
                cmd.Parameters.Add(new SqlParameter("@Salary", employeeInfo.Salary));
                cmd.Parameters.Add(new SqlParameter("@DesignationId", employeeInfo.DesignationId));
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

        // Write a query that displays only those designation names that have more than 1 employee
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

        // Write a query to find the employee having maximum salary
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

        // Create a stored procedure that returns a list of employees with columns Employee Id, First Name, Middle Name, Last Name, Designation, DOB, Mobile Number, Address & Salary (records should be ordered by DOB)
        public async Task<IEnumerable<EmployeeDetails>> GetEmployeeDetailsUsingSPAsync()
        {
            List<EmployeeDetails> list = new List<EmployeeDetails>();
            try
            {
                SqlCommand cmd = new SqlCommand("usp_EmployeesInformation", _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                await _connection.OpenAsync();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeDetails data = new EmployeeDetails();
                    data.EmployeeInfo.Id = Convert.ToInt32(reader["Id"]);
                    data.EmployeeInfo.FirstName = reader["FirstName"].ToString();
                    data.EmployeeInfo.MiddleName = reader["MiddleName"].ToString();
                    data.EmployeeInfo.LastName = reader["LastName"].ToString();
                    data.EmployeeInfo.Salary = Convert.ToDecimal(reader["Salary"]);
                    data.EmployeeInfo.DOB = Convert.ToDateTime(reader["FirstName"]).Date;
                    data.EmployeeInfo.Address = reader["Address"].ToString();
                    data.Designation = reader["Designation"].ToString();
                    list.Add(data);
                }
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return list;
        }

        // Create a stored procedure that return a list of employees by designation id (Input) with columns Employee Id, First Name, Middle Name, Last Name, DOB, Mobile Number, Address & Salary (records should be ordered by First Name)
        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeDetailsByDesignationIdUsingSPAsync(int designationId)
        {
            List<EmployeeInfo> list = new List<EmployeeInfo>();
            try
            {
                await _connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("usp_GetEmployeesByDesignationId", _connection);
                cmd.Parameters.AddWithValue("@DesignationId", designationId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeInfo data = new EmployeeInfo();
                    data.Id = Convert.ToInt32(reader["Id"]);
                    data.FirstName = reader["FirstName"].ToString();
                    data.MiddleName = reader["MiddleName"].ToString();
                    data.LastName = reader["LastName"].ToString();
                    data.Salary = Convert.ToDecimal(reader["Salary"]);
                    data.DOB = Convert.ToDateTime(reader["FirstName"]).Date;
                    data.Address = reader["Address"].ToString();
                    list.Add(data);
                }
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return list;
        }
    }
}
