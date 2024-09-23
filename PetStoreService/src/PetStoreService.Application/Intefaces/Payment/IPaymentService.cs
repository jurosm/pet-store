namespace PetStoreService.Application.Interfaces.Payment;

public interface IPaymentService
{
    Task<PaymentIntentResponse> CreatePaymentIntent(PaymentIntentRequest request);
}

public class PaymentIntentRequest
{
    public decimal Amount { get; set; }
    // For now Dollars are used for currency
    public string Currency { get; set; } = "usd";
}

public class PaymentIntentResponse
{
    public string ClientSecret { get; set; }
    public string PaymentIntentId { get; set; }
}