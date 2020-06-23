using OriginBanking.Data.DTOs;

namespace OriginBanking.Data.Repositories.RepositoryCard
{
    public interface ICardRepository
    {
        void BlockCard(CardDTO model);
        bool ExistAndIsNotBlocked(CardDTO model);
        UserDTO GetUser(string cardnumber);
        bool PinIsCorrect(CardDTO model);
    }
}