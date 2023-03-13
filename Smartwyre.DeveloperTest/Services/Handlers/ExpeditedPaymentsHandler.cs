using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Handlers
{
    internal class ExpeditedPaymentsHandler : PaymentSchemeHandler
    {
        public override PaymentScheme PaymentScheme => PaymentScheme.ExpeditedPayments;

        public override bool IsValid(MakePaymentRequest request, Account account)
        {
            if (account == null)
            {
                return false;
            }
            
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments))
            {
                return false;
            }
            
            if (IsBalanceLessThanAmount(request, account))
            {
                return false;
            }

            // Without further information, assuming "true" was an ommission, would pose question back to BA
            return true;
        }

        // Doing this for testability - could probably be in generic helper class
        internal static bool IsBalanceLessThanAmount(MakePaymentRequest request, Account account)
         => account.Balance < request.Amount;
    }
}