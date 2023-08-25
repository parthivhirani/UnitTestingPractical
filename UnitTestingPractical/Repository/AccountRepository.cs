using UnitTestingPractical.Models;

namespace UnitTestingPractical.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankDetails _bankDetails;

        public AccountRepository(BankDetails bankDetails)
        {
            _bankDetails = bankDetails;
        }

        public double GetBalance(int id)
        {
            return _bankDetails.GetBankDetails().Where(c => c.Id == id).Select(c => c.Amount).SingleOrDefault();
        }

        public double WithdrawAmount(int id, double amount)
        {
            var customer = _bankDetails.GetBankDetails().Where(c => c.Id == id).SingleOrDefault();
            if(customer != null && amount < customer.Amount)
            {
                customer.Amount -= amount;
                return customer.Amount;
            }
            return 0;
        }


        public double CreditAmount(int id, double amount)
        {
            var customer = _bankDetails.GetBankDetails().Where(c => c.Id == id).SingleOrDefault();
            if (customer != null)
            {
                customer.Amount += amount;
                return customer.Amount;
            }
            return 0;
        }

        public double FundTransfer(int senderId, int recipientId, double amount)
        {
            double output = 0;
            var senderObj = _bankDetails.GetBankDetails().Where(c => c.Id == senderId).SingleOrDefault();
            if (senderObj != null)
            {
                if(amount < senderObj.Amount)
                {
                    var recipientObj = _bankDetails.GetBankDetails().Where(c => c.Id == recipientId).SingleOrDefault();
                    if (recipientObj != null)
                    {
                        senderObj.Amount -= amount;
                        recipientObj.Amount += amount;
                        return senderObj.Amount;
                    }
                    return output;
                }
                output = -1;
                return output;
            }
            return output;
        }
    }
}
