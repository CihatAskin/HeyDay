using Heyday.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string name { get; set; }

        public IList<UserSchedule> user_schedule { get; set; }=new List<UserSchedule>();

        public IList<Schedule>? schedules { get; set; }
    }
}
