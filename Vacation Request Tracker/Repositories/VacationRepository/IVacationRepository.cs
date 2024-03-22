using Vacation_Request_Tracker.Models;

namespace Vacation_Request_Tracker.Repositories.Vacation
{
    public interface IVacationRepository
    {
        Task<IEnumerable<TbVacationRequest>> GetAllAsync();
        Task<TbVacationRequest?> GetAsync(Guid id);
        Task<TbVacationRequest> AddAsync(TbVacationRequest Vacation);
        Task<TbVacationRequest?> UpdateAsync(TbVacationRequest Vacation);
        Task<TbVacationRequest?> DeleteAsync(Guid id);
    }
}
