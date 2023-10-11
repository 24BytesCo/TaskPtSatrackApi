using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Domain.Common;

namespace Tareas.Domain
{
    public class Category : BaseDomainModel
    {
        [Column(TypeName = "NVARCHAR(50)")]

        public string? Name { get; set; }


        public virtual ICollection<ScheduledTask>? ScheduledTasks { get; set;}
    }
}
