using Square;
using Square.Exceptions;
using Square.Models;

namespace Phlox.Pages
{
    public partial class CheckoutPage : ContentPage
    {
        private SquareClient client;
        private Square.Environment sqEnv = Square.Environment.Sandbox;
        private string clientAccessToken;

        public CheckoutPage()
        {
            InitializeComponent();

            sqEnv = Square.Environment.Sandbox;
            clientAccessToken = "EAAAEM_ZfQRei27WO2zR_V8loSNuntgz_prw8RJ6dTYp93ME3L_FSC3EMhm4bbRb";

            client = new SquareClient.Builder()
                .Environment(sqEnv)
                .AccessToken(clientAccessToken)
                .Build();

            var priceMoney = new Money.Builder()
              .Amount(1000L)
              .Currency("USD")
              .Build();

            var quickPay = new QuickPay.Builder(
                name: "Auto Detailing",
                priceMoney: priceMoney,
                locationId: "L4P1949RZ3WFT")
                .Build();

            var body = new CreatePaymentLinkRequest.Builder()
                .IdempotencyKey(Guid.NewGuid().ToString())
                .QuickPay(quickPay)
                .Build();

            try
            {
                var result = client.CheckoutApi.CreatePaymentLink(body: body);

                WebView webvView = new WebView
                {
                    Source = result.PaymentLink.Url,
                };

                content.Content = webvView;
                webvView.Reload();
            }
            catch (ApiException ex)
            {
                Console.WriteLine("Failed to make the request");
                Console.WriteLine($"Response Code: {ex.ResponseCode}");
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}