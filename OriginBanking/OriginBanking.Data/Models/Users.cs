using System;
using System.Collections.Generic;

namespace OriginBanking.Data.Models
{
    public partial class Users
    {
        public Users()
        {
            Cards = new HashSet<Cards>();
            Logs = new HashSet<Logs>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CardId { get; set; }

        public virtual ICollection<Cards> Cards { get; set; }
        public virtual ICollection<Logs> Logs { get; set; }
    }
}
