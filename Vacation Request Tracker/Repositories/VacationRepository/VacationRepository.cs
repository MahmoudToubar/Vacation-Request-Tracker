using Microsoft.EntityFrameworkCore;
using Vacation_Request_Tracker.Data;
using Vacation_Request_Tracker.Models;

namespace Vacation_Request_Tracker.Repositories.Vacation
{
    public class VacationRepository : IVacationRepository
    {
        private readonly VacationDbContext vacationDbContext;

        public VacationRepository(VacationDbContext vacationDbContext)
        {
            this.vacationDbContext = vacationDbContext;
        }

        public async Task<TbVacationRequest> AddAsync(TbVacationRequest vacation)
        {
            await vacationDbContext.VacationsRequest.AddAsync(vacation);
            await vacationDbContext.SaveChangesAsync();

            return vacation;
        }

        public async Task<TbVacationRequest?> DeleteAsync(Guid id)
        {
            var vacation = await vacationDbContext.VacationsRequest.FindAsync(id);

            if (vacation != null)
            {
                vacationDbContext.VacationsRequest.Remove(vacation);
                await vacationDbContext.SaveChangesAsync();
                return vacation;
            }

            return null;
        }

        public async Task<IEnumerable<TbVacationRequest>> GetAllAsync()
        {
            return await vacationDbContext.VacationsRequest.ToListAsync();
        }

        public Task<TbVacationRequest?> GetAsync(Guid id)
        {
            return vacationDbContext.VacationsRequest.FirstOrDefaultAsync(x => x.RequestId == id);
        }

        public async Task<TbVacationRequest?> UpdateAsync(TbVacationRequest request)
        {
            var vacation = await vacationDbContext.VacationsRequest.FindAsync(request.RequestId);

            if (vacation != null)
            {
                vacation.EmployeeName = request.EmployeeName;
                vacation.Department = request.Department;
                vacation.SubmissionDate = request.SubmissionDate;
                vacation.VacationDateFrom = request.VacationDateFrom;
                vacation.VacationDateTo = request.VacationDateTo;
                vacation.Title = request.Title;
                vacation.Notes = request.Notes;

                await vacationDbContext.SaveChangesAsync();

                return vacation;
            }

            return null;
        }
    }
}
