#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

namespace Slice.Core.Underwriter.Weather.Constants
{
    public enum WarningType
    {
        Undefined = 0,
        NoWarning = 1,
        TropicalStormWatches = 2,
        TropicalStormWarnings = 3,
        HurricaneWatches = 4,
        HurricaneWarnings = 5,
        FireWarnings = 6,
        FireWatch = 7
    }
}