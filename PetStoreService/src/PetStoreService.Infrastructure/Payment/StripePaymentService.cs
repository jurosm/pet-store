using PetStoreService.Application.Interfaces.Payment;
using Stripe;

namespace PetStoreService.Infrastructure.Payment;

public class StripePaymentService : IPaymentService
{
    public StripePaymentService()
    {
        StripeConfiguration.ApiKey = StripeConfig.ApiKey;
    }

    public async Task<PaymentIntentResponse> CreatePaymentIntent(PaymentIntentRequest request)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)Math.Round(request.Amount, 2) * 100,
            Currency = request.Currency,
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
            },
        };
        var service = new PaymentIntentService();
        var paymentIntent = await service.CreateAsync(options);

        return new PaymentIntentResponse
        {
            ClientSecret = paymentIntent.ClientSecret,
            PaymentIntentId = paymentIntent.Id
        };
    }
}

public class StripeConfig
{
    public static string ApiKey { get; set; } = Environment.GetEnvironmentVariable("STRIPE_API_KEY");
}
