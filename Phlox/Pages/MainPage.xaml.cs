using Square;
using Square.Exceptions;
using Square.Models;
using Square.Authentication;
using System.Reflection.Metadata.Ecma335;
using Phlox.Pages;

namespace Phlox;

public partial class MainPage : ContentPage
{
	int Count = 0;
    private SquareClient Client;
    private Square.Environment SqEnv;

    private string ClientAccessToken;
    private string Source;

    private DeviceCode DeviceCode;

    public MainPage()
	{
		InitializeComponent();

        SqEnv = Square.Environment.Sandbox;

        // Sandbox
        ClientAccessToken = "EAAAEM_ZfQRei27WO2zR_V8loSNuntgz_prw8RJ6dTYp93ME3L_FSC3EMhm4bbRb";
        // Production
        //ClientAccessToken = "EAAAEarl0xq8aFAr_KZEVzEnRY9NjX2w7U5foBFzJZTgTDpCecY6ueE7N_VFSbiX";

        Client = new SquareClient.Builder()
            .Environment(SqEnv)
            .AccessToken(ClientAccessToken)
            .Build();

        DeviceCode = new DeviceCode.Builder(productType: "TERMINAL_API")
            .Name("MoneyMaker")
            .LocationId("L4P1949RZ3WFT")
            .Build();
    }

	private async void BrowserOpen_Clicked(object sender, EventArgs e)
	{
        //Testing a request
        var body = new CreateDeviceCodeRequest.Builder(idempotencyKey: Guid.NewGuid().ToString(), deviceCode: DeviceCode)
            .Build();

        try
        {
            var result = await Client.DevicesApi.CreateDeviceCodeAsync(body: body);

            CounterBtn.Text = result.DeviceCode.Code;
        }
        catch (ApiException ex)
        {
            Console.WriteLine("Failed to make the request");
            Console.WriteLine($"Response Code: {ex.ResponseCode}");
            Console.WriteLine($"Exception: {ex.Message}");
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

