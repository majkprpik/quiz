using System.ComponentModel.DataAnnotations;

namespace quiz.Entities
{
    public abstract class AuditableBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public int? UserCreatedId { get; set; }
        public int? UserModifiedId { get; set; }
    }
}
