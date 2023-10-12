using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas.Application.Features.Categories.Queries.Vms
{
    public class CategoryVm
    {
        public Guid CategoryId { get; set; }
        
        public string? Name { get; set; }
        
        public bool Status { get; set; }
    }
}
