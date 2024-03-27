using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ServicePosition
    {
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}
