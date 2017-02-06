using System;
using System.Collections.Generic;
using Mondido.Configuration;
using Mondido.Payment;
using Mondido.Utils;
using MondidoCrossPlatformSDK.Payment;
using MondidoCrossPlatformSDK.Request;

namespace MondidoCrossPlatformSDK
{
	public class Example
	{
		
		public Example()
		{
			Settings.RSAKey = "-----BEGIN PUBLIC KEY-----\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwVOZDXDqF0cFGwwLqmJB\nMi7ipOLK7d3cwL/v/UhaLRGVqfiDnDbYkax2URd6yBY+81U0OqIvH3ucBap14I3P\nXX+KJ1t7OC16gXwe67OKklaIYx4ELXUiOca84vOqAzT3aKWISX7Rhu7vDqSceMHU\n4xBE6PXXSPdBRYJ5qyau1N2jGj8T9u1Gm9A5jXEQQVyt41cFhY3u1mSf4fUZTN3E\nyU1BGBJVntSf+W5OP6JIRGnPip2RBFib7Ce+wWqDzRhc54quRkgjZ7z7nRBeuWFT\npJMioJJW5cCug2k6plL3vv5lbMD0bYYbuhn0wDg6c4Bf/GR4qJ1PPSsFRY9uKs3A\npwIDAQAB\n-----END PUBLIC KEY-----";
			Settings.ApiPassword = "custom00";
			Settings.ApiUsername = "997";
			Settings.ApiSecret = "$2a$10$qBgrHO8/ijJ7nT.OJ56lCe";
		}

		public Transaction CreateAPICardPayment(bool doAuth = false)
		{
			var postData = new HttpParams();

			var payment_ref = DateTimeOffset.Now.Ticks.ToString();
			var customer_ref = "123";
			var currency = "sek";
			var test = "true";
			var encryptedCard = "4111111111111111".RSAEncrypt(); //DO NOT SEND/RECEIVE CARD NUMBERS IN CLEAR TEXT
			var amount = "10.00";
			var successUrl = "https://www.mondido.com/success/";
			var failUrl = "https://www.mondido.com/fail/";
			var planId = "";
			var webhook = "";
			var auth = "false";
			if (doAuth)
			{
				auth = "true";
			}

			string metadata = @"{ Name: 'Bob', HairColor: 'Brown' }";

			var items = new Items();
			var item = new Item();
			item.Artno = Guid.NewGuid().ToString();
			item.Description = "a prod";
			item.Amount = "10.00";
			item.Discount = "0";
			item.Vat = "25";
			item.Qty = "1";

			items.Add(item);

			postData.Add("merchant_id", Settings.ApiUsername);
			postData.Add("amount", amount);
			postData.Add("payment_ref", payment_ref);
			postData.Add("card_expiry", "0118");
			postData.Add("card_holder", ".net sdk");
			postData.Add("test", test);
			postData.Add("card_cvv", "200");
			postData.Add("card_number", encryptedCard);
			postData.Add("card_type", "VISA");
			postData.Add("currency", currency);
			postData.Add("locale", "en");
			postData.Add("encrypted", "card_number");
			postData.Add("customer_ref", customer_ref);
			postData.Add("metadata", metadata.ToJsonString(true));
			postData.Add("items", items.ToString());
			postData.Add("authorize", auth);
			postData.Add("success_url", successUrl);
			postData.Add("error_url", failUrl);
			postData.Add("plan_id", planId);
			postData.Add("webhook", webhook);

			postData.Add("hash", (Settings.ApiUsername + payment_ref + customer_ref + amount + currency + (test.Equals("true") ? "test" : "") + Settings.ApiSecret).ToMD5());

			var transaction = Transaction.Create(postData);
			return transaction.Result;
		}

		public string CreateHostedCardPaymentUrl(bool doAuth = false)
		{
			var postData = new HttpParams();

			var payment_ref = DateTimeOffset.Now.Ticks.ToString();
			var customer_ref = "123";
			var currency = "sek";
			var test = "true";
			var amount = "10.00";
			var successUrl = "https://www.mondido.com/success/";
			var failUrl = "https://www.mondido.com/fail/";
			var planId = "";
			var webhook = "";
			var auth = "false";
			if (doAuth)
			{
				auth = "true";
			}

			string metadata = @"{ Name: 'Bob', HairColor: 'Brown' }";

			var items = new Items();
			var item = new Item();
			item.Artno = Guid.NewGuid().ToString();
			item.Description = "a prod";
			item.Amount = "10.00";
			item.Discount = "0";
			item.Vat = "25";
			item.Qty = "1";

			items.Add(item);

			postData.Add("merchant_id", Settings.ApiUsername);
			postData.Add("amount", amount);
			postData.Add("payment_ref", payment_ref);
			postData.Add("test", test);
			postData.Add("currency", currency);
			postData.Add("locale", "en");
			postData.Add("customer_ref", customer_ref);
			postData.Add("metadata", metadata.ToJsonString(true));
			postData.Add("items", items.ToString());
			postData.Add("authorize", auth);
			postData.Add("success_url", successUrl);
			postData.Add("error_url", failUrl);
			postData.Add("plan_id", planId);
			postData.Add("webhook", webhook);
			postData.Add("process", "false"); // important when not to execute a payment

			postData.Add("hash", (Settings.ApiUsername + payment_ref + customer_ref + amount + currency + (test.Equals("true") ? "test" : "") + Settings.ApiSecret).ToMD5());

			var transaction = Transaction.Create(postData);
			return transaction.Result.Href;
		}

		public Transaction CapturePayment()
		{
			var t = CreateAPICardPayment(true); //authorize
			return Transaction.Capture(t.Id, "5.00");
		}

		public IEnumerable<Transaction> ListTransactions()
		{
			//create a transaction
			this.CreateAPICardPayment();
			//filter to get only test transactions
			var filters = new Dictionary<string, string>();
			filters.Add("test", "true");

			return Transaction.List(1, 0, filters);
		}

		public StoredCard UpdateStoredCard()
		{
			var postData = new HttpParams();
			CreateStoreCard();
			postData.Add("status", "inactive");
			return StoredCard.Create(postData);
		}

		public StoredCard CreateStoreCard()
		{
			var postData = new HttpParams();
			var encryptedCard = "4111111111111111".RSAEncrypt(); //DO NOT SEND/RECEIVE CARD NUMBERS IN CLEAR TEXT

			postData.Add("card_expiry", "0118");
			postData.Add("card_holder", ".net sdk");
			postData.Add("card_cvv", "200");
			postData.Add("card_number", encryptedCard);
			postData.Add("card_type", "VISA");
			postData.Add("currency", "EUR");
			postData.Add("encrypted", "card_number");
			return StoredCard.Create(postData);
		}


	}
}
