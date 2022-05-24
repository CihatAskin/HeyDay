using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Domain.Contracts
{
    public interface IAuditableEntity
    {
        public Guid created_by { get; set; }
        public DateTime created_at { get; set; }

        public Guid? updated_by { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
