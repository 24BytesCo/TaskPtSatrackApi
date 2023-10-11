using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas.Domain.Common
{
    // Esta clase servirá como una plantilla común para otras clases relacionadas con el dominio de tareas.
    public abstract class BaseDomainModel
    {
        public Guid Id { get; set; }
     
        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public bool Status { get; set; } = true;
    }
}
