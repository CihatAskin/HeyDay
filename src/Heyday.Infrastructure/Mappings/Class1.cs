using Heyday.Domain.Entities;
using Heyday.Shared.Schedule;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Infrastructure.Mappings;

public class MapsterSettings
{
    public static void Configure()
    {
        // here we will define the type conversion / Custom-mapping
        // More details at https://github.com/MapsterMapper/Mapster/wiki/Custom-mapping
        TypeAdapterConfig<Schedule, ScheduleSearchDto>.NewConfig()
            .Map(dest => dest.Id, src => src.id)
            .Map(dest => dest.Title, src => src.title)
            .Map(dest => dest.Period, src => src.period)
            .Map(dest => dest.IsExecuted, src => src.is_executed);
    }
}
