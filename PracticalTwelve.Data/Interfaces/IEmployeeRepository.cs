using System.Threading.Tasks;

namespace PracticalTwelve.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<int> InsertSingleRecordAsync();
        Task<int> InsertMultipleRecord();

        Task<int> UpdateFirstRecordAsync();
        Task<int> UpdateMiddleNameAsync();

        Task<int> DeleteHavingLessValueThanId(int id);

        Task<int> DeleteAllData();
    }
}
