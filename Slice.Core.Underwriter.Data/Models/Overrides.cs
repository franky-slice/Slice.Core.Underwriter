﻿#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;

namespace Slice.Core.Underwriter.Data.Models
{
    public class Overrides
    {
        public long RowId { get; set; }

        public string Country { get; set; }

        public string Area { get; set; }

        public DateTime StartsOn { get; set; }

        public DateTime EndsOn { get; set; }

        public string Warning { get; set; }

        public int? WarningId { get; set; }

        public string OverrideType { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}