using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutoRobo.WebApi.Entities
{
    public class MockUser : User
    {
        /// <summary>
        /// mockuserId
        /// </summary>
        public string Id { get; set; }
        public bool IsDefault { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public bool? IsActiveMail { get; set; }
         [JsonProperty(ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        public DateTime CreateDate { get; set; }
    }
}
