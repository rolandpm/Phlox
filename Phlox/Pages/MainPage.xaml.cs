using Square;
using Square.Exceptions;
using Square.Models;
using Square.Authentication;
using System.Reflection.Metadata.Ecma335;
using Phlox.Pages;

namespace Phlox;

public partial class MainPage : ContentPage
{
	int count = 0;
    private SquareClient Client;
    private Square.Environment SqEnv = Square.Environment.Sandbox;
    private string clientAccessToken;
    private string Source;

    public MainPage()
	{
		InitializeComponent();

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

        var result = Client.CheckoutApi.CreatePaymentLink(body: body);
        this.Source = result.PaymentLink.Url;
    }

	private async void BrowserOpen_Clicked(object sender, EventArgs e)
	{
        //Testing a payment
        try
        {
            Uri uri = new Uri(this.Source);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // An unexpected error occurred. No browser may be installed on the device.
        }
        /*var amountMoney = new Money.Builder()
            .Amount(1500L)
            .Currency("USD")
            .Build();

        var body = new CreatePaymentRequest.Builder(
            sourceId: "cnon:card-nonce-ok",
            idempotencyKey: Guid.NewGuid().ToString(),
            amountMoney: amountMoney)
            .Build();

        try
        {
            var result = await client.PaymentsApi.CreatePaymentAsync(body: body);
        }
        catch (ApiException ex)
        {
            Console.WriteLine("Failed to make the request");
            Console.WriteLine($"Response Code: {ex.ResponseCode}");
            Console.WriteLine($"Exception: {ex.Message}");
        }*/

        /*count++;

		if (count == 1)
			CounterBtn.Text = $"Made $15 payment {count} time";
		else
			CounterBtn.Text = $"Made $15 payment {count} times";*/

        //SemanticScreenReader.Announce(CounterBtn.Text);
    }
}

