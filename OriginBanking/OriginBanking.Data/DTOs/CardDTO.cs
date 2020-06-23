using System;
using System.Collections.Generic;
using System.Text;

namespace OriginBanking.Data.DTOs
{
    public class CardDTO
    {
        public string card { get; set; }
        public string number { get; set; }
        public string pin { get; set; }
        public double monto { get; set; }
        public int Attempts { get; set; }

        public DateTime date { get; set; }

        public double quantity { get; set; }
    }
}
