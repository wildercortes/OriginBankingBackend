using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OriginBanking.Data.DTOs;
using OriginBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OriginBanking.Data.Repositories.RepositoryBalance
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly OriginBankingContext db;

        public BalanceRepository(OriginBankingContext db)
        {
            this.db = db;
        }

        public bool OverPassBalance(CardDTO model)
        {
            var Balance = db.Cards.Where(x => x.Number == model.number && x.IsBlocked == false).Select(x => x.Balance).FirstOrDefault();

            if (model.monto > Balance)
                return true;

            return false;
        }

        public bool GetMoney(CardDTO model)
        {
            using (IDbContextTransaction transaction = this.db.Database.BeginTransaction())
            {
                try
                {
                    var Card = db.Cards.Where(x => x.Number == model.number && x.IsBlocked == false).FirstOrDefault();
                    Card.Balance = Card.Balance - model.monto;
                    db.SaveChanges();

                    var OperationId = db.Operations.Where(x => x.Description == "Retiro").Select(x => x.OperationId).FirstOrDefault();

                    db.Logs.Add(new Logs { UserId = Card.UserId, Date = DateTime.Now, OperationId = OperationId, Quantity = model.monto, Cardnumber = model.number });
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }


            }

        }

        public List<BalanceDTO> GetBalance(string cardnumber)
        {
            var UserId = db.Cards.Where(x => x.Number == cardnumber && x.IsBlocked == false).Select(x => x.UserId).FirstOrDefault();

            var Operations = db.Operations.ToList();

            try
            {
                var OperationId = Operations.Where(x => x.Description == "Balance").Select(x => x.OperationId).FirstOrDefault();

                db.Logs.Add(new Logs { UserId = UserId, Date = DateTime.Now, OperationId = OperationId, Cardnumber = cardnumber });

                db.SaveChanges();
                OperationId = Operations.Where(x => x.Description == "Retiro").Select(x => x.OperationId).FirstOrDefault();
                return db.Logs.Where(x => x.UserId == UserId && x.OperationId == OperationId).Include(c => c.Operation).Include(c => c.User).ToList().Select(x => new BalanceDTO(x)).ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
