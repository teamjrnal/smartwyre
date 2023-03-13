using FluentAssertions;
using Smartwyre.DeveloperTest.Services.Handlers;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.HandlerTests
{
    public class BankToBankTransferHandlerTests
    {
        private readonly BankToBankTransferHandler _sut;

        public BankToBankTransferHandlerTests()
        {
            _sut = new BankToBankTransferHandler();
        }

        [Fact]
        public void AccountNullFalse()
        {
            // Act
            var res = _sut.IsValid(new MakePaymentRequest(), null);

            // Assert

            res.Should().BeFalse();
        }

        [Fact]
        public void AllowedTrue()
        {
            // Act
            var res = _sut.IsValid(new MakePaymentRequest(), new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments | AllowedPaymentSchemes.BankToBankTransfer | AllowedPaymentSchemes.AutomatedPaymentSystem
            });

            // Assert

            res.Should().BeTrue();
        }

        [Fact]
        public void NotAllowedFalse()
        {
            // Act
            var res = _sut.IsValid(new MakePaymentRequest(), new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments | AllowedPaymentSchemes.AutomatedPaymentSystem
            });

            // Assert

            res.Should().BeFalse();
        }
    }
}