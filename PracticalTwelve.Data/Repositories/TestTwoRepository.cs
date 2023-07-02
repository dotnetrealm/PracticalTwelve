using PracticalTwelve.Data.Interfaces;
using PracticalTwelve.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PracticalTwelve.Data.Repositories
{
    public class TestTwoRepository : ITestTwoRepository
    {
        public const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True";

        public readonly SqlConnection _connection;

        public TestTwoRepository()
        {
            _connection = new SqlConnection(ConnectionString);
        }
        public async Task<IEnumerable<EmployeeSalary>> GetAllEmployeeAsync()
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[EmployeeSalary] ", _connection);
            List<EmployeeSalary> employees = new List<EmployeeSalary>();

            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                EmployeeSalary emp = new EmployeeSalary();
                emp.Id = Convert.ToInt32(reader["Id"].ToString());
                emp.FirstName = reader["FirstName"].ToString();
                emp.MiddleName = reader["MiddleName"].ToString();
                emp.LastName = reader["LastName"].ToString();
                emp.DOB = Convert.ToDateTime(reader["DOB"].ToString()).Date;
                emp.Address = reader["Address"].ToString();
                emp.Salary = Convert.ToDecimal(reader["Salary"]);
                employees.Add(emp);
            }
            await _connection.CloseAsync();
            return employees;
        }
        public async Task<int> InsertMultipleRecord(IEnumerable<EmployeeSalary> employees)
        {
            if (employees == null) return 0;

            int count = 0;
            await _connection.OpenAsync();
            //begin SQL transaction
            using (SqlTransaction transaction = _connection.BeginTransaction())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[EmployeeSalary]([FirstName], [MiddleName], [LastName],[DOB], [MobileNumber], [Address], [Salary] ) VALUES (@FirstName, @MiddleName, @LastName, @DOB, @MobileNumber, @Address, @Salary)", _connection, transaction);
                cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@MiddleName", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Decimal));

                try
                {
                    foreach (var emp in employees)
                    {
                        cmd.Parameters[0].Value = emp.FirstName;

                        // Assigned DBNull.Value to MiddleName,otherwise it'll consider default value of DB if MiddleName is null
                        // which might throw exception if default value was not assigned
                        cmd.Parameters[1].Value = emp.MiddleName ?? (object)DBNull.Value;
                        cmd.Parameters[2].Value = emp.LastName;
                        cmd.Parameters[3].Value = emp.DOB;
                        cmd.Parameters[4].Value = emp.MobileNumber;
                        cmd.Parameters[5].Value = emp.Address ?? (object)DBNull.Value;
                        cmd.Parameters[6].Value = emp.Salary;
                        if (await cmd.ExecuteNonQueryAsync() != 1) throw new InvalidProgramException();
                        else ++count;
                    }
                    //commit transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    //rollback transaction
                    transaction.Rollback();
                    throw ex;
                }
            }
            await _connection.CloseAsync();
            return count;
        }
        public async Task<decimal> GetTotalSalaryAsync()
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new SqlCommand("SELECT SUM(Salary) as TotalSalary FROM [dbo].[EmployeeSalary]", _connection);
            decimal totalSalary = (decimal)await cmd.ExecuteScalarAsync();
            await _connection.CloseAsync();
            return totalSalary;
        }
        public async Task<IEnumerable<EmployeeSalary>> GetEmployeesOlderThanGivenDate(DateTime date)
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[EmployeeSalary] WHERE DOB < @Date", _connection);
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(date).Date);
            List<EmployeeSalary> employees = new List<EmployeeSalary>();

            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                EmployeeSalary emp = new EmployeeSalary();
                emp.Id = Convert.ToInt32(reader["Id"].ToString());
                emp.FirstName = reader["FirstName"].ToString();
                emp.MiddleName = reader["MiddleName"].ToString();
                emp.LastName = reader["LastName"].ToString();
                emp.DOB = Convert.ToDateTime(reader["DOB"].ToString()).Date;
                emp.Address = reader["Address"].ToString();
                emp.Address = reader["Address"].ToString();
                employees.Add(emp);
            }
            await _connection.CloseAsync();
            return employees;

        }
        public async Task<IEnumerable<EmployeeSalary>> GetNullMiddleNameEmployees()
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[EmployeeSalary] WHERE MiddleName IS NULL", _connection);
            List<EmployeeSalary> employees = new List<EmployeeSalary>();

            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                EmployeeSalary emp = new EmployeeSalary();
                emp.Id = Convert.ToInt32(reader["Id"].ToString());
                emp.FirstName = reader["FirstName"].ToString();
                emp.MiddleName = reader["MiddleName"].ToString();
                emp.LastName = reader["LastName"].ToString();
                emp.DOB = Convert.ToDateTime(reader["DOB"].ToString()).Date;
                emp.Address = reader["Address"].ToString();
                emp.Address = reader["Address"].ToString();
                employees.Add(emp);
            }
            await _connection.CloseAsync();
            return employees;
        }


    }
}
