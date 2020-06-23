using System;
using System.Collections.Generic;

namespace OriginBanking.Data.Models
{
    public partial class Cards
    {
        public int CardId { get; set; }
        public string Number { get; set; }
        public bool IsBlocked { get; set; }
        public int UserId { get; set; }
        public DateTime Begda { get; set; }
        public DateTime Endda { get; set; }
        public string Pin { get; set; }
        public double Balance { get; set; }

        public virtual Users User { get; set; }
    }
}
