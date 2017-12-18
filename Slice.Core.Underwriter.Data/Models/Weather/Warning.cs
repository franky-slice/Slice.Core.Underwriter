#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using Slice.Core.Underwriter.Common.Constants;

namespace Slice.Core.Underwriter.Data.Models.Weather
{
    public class Warning : BaseModel
    {
        public string Country { get; set; }

        public string Area { get; set; }

        public DateTime SearchedOn { get; set; }

        public DateTime StartsOn { get; set; }

        public DateTime EndsOn { get; set; }

        public WarningType WarningType { get; set; }
    }
}