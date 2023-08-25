using UnitTestingPractical.Models;

namespace UnitTestingPractical.Repository
{
    public interface ICustomerRepository
    {
        BankDetails AddNewCustomer(BankDetails bankDetails);
        BankDetails GetCustomer(int id);
        List<BankDetails> GetAllCustomers();
    }
}
