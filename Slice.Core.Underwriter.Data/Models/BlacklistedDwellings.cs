using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace Slice.Core.Underwriter.Data.Models
{
    public partial class BlacklistedDwellings
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
        public string Reason { get; set; }
        public DateTime? Tstamp { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string FormattedAddress { get; set; }
        public Guid Id { get; set; }
        public PostgisGeometry CoordPoint { get; set; }
    }
}
