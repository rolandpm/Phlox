using Square;
using Square.Exceptions;
using Square.Models;
using static Android.Graphics.ImageDecoder;

namespace Phlox.Pages
{
    public partial class CheckoutPage : ContentPage
    {
        private SquareClient Client;
        private Square.Environment SqEnv = Square.Environment.Sandbox;
        private string clientAccessToken;
        private string Source;

        public CheckoutPage()
        {
            InitializeComponent();

            SqEnv = Square.Environment.Sandbox;
            clientAccessToken = "EAAAEM_ZfQRei27WO2zR_V8loSNuntgz_prw8RJ6dTYp93ME3L_FSC3EMhm4bbRb";

            Client = new SquareClient.Builder()
                .Environment(SqEnv)
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
                var result = Client.CheckoutApi.CreatePaymentLink(body: body);
                this.Source = result.PaymentLink.Url;

                /*this.webView = new WebView
                {
                    Source = this.Source
                };

                this.webView.Navigating += WebView_Navigating;

                content.Content = this.webView;*/
                Browser.Default.OpenAsync(this.Source, BrowserLaunchMode.SystemPreferred);
            }
            catch (ApiException ex)
            {
                Console.WriteLine("Failed to make the request");
                Console.WriteLine($"Response Code: {ex.ResponseCode}");
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            /*UrlWebViewSource source = e.Source as UrlWebViewSource;
            if (!source.Url.StartsWith("https://checkout.square.site"))
            {
                e.Cancel = true;
                Browser.Default.OpenAsync(source.Url, BrowserLaunchMode.SystemPreferred);
            }*/
        }
    }
}