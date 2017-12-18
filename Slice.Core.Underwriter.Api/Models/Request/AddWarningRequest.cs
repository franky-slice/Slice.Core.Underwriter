#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using Newtonsoft.Json;
using Slice.Core.Underwriter.Common.Constants;

namespace Slice.Core.Underwriter.Api.Models.Request
{
    public class AddWarningRequest
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("searched_on")]
        public DateTime SearchedOn { get; set; }

        [JsonProperty("starts_on")]
        public DateTime StartsOn { get; set; }

        [JsonProperty("ends_on")]
        public DateTime EndsOn { get; set; }

        [JsonProperty("type")]
        public WarningType Type { get; set; }
    }
}