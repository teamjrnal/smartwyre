using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Handlers
{
    internal class AutomatedPaymentSystemHandler : PaymentSchemeHandler
    {
        public override PaymentScheme PaymentScheme => PaymentScheme.AutomatedPaymentSystem;

        public override bool IsValid(MakePaymentRequest request, Account account)
        {
            if (account == null)
            {
                return false;
            }
            
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem))
            {
                return false;
            }
            
            if (account.Status != AccountStatus.Live)
            {
                return false;
            }

            // Without further information, assuming "true" was an ommission, would pose question back to BA
            return true;
        }
    }
}