using PracticalTwelve.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticalTwelve.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Returns all employees
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetAllEmployeeAsync();
        
        /// <summary>
        /// Insert new employee
        /// </summary>
        /// <returns>numbers of affected row</returns>
        Task<int> InsertSingleRecordAsync();

        /// <summary>
        /// Insert multiple empolyees
        /// </summary>
        /// <param name="employees">enumerable of Employee object</param>
        /// <returns>Number of inserted records in DB</returns>
        /// <exception cref="InvalidProgramException">thrown if any employee insertion fails</exception>
        Task<int> InsertMultipleRecord(IEnumerable<Employee>? employees);

        /// <summary>
        /// Update FirstName of first record in DB
        /// </summary>
        /// <param name="firstName">default is SQLPerson</param>
        /// <returns>Number of affected rows</returns>
        Task<int> UpdateFirstNameOfFirstRecordAsync(string FistName);

        /// <summary>
        /// Update MiddleName of all records in DB
        /// </summary>
        /// <param name="middleName">default is 'I'</param>
        /// <returns>Number of affected rows</returns>
        Task<int> UpdateMiddleNameOfAllRecordsAsync(string middleName = "I");

        /// <summary>
        /// Delete employees whose id is less than given id
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Number of rows affected in DB</returns>
        Task<int> DeleteHavingLessValueThanId(int id);

        /// <summary>
        /// Delete all employee data from DB
        /// </summary>
        /// <returns>Number of rows affected in DB</returns>
        Task DeleteAllData();
    }
}
