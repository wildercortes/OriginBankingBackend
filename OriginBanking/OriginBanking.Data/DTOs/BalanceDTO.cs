using OriginBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OriginBanking.Data.DTOs
{
    public class BalanceDTO
    {
        public DateTime date { get; set; }
        public string number { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public double quantity { get; set; }

        public string operacion { get; set; }

        public BalanceDTO(Logs obj)
        {
            date = obj.Date;
            number = obj.Cardnumber;
            quantity = obj.Quantity;
            operacion = obj.Operation.Description;
            firstname = obj.User.FirstName;
            lastname = obj.User.LastName;
        }

    }
}
