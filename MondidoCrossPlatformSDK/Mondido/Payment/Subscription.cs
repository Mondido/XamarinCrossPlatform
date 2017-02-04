﻿using System;
using System.Collections.Generic;
using Mondido.Utils;
using MondidoCrossPlatformSDK.Request;
using Newtonsoft.Json;

namespace Mondido.Payment
{
    public class Subscription : BaseModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "interval_unit")]
        public string IntervalUnit { get; set; }

        [JsonProperty(PropertyName = "periods_left")]
        public int PeriodsLeft { get; set; }

        [JsonProperty(PropertyName = "total_periods")]
        public int TotalPeriods { get; set; }

        [JsonProperty(PropertyName = "price")]
        public Decimal Price { get; set; }

        [JsonProperty(PropertyName = "interval")]
        public int Interval { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "next_at")]
        public DateTime NextAt { get; set; }

        [JsonProperty(PropertyName = "customer")]
        public dynamic Customer { get; set; }

        [JsonProperty(PropertyName = "plan")]
        public dynamic Plan { get; set; }

        [JsonProperty(PropertyName = "stored_card")]
        public dynamic StoredCard { get; set; }

        [JsonProperty(PropertyName = "subscription_quantity")]
        public int SubscriptionQuantity { get; set; }

        [JsonProperty(PropertyName = "items")]
        public Array Items { get; set; }

        public static Subscription Create(HttpParams data)
        {
            return HttpPost("/plans", data).Result.FromJson<Subscription>();
        }

        public static Subscription Get(int id)
        {
            return HttpGet("/subscriptions/" + id).Result.FromJson<Subscription>();
        }

		public static IEnumerable<Subscription> List(int take, int skip, Dictionary<string,string> filters = null, string sortBy = "id:desc")
        {
            string parsedFilters = ParseFilters(filters);
            return HttpGet(string.Format("/subscriptions?limit={0}&offset={1}{3}&order_by={2}", take, skip, sortBy, parsedFilters)).Result.FromJson<IEnumerable<Subscription>>();
        }

        public static Subscription Delete(int id)
        {
            return HttpDelete(string.Format("/subscriptions/" + id)).Result.FromJson<Subscription>();
        }

		public static Subscription Update(int id, HttpParams data)
        {
            return HttpPut(string.Format("/subscriptions/{0}", id), data).Result.FromJson<Subscription>();
        }

    }
}