using Moq;
using UnitTestingPractical.Repository;

namespace NUnitTestProject.Tests;

[TestFixture]
public class AmountCreditTest
{
    private readonly AccountController _accountController;
    private readonly Mock<IAccountRepository> _mock = new Mock<IAccountRepository>();

    public AmountCreditTest()
    {
        _accountController = new AccountController(_mock.Object);
    }
    [Test]
    [TestCase(1, 1000)]
    public void testAmountCreditSuccess(int id, double amount)
    {
        // ARRANGE
        var expectedAmount = 51000.00;
        _mock.Setup(c => c.CreditAmount(id, amount)).Returns(51000.00);

        // ACT
        var actual = _accountController.Credit(id, amount) as OkObjectResult;
        var actualAmount = actual.Value;

        // ASSERT
        Assert.True(expectedAmount.Equals(actualAmount));
    }


    [Test]
    [TestCase(10, 1000)]
    public void Should_Return_NotFound_When_accountNotFoundwithValidInput(int id, double amount)
    {
        // ARRANGE
        var expectedAmount = 0.00;
        var expectedStatusCode = HttpStatusCode.BadRequest;
        _mock.Setup(c => c.CreditAmount(id, amount)).Returns(0.00);

        // ACT
        var actual = _accountController.Credit(id, amount) as BadRequestResult;
        var actualStatusCode = (HttpStatusCode)actual.StatusCode;

        // ASSERT
        Assert.True(expectedStatusCode.Equals(actualStatusCode));
    }


    [Test]
    [TestCase(-1, 2000)]
    [TestCase(1, 50)]
    [TestCase(1, 60000)]
    public void Should_Return_BadRequest_When_inputsAreNotvalid(int id, double amount)
    {
        // ARRANGE
        var expectedAmount = 0.00;
        var expectedStatusCode = HttpStatusCode.BadRequest;
        _mock.Setup(c => c.CreditAmount(id, amount)).Returns(0.00);

        // ACT
        var actual = _accountController.Credit(id, amount) as BadRequestResult;
        var actualStatusCode = (HttpStatusCode)actual.StatusCode;

        // ASSERT
        Assert.True(expectedStatusCode.Equals(actualStatusCode));
    }
}
