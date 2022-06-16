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
            for (int i = 0; i < request.UserIds.Count; i++)
            {
                var userExists = await _repository.ExistsAsync<User>(a => a.id == request.UserIds[i]);
                if (!userExists)
                {
                    // uuid değil string alınmalı email ise yeni kullanıcı demektir databeden bu emailde bi kullancı var mı
                    // kontrol et varsa arkadaş olarak bağla kullanıcı yoksa user_236838 adı ile eklenmeli 
                }
            }

            var schedule = new Schedule();

            schedule.title = request.Title;
            schedule.description = request.Description;
            schedule.period = request.Period;
            schedule.start_date = request.StartDate;
            schedule.manager_id = request.ManagerId;

            schedule.user_schedule.Add(
                new UserSchedule()
                {
                    user_id = request.ManagerId,
                    suitable_hour_keys = request.HourKeys

                });

            var brandId = await _repository.CreateAsync<Schedule>(schedule);

            await _repository.SaveChangesAsync();
            return await Result<Guid>.SuccessAsync(brandId);
        }
    }
}
