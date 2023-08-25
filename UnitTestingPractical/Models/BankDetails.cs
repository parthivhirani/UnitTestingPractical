namespace UnitTestingPractical.Models
{
    public class BankDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Amount { get; set; }

        public IList<BankDetails> GetBankDetails()
        {
            var details = new List<BankDetails>()
            {
                new BankDetails {  Id = 1, FirstName = "Parthiv", LastName = "Hirani", Amount = 50000 },
                new BankDetails {  Id = 2, FirstName = "Meet", LastName = "Patel", Amount = 40000 },
                new BankDetails {  Id = 3, FirstName = "Abhi", LastName = "Dashadiya", Amount = 60000 }
            };

            return details;
        }
    }
}
