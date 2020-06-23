using Microsoft.EntityFrameworkCore;
using OriginBanking.Data.DTOs;
using OriginBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OriginBanking.Data.Repositories.RepositoryCard
{
    public class CardRepository : ICardRepository
    { 
        private readonly OriginBankingContext db;

        public CardRepository(OriginBankingContext db)
        {
            this.db = db;
        }

        public bool ExistAndIsNotBlocked(CardDTO model)
        {
            var Card = db.Cards.Where(x => x.Number == model.number && x.IsBlocked == false).FirstOrDefault();

            if (Card == null)
                return false;

            return true;
        }

        public bool PinIsCorrect(CardDTO model)
        {
            var Card = db.Cards.Where(x => x.Number == model.number && x.IsBlocked == false && x.Pin == model.pin).FirstOrDefault();

            if (Card == null)
                return false;

            return true;
        }

        public void BlockCard(CardDTO model)
        {
            var Card = db.Cards.Where(x => x.Number == model.number && x.IsBlocked == false).FirstOrDefault();

            if (Card != null)
            {
                try
                {
                    Card.IsBlocked = true;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public UserDTO GetUser(string cardnumber)
        {
            var data = db.Cards.Where(x => x.Number == cardnumber && x.IsBlocked == false).Include(c => c.User).FirstOrDefault();
            return new UserDTO(data);

        }

    }
}
