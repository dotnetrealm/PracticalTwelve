using PracticalTwelve.Data.Interfaces;
using PracticalTwelve.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PracticalTwelve.Data.Repositories
{
    public class TestOneRepository : ITestOneRepository
    {
        public const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True";

        public readonly SqlConnection _connection;

        public TestOneRepository()
        {
            _connection = new SqlConnection(ConnectionString);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Employees]", _connection);
            List<Employee> employees = new List<Employee>();

            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Employee emp = new Employee();
                emp.Id = Convert.ToInt32(reader["Id"].ToString());
                emp.FirstName = reader["FirstName"].ToString();
                emp.MiddleName = reader["MiddleName"].ToString();
                emp.LastName = reader["LastName"].ToString();
                emp.DOB = Convert.ToDateTime(reader["DOB"].ToString()).Date;
                emp.Address = reader["Address"].ToString();
                employees.Add(emp);
            }
            await _connection.CloseAsync();
            return employees;

        }

        public async Task<int> InsertSingleRecordAsync()
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Employees] VALUES (@FirstName, @MiddleName, @LastName, @DOB, @MobileNumber, @Address)", _connection);
            cmd.Parameters.Add(new SqlParameter("@FirstName", "Bhavin"));
            cmd.Parameters.Add(new SqlParameter("@MiddleName", "Rajeshbhai"));
            cmd.Parameters.Add(new SqlParameter("@LastName", "Kareliya"));
            cmd.Parameters.Add(new SqlParameter("@DOB", Convert.ToDateTime("09-02-2002").Date));
            cmd.Parameters.Add(new SqlParameter("@MobileNumber", "1231231231"));
            cmd.Parameters.Add(new SqlParameter("@Address", "Rajkot"));
            int inesrtedRow = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return inesrtedRow;
        }

        public async Task<int> InsertMultipleRecord(IEnumerable<Employee>? employees)
        {
            if (employees == null) return 0;

            int count = 0;
            await _connection.OpenAsync();
            //begin SQL transaction
            using (SqlTransaction transaction = _connection.BeginTransaction())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Employees]([FirstName], [MiddleName], [LastName],[DOB], [MobileNumber], [Address] ) VALUES (@FirstName, @MiddleName, @LastName, @DOB, @MobileNumber, @Address)", _connection, transaction);
                cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@MiddleName", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar, 100));

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

        public async Task<int> UpdateFirstNameOfFirstRecordAsync(string firstName = "SQLPerson")
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Employees] SET FirstName = @FirstName WHERE Id = (SELECT TOP(1) ID FROM [dbo].[Employees] ORDER BY Id)", _connection);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            int updatedRow = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return updatedRow;
        }

        public async Task<int> UpdateMiddleNameOfAllRecordsAsync(string middleName = "I")
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Employees] SET MiddleName = @MiddleName", _connection);
            cmd.Parameters.AddWithValue("@MiddleName", middleName);
            int updatedRow = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return updatedRow;
        }

        public async Task<int> DeleteHavingLessValueThanId(int id)
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new SqlCommand($"DELETE FROM [dbo].[Employees] WHERE Id < {id}", _connection);
            int deletedRows = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return deletedRows;
        }

        public async Task DeleteAllData()
        {
            await _connection.OpenAsync();
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[Employees]", _connection);
            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

    }
}
