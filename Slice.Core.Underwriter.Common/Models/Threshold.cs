#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

namespace Slice.Core.Underwriter.Common.Models
{
    public class Threshold
    {
        public int Min { get; set; }

        public int Max { get; set; }

        public bool InRange(int value)
        {
            return value >= Min && value <= Max;
        }
    }
}