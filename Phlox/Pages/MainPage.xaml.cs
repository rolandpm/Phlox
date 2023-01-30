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



    public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
        //Testing a payment
        await Application.Current.MainPage.Navigation.PushModalAsync(new CheckoutPage());
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

