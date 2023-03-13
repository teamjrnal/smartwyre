using Smartwyre.DeveloperTest.Services.Handlers;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Services
{
    public interface IRequestValidatorService
    {
        bool IsValidForPayments(MakePaymentRequest request, Account account);
    }

    public class RequestValidatorService : IRequestValidatorService
    {
        private Dictionary<PaymentScheme, PaymentSchemeHandler> _handlers;

        public RequestValidatorService()
        {
            // Could theoretically pick these up via reflection should the app be efficient and scoped enough
            _handlers = new List<PaymentSchemeHandler>()
            {
                new ExpeditedPaymentsHandler(),
                new BankToBankTransferHandler(),
                new AutomatedPaymentSystemHandler()
            }.ToDictionary(x => x.PaymentScheme, x => x);
        }

        public bool IsValidForPayments(MakePaymentRequest request, Account account)
        {
            var handler = _handlers[request.PaymentScheme];

            if (handler == null)
            {
                // Again, various things we could do here, but let's assume an exception
                throw new KeyNotFoundException(); // Probably a custom exception here instead of this.
            }

            return handler.IsValid(request, account);
        }
    }
}