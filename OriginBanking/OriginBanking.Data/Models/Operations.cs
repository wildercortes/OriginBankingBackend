using System;
using System.Collections.Generic;

namespace OriginBanking.Data.Models
{
    public partial class Operations
    {
        public Operations()
        {
            Logs = new HashSet<Logs>();
        }

        public int OperationId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Logs> Logs { get; set; }
    }
}
