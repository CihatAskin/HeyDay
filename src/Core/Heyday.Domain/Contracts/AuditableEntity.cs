using System.ComponentModel.DataAnnotations.Schema;

namespace Heyday.Domain.Contracts;
public abstract class AuditableEntity : BaseEntity, IAuditableEntity, ISoftDelete
{
    public Guid created_by { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime created_at { get; set; }
    public Guid? updated_by { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? updated_at { get; set; }
    public Guid? deleted_by { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? deleted_at { get; set; }

    protected AuditableEntity()
    {
        created_at = DateTime.UtcNow;
        updated_at = DateTime.UtcNow;
    }
}

