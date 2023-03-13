using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Handlers
{
    internal abstract class PaymentSchemeHandler
    {
        public abstract PaymentScheme PaymentScheme { get; }

        public abstract bool IsValid(MakePaymentRequest request, Account account);
    }
}