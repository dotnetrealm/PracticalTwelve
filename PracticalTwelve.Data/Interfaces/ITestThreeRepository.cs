using PracticalTwelve.Domain.Entities;
using PracticalTwelve.Domain.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticalTwelve.Data.Interfaces
{
    public interface ITestThreeRepository
    {
        /// <summary>
        /// Return number of employees by designation name
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CountOfEmployeeByDesginationId>> GetEmployeeCountsByDesignationAsync();

        /// <summary>
        /// Returns list of FirstName, MiddleName, LastName and Designation of employee
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeDesignationDetails>> GetEmployeeDesignationDetailsAsync();

        /// <summary>
        /// Returns Designation names that have more than one employee
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetDesignationThatHaveMoreThanOneEmployeeAsync();

        /// <summary>
        /// Find the employee having maximum salary
        /// </summary>
        /// <returns></returns>
        Task<EmployeeInfo> GetEmployeeHavingMaxSalaryAsync();
    }
}
