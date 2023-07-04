﻿using PracticalTwelve.Domain.Entities;
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
        Task<IEnumerable<EmployeeWithDesignation>> GetEmployeeDesignationDetailsAsync();

        /// <summary>
        /// Returns employee details from view created in DB
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeDetails>> GetEmployeeDesignationDetailsUsingViewAsync();

        /// <summary>
        /// Insert new designation
        /// </summary>
        /// <param name="designation"></param>
        /// <returns></returns>
        Task<int> InsertDesignationAsync(string designation);

        /// <summary>
        /// Insert new employee
        /// </summary>
        /// <param name="employeeInfo">EmployeeInfo object</param>
        /// <returns></returns>
        Task<int> InsertEmployeeInfoAsync(EmployeeInfo employeeInfo);

        /// <summary>
        /// Returns Designation names that have more than one employee
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetDesignationThatHaveMoreThanOneEmployeeAsync();

        /// <summary>
        /// Returns Users details from Store Procedure
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeDetails>> GetEmployeeDetailsUsingSPAsync();

        /// <summary>
        /// Returns Users details from Store Procedure
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeInfo>> GetEmployeeDetailsByDesignationIdUsingSPAsync(int designationId);

        /// <summary>
        /// Find the employee having maximum salary
        /// </summary>
        /// <returns></returns>
        Task<EmployeeInfo> GetEmployeeHavingMaxSalaryAsync();

        
    }
}
