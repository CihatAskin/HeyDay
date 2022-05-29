using Heyday.Application.Common.Contracts;
using Heyday.Application.Wrapper;
using Heyday.Shared.Schedule;

namespace Heyday.Application.Catalog.Contracts;

public interface IScheduleService : ITransientService
{

    Task<Result<Guid>> CreateScheduleAsync(CreateScheduleRequest request);

}

