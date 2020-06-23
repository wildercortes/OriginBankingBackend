using OriginBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OriginBanking.Data.DTOs
{
    public class UserDTO
    {
        public string firstname { get; set; }
        public string lastname { get; set; }

        public double balance { get; set; }

        public DateTime endda { get; set; }


        public UserDTO(Cards obj)
        {
            endda = obj.Endda;
            balance = obj.Balance;
            lastname = obj.User.LastName;
            firstname = obj.User.FirstName;

        }

    }
}
