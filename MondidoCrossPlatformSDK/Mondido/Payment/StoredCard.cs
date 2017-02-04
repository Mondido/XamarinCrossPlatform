﻿using System;
using System.Collections.Generic;
using Mondido.Utils;
using MondidoCrossPlatformSDK.Request;
using Newtonsoft.Json;

namespace Mondido.Payment
{
    public class StoredCard : BaseModel
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

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "merchant_id")]
        public int MerchantId { get; set; }

        [JsonProperty(PropertyName = "customer")]
        public dynamic Customer { get; set; }

        [JsonProperty(PropertyName = "customer_ref")]
        public string CustomerRef { get; set; }

        [JsonProperty(PropertyName = "customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty(PropertyName = "encrypted")]
        public string Encrypted { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool Test { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        public static StoredCard Create(HttpParams data)
        {
            return HttpPost("/stored_cards", data).Result.FromJson<StoredCard>();
        }

        public static StoredCard Get(int id)
        {
            return HttpGet("/stored_cards/" + id).Result.FromJson<StoredCard>();
        }

		public static IEnumerable<StoredCard> List(int take, int skip, Dictionary<string,string> filters = null, string sortBy = "id:desc")
        {
            string parsedFilters = ParseFilters(filters);
            return HttpGet(string.Format("/stored_cards?limit={0}&offset={1}{3}&order_by={2}", take, skip, sortBy, parsedFilters)).Result.FromJson<IEnumerable<StoredCard>>();
        }

        public static StoredCard Delete(int id)
        {
            return HttpDelete(string.Format("/stored_cards/" + id)).Result.FromJson<StoredCard>();
        }

		public static StoredCard Update(int id, HttpParams data)
        {
            return HttpPut(string.Format("/stored_cards/" + id),data).Result.FromJson<StoredCard>();
        }
    }
}