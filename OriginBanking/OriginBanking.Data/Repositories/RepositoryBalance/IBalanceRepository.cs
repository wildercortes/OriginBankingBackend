using System.Collections.Generic;
using OriginBanking.Data.DTOs;

namespace OriginBanking.Data.Repositories.RepositoryBalance
{
    public interface IBalanceRepository
    {
        List<BalanceDTO> GetBalance(string cardnumber);
        bool GetMoney(CardDTO model);
        bool OverPassBalance(CardDTO model);
    }
}