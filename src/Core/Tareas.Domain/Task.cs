using System.ComponentModel.DataAnnotations.Schema;
using Tareas.Domain.Common;

namespace Tareas.Domain
{
    public class Task : BaseDomainModel
    {
        [Column(TypeName =  "NVARCHAR(100)")]
        public string? Title { get; set; }

        [Column(TypeName = "NVARCHAR(500)")]
        public string? Description { get; set; }

        public DateTime Deadline { get; set; }

        public Category? Category { get; set; }
        
        public Guid CategoryId { get; set; }
    }
}


