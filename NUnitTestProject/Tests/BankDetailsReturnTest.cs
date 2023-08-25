namespace NUnitTestProject.Tests;

[TestFixture]
public class BankDetailsReturnTest
{
    private readonly UserController _userController;
    private readonly Mock<ICustomerRepository> _mock = new Mock<ICustomerRepository>();

    public BankDetailsReturnTest()
    {
        _userController = new UserController(_mock.Object);
    }

    [Test]
    [TestCase(1)]
    [TestCase(3)]
    public void Should_Return_ValidCustomer_When_inputIsValid(int id)
    {
        // ARRANGE
        var expectedObject = new BankDetails()
        {
            Id = id
        };
        _mock.Setup(c => c.GetCustomer(id)).Returns(expectedObject);

        // ACT
        var actual = _userController.GetCustomer(id) as OkObjectResult;

        var actualData = actual.Value;
        var jsonResult = JsonConvert.SerializeObject(actualData);
        var actualObject = JsonConvert.DeserializeObject<BankDetails>(jsonResult);

        // ASSERT
        Assert.IsNotNull(actual);
        Assert.That(actualObject.Id, Is.EqualTo(expectedObject.Id));
    }

    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    public void Should_Return_BadRequest_When_inputIsNotValid(int id)
    {
        // ARRANGE
        var expectedStatusCode = HttpStatusCode.BadRequest;
        _mock.Setup(c => c.GetCustomer(It.IsAny<int>())).Returns(() => null);

        // ACT
        var actual = _userController.GetCustomer(id) as BadRequestResult;

        var actualStatusCode = (HttpStatusCode)actual.StatusCode;

        // ASSERT
        Assert.True(actualStatusCode.Equals(expectedStatusCode));
    }

    [Test]
    [TestCase(10)]
    public void Should_Return_NotFound_When_objectNotFoundWithValidInput(int id)
    {
        // ARRANGE
        var expectedStatusCode = HttpStatusCode.NotFound;
        _mock.Setup(c => c.GetCustomer(It.IsAny<int>())).Returns(() => null);

        // ACT
        var actual = _userController.GetCustomer(id) as NotFoundResult;

        var actualStatusCode = (HttpStatusCode)actual.StatusCode;

        // ASSERT
        Assert.True(actualStatusCode.Equals(expectedStatusCode));
    }
}
