using UnitTestingPractical.Models;

namespace UnitTestingPractical.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankDetails _bankDetails;

        public CustomerRepository(BankDetails bankDetails)
        {
            _bankDetails = bankDetails;
        }

        public BankDetails AddNewCustomer(BankDetails bankDetails)
        {
            if (bankDetails != null)
            {
                var existing = _bankDetails.GetBankDetails().Where(c => c.Id == bankDetails.Id).SingleOrDefault();
                if (existing == null && bankDetails.Amount > 0)
                {
                    _bankDetails.GetBankDetails().Add(bankDetails);
                    return bankDetails;
                }
                return null;
            }
            return null;
        }

        public BankDetails GetCustomer(int id)
        {
            var customer = _bankDetails.GetBankDetails().Where(c => c.Id == id).SingleOrDefault();
            return customer;
        }

        public List<BankDetails> GetAllCustomers()
        {
            return _bankDetails.GetBankDetails().ToList();
        }
    }
}
