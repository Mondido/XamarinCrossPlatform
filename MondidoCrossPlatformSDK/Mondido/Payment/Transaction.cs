﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Mondido.Utils;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using MondidoCrossPlatformSDK;
using MondidoCrossPlatformSDK.Payment;
using MondidoCrossPlatformSDK.Request;

namespace Mondido.Payment
{
    [DataContract]
    public class Transaction : BaseModel
    {

        [JsonProperty(PropertyName = "card_holder")]
        public string CardHolder { get; set; }

        [JsonProperty(PropertyName = "card_number")]
        public string CardNumber { get; set; }

        [JsonProperty(PropertyName = "card_type")]
        public string CardType { get; set; }

        [JsonProperty(PropertyName = "card_cvv")]
        public string CardCVV { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }


        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "merchant_id")]
        public int MerchantId { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }

        [JsonProperty(PropertyName = "payment_ref")]
        public string PaymentRef { get; set; }

        [JsonProperty(PropertyName = "customer_ref")]
        public string CustomerRef { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool Test { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public dynamic MetaData { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "payment_request")]
        public dynamic PaymentRequest { get; set; }

        [JsonProperty(PropertyName = "template_id")]
        public int? TemplateId { get; set; }

        [JsonProperty(PropertyName = "error")]
        public dynamic Error { get; set; }

        [JsonProperty(PropertyName = "cost")]
        public dynamic Cost { get; set; }

        [JsonProperty(PropertyName = "success_url")]
        public string SucessUrl { get; set; }

        [JsonProperty(PropertyName = "error_url")]
        public string ErrorUrl { get; set; }

        [JsonProperty(PropertyName = "refund")]
        public List<Refund> Refund { get; set; }

        [JsonProperty(PropertyName = "stored_card")]
        public StoredCard StoredCard { get; set; }

        [JsonProperty(PropertyName = "customer")]
        public dynamic Customer { get; set; }

        [JsonProperty(PropertyName = "transaction_type")] 
        public string TransactionType { get; set; }

        [JsonProperty(PropertyName = "subscription")]
        public object Subscription { get; set; }

        [JsonProperty(PropertyName = "webhooks")]
        public object WebHooks { get; set; }

        [JsonProperty(PropertyName = "encrypted")]
        public string Encrypted { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "authorize")]
        public string Authorize { get; set; }

        [JsonProperty(PropertyName = "items")]
        public Items Items { get; set; }


		public static async Task<Transaction> Create(HttpParams data)
		{
			var res = await HttpPost("/transactions", data);
			return res.FromJson<Transaction>();
		}

		public static Transaction Capture(int id, string amount)
        {
			var data = new HttpParams();
            data.Add("amount", amount);
            return HttpPut(string.Format("/transactions/{0}/capture", id),data).Result.FromJson<Transaction>();
        }

		public static Transaction Update(int id, HttpParams data)
        {
            return HttpPut(string.Format("/transactions/{0}", id), data).Result.FromJson<Transaction>();
        }

        public static Transaction Get(int id)
        {
            return HttpGet("/transactions/" + id).Result.FromJson<Transaction>();
        }

		public static IEnumerable<Transaction> List(int take, int skip, Dictionary<string,string> filters=null, string sortBy="id:desc")
        {
            string parsedFilters = ParseFilters(filters);
            return HttpGet(string.Format("/transactions?limit={0}&offset={1}{3}&order_by={2}",take,skip,sortBy,parsedFilters)).Result.FromJson<IEnumerable<Transaction>>();
        }

    }
}