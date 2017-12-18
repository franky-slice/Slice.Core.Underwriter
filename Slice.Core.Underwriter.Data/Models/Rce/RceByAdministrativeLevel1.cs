#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;

namespace Slice.Core.Underwriter.Data.Models.Rce
{
    public class RceByAdministrativeLevel1 : RceBaseModel
     {
        public string Country { get; set; }

        public string AdministrativeLevel1 { get; set; }

        public int? DefaultRce { get; set; }

        public string CurrencyUnits { get; set; }
    }
}