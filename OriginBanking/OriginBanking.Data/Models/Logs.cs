using System;
using System.Collections.Generic;

namespace OriginBanking.Data.Models
{
    public partial class Logs
    {
        public int LogId { get; set; }
        public DateTime Date { get; set; }
        public string Cardnumber { get; set; }
        public int OperationId { get; set; }
        public int UserId { get; set; }
        public double Quantity { get; set; }

        public virtual Operations Operation { get; set; }
        public virtual Users User { get; set; }
    }
}
