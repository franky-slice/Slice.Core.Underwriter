#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

namespace Slice.Core.Underwriter.Data.Models.Rce
{
    public class RceByPostalCode1 : RceBaseModel
    {
        public string Country { get; set; }

        public string PostalCode1 { get; set; }

        public long? DefaultRce { get; set; }

        public string CurrencyUnits { get; set; }
    }
}