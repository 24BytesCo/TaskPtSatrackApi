using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Categories.Queries.Vms;

namespace Tareas.Application.Features.Tasks.Queries.Vms
{
    public class TaskVm
    {
        public Guid TaskId { get; set; }
        
        public string? Title { get; set; }
        
        public string? Description { get; set; }
        
        public bool Status { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public ICollection<CategoryVm>? Categories { get; set; }
    }
}
