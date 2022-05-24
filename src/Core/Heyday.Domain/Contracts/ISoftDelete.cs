using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Domain.Contracts
{
    public interface ISoftDelete
    {
        public Guid? deleted_by { get; set; }
        public DateTime? deleted_at { get; set; }
    }
}
