using Heyday.Application.Common.Contracts;
using Heyday.Application.Catalog.Contracts;
using Heyday.Application.Wrapper;
using Heyday.Shared.Schedule;
using Heyday.Domain.Entities;

namespace Heyday.Application.Catalog.Services
{
    public class SchedueService : IScheduleService
    {
        private readonly IRepositoryAsync _repository;

        public SchedueService(IRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> CreateScheduleAsync(CreateScheduleRequest request)
        {
            var schedule = new Schedule();

            schedule.title = request.Title;
            schedule.period = request.Period;
            schedule.manager_id = request.ManagerId;

            var brandId = await _repository.CreateAsync<Schedule>(schedule);

            await _repository.SaveChangesAsync();
            return await Result<Guid>.SuccessAsync(brandId);
        }
    }
}
