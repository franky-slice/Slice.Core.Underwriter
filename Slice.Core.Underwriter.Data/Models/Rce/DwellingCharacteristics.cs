#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using NpgsqlTypes;

namespace Slice.Core.Underwriter.Data.Models.Rce
{
    public class DwellingCharacteristics : RceBaseModel
    {
        public string Country { get; set; }

        public string AdministrativeLevel1 { get; set; }

        public string AdministrativeLevel2 { get; set; }

        public string Locality { get; set; }

        public string PostalCode1 { get; set; }

        public string PostalCode2 { get; set; }

        public string StreetName { get; set; }

        public string StreetPrefix { get; set; }

        public string StreetSuffix { get; set; }

        public string StreetPredirection { get; set; }

        public string StreetPostdirection { get; set; }

        public string HouseNum { get; set; }

        public string AptNum { get; set; }

        public string StreetPrefixPreposition { get; set; }

        public DateTime? Tstamp { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }

        public string FormattedAddress { get; set; }

        public double? DwellingArea { get; set; }

        public int? DwellingYearBuilt { get; set; }

        public PostgisGeometry CoordPoint { get; set; }
    }
}