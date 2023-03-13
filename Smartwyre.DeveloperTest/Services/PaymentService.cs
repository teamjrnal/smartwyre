using Smartwyre.DeveloperTest.Abstractions;
using Smartwyre.DeveloperTest.Exceptions;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IRequestValidatorService _requestValidatorService;

        public PaymentService(IAccountDataStore accountDataStore,
            IRequestValidatorService requestValidatorService)
        {
            _accountDataStore = accountDataStore;
            _requestValidatorService = requestValidatorService;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

            if (account == null)
            {
                // Either return false, which could be misleading
                //return new MakePaymentResult
                //{
                //    Success = false
                //};

                // Or throw exception, which could "break" downstream services
                throw new NoAccountException("Account xxx does not exist");
            }

            var canMakePayments = _requestValidatorService.IsValidForPayments(request, account);

            if (canMakePayments)
            {
                account.Balance -= request.Amount;

                _accountDataStore.UpdateAccount(account);
            }

            return new MakePaymentResult
            {
                Success = canMakePayments
            };
        }
    }
}
