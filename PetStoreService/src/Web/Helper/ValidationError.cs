﻿using Newtonsoft.Json;

namespace PetStoreService.Web.Helper
{
    public class ValidationError(string field, string message)
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; } = field != string.Empty ? field : null;

        public string Message { get; } = message;
    }
}