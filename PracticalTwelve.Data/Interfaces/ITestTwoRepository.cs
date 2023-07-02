using PracticalTwelve.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticalTwelve.Data.Interfaces
{
    public interface ITestTwoRepository
    {
        /// <summary>
        /// Returns all employees
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeSalary>> GetAllEmployeeAsync();

        /// <summary>
        /// Insert multiple employees
        /// </summary>
        /// <returns>numbers of affected row</returns>
        Task<int> InsertMultipleRecord(IEnumerable<EmployeeSalary> employees);

        /// <summary>
        /// Find the total amount of salaries
        /// </summary>
        /// <returns>Total employee salary</returns>
        Task<decimal> GetTotalSalaryAsync();

        /// <summary>
        /// Get employees list older than given date
        /// </summary>
        /// <param name="date">date</param>
        /// <returns>Number of affected rows</returns>
        Task<IEnumerable<EmployeeSalary>> GetEmployeesOlderThanGivenDate(DateTime date);

        /// <summary>
        /// Get count of employees whose middlename is null
        /// </summary>
        /// <returns>count of employees</returns>
        Task<IEnumerable<EmployeeSalary>> GetNullMiddleNameEmployees();
    }
}
