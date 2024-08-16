using Stripe;
using System.Threading.Tasks;

namespace PetStore.API.Services.ExternalServices
{
    public class StripeService
    {
        public string CreateCharge(string tokenId, long amount)
        {
            var options = new ChargeCreateOptions
            {
                Source = tokenId,
                Currency = "USD",
                Amount = amount * 100
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            return charge.Id;
        }

        public async Task<string> GetStatusAsync(string chargeId)
        {
            var service = new ChargeService();
            return (await service.GetAsync(chargeId)).Status;
        }
    }
}
