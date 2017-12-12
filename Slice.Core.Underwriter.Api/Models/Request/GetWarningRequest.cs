#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using Newtonsoft.Json;

namespace Slice.Core.Underwriter.Api.Models.Request
{
    public class GetWarningRequest
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("effective_on")]
        public DateTime? EffectiveOn { get; set; }
    }
}