﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Loginregister.Models
{
    public class UpdateInfo
    {
        [Key]
        public int UpdateNumber { get; set; }

        public DateTime Timestamp { get; set; }
        [JsonProperty(PropertyName = "Timestamp")]
        public DateTime TimestampString { get; set; }


        public string SerialNumber { get; set; }

        public string ContactName { get; set; }

        public string Designation { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string ClientName { get; set; }

        public string Address { get; set; }
    }
}

