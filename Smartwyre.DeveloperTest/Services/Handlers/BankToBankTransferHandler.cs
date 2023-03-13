using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Handlers
{
    internal class BankToBankTransferHandler : PaymentSchemeHandler
    {
        public override PaymentScheme PaymentScheme => PaymentScheme.BankToBankTransfer;

        public override bool IsValid(MakePaymentRequest request, Account account)
        {
            if (account == null)
            {
                return false;
            }
                        
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer))
            {
                return false;
            }

            // Without further information, assuming "true" was an ommission, would pose question back to BA
            return true;
        }
    }
}