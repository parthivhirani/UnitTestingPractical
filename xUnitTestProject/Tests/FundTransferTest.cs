namespace xUnitTestProject.Tests;

public class FundTransferTest
{
    private readonly AccountController _accountController;
    private readonly Mock<IAccountRepository> _mock = new Mock<IAccountRepository>();

    public FundTransferTest()
    {
        _accountController = new AccountController(_mock.Object);
    }

    [Theory]
    [InlineData(2, 1, 2000)]
    public void Return_Updated_Amount_When_fundTransferedSuccessfully(int senderId, int recipientId, double amount)
    {
        // ARRANGE
        double expectedBalance = 38000;
        _mock.Setup(service => service.FundTransfer(senderId, recipientId, amount))
                           .Returns(expectedBalance);

        // ACT
        var result = _accountController.TransferAmount(senderId, recipientId, amount) as OkObjectResult;
        var actualBalance = result.Value;

        // ASSERT
        Assert.True(expectedBalance.Equals(actualBalance));
    }

    [Theory]
    [InlineData(-1, 2, 3000)]
    [InlineData(1, -2, 2000)]
    [InlineData(1, 2, 60000)]
    [InlineData(1, 1, 2500)]
    public void Return_BadRequest_When_invalidDataPassed(int senderId, int recipientId, double amount)
    {
        // ARRANGE
        var expectedStatusCode = HttpStatusCode.BadRequest;
        _mock.Setup(service => service.FundTransfer(senderId, recipientId, amount))
                           .Returns(-1);

        // ACT
        var result = _accountController.TransferAmount(senderId, recipientId, amount) as BadRequestResult;
        var actualStatusCode = (HttpStatusCode)result.StatusCode;

        // ASSERT
        Assert.True(expectedStatusCode.Equals(actualStatusCode));
    }


    [Theory]
    [InlineData(10, 2, 3000)]
    [InlineData(1, 15, 2000)]
    [InlineData(6, 17, 5000)]
    public void Return_NotFound_When_useNotFound(int senderId, int recipientId, double amount)
    {
        // ARRANGE
        var expectedStatusCode = HttpStatusCode.NotFound;
        _mock.Setup(service => service.FundTransfer(senderId, recipientId, amount))
                           .Returns(0);

        // ACT
        var result = _accountController.TransferAmount(senderId, recipientId, amount) as NotFoundResult;
        var actualStatusCode = (HttpStatusCode)result.StatusCode;

        // ASSERT
        Assert.True(expectedStatusCode.Equals(actualStatusCode));
    }
}
