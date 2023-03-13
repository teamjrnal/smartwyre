using FluentAssertions;
using Moq;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class RequestValidatorServiceTests
    {
        private readonly IRequestValidatorService _sut;

        public RequestValidatorServiceTests()
        {
            _sut = new RequestValidatorService();
        }

        [Fact]
        public void AllPaymentSchemesHaveHandlers()
        {
            // This could be done more sensibly with some property based testing
            foreach (PaymentScheme schemeToTest in Enum.GetValues(typeof(PaymentScheme)))
            {
                _sut.Invoking(x => x.IsValidForPayments(new MakePaymentRequest { PaymentScheme = schemeToTest }, It.IsAny<Account>()))
                    .Should().NotThrow();
            }
        }
    }
}