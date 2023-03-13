using FluentAssertions;
using Moq;
using Smartwyre.DeveloperTest.Abstractions;
using Smartwyre.DeveloperTest.Exceptions;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        private readonly IPaymentService _sut;
        private readonly Mock<IAccountDataStore> _mockAccountDataStore = new();
        private readonly Mock<IRequestValidatorService> _mockValidatorService = new();

        public PaymentServiceTests()
        {
            _sut = new PaymentService(_mockAccountDataStore.Object, 
                _mockValidatorService.Object);
        }

        // Sorry about the awkward gherkin notation. Skipping it in places for speed
        [Fact]
        public void Given_ImMakingAPayment_When_ItsAllowed_Then_BalanceShouldBeAdjusted()
        {
            // Arrange

            _mockAccountDataStore.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns(new Account
                {
                    Balance = 100
                });

            _mockValidatorService.Setup(x => x.IsValidForPayments(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()))
                .Returns(true);

            // Act

            var res = _sut.MakePayment(new MakePaymentRequest
            {
                Amount = 25
            });

            // Assert

            res.Success.Should().BeTrue();

            _mockAccountDataStore.Verify(x => x.UpdateAccount(It.Is<Account>(acc => acc.Balance == 75)), Times.Once);
        }

        [Fact]
        public void SimpleNullTest()
        {
            // Arrange

            _mockAccountDataStore.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns((Account)null);

            // Act & Assert

            _sut.Invoking(x => x.MakePayment(new MakePaymentRequest
            {
                DebtorAccountNumber = "123"
            }))
                .Should()
                .Throw<NoAccountException>();
        }

        [Fact]
        public void Given_ImMakingAPayment_When_ItsNotAllowed_Then_BalanceShouldBeAdjusted()
        {
            // Arrange

            _mockAccountDataStore.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns(new Account
                {
                    Balance = 100
                });

            _mockValidatorService.Setup(x => x.IsValidForPayments(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()))
                .Returns(false);

            // Act

            var res = _sut.MakePayment(new MakePaymentRequest
            {
                Amount = 25
            });

            // Assert

            res.Success.Should().BeFalse();

            _mockAccountDataStore.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Never);
        }
    }
}
