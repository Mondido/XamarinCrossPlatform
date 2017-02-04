using NUnit.Framework;
using System;
using MondidoCrossPlatformSDK;
using System.Collections.Generic;
using Mondido.Payment;
using System.Linq;

namespace Test
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void CreateAPICardPayment()
		{
			var ex = new Example();
			var t = ex.CreateAPICardPayment();
			Assert.IsNotNull(t.Id);

		}
		[Test()]
		public void CreateHostedCardPaymentUrl()
		{
			var ex = new Example();
			var url = ex.CreateHostedCardPaymentUrl();
			Assert.IsNotEmpty(url);

		}
		[Test()]
		public void CreateStoredCard()
		{
			var ex = new Example();
			var sc = ex.CreateStoreCard();
			Assert.IsNotNull(sc.Id);

		}
		[Test()]
		public void CapturePayment()
		{
			var ex = new Example();
			var t = ex.CapturePayment();
			Assert.IsNotNull(t.Id);

		}
		[Test()]
		public void ListTransactions()
		{
			var ex = new Example();
			IEnumerable<Transaction> t = ex.ListTransactions();
			Assert.GreaterOrEqual(1,t.Count());

		}
	}
}
