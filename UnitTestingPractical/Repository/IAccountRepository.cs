using UnitTestingPractical.Models;

namespace UnitTestingPractical.Repository
{
    public interface IAccountRepository
    {
        double GetBalance(int id);
        double WithdrawAmount(int id, double amount);
        double CreditAmount(int id, double amount);
        double FundTransfer(int senderId, int recipientId, double amount);
    }
}
