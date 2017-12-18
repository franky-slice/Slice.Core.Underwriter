﻿#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;

namespace Slice.Core.Underwriter.Common.Extensions
{
    public static class ConversionExtensions
    {
        public static int AsPercent(this double value)
        {
            return (int) Math.Round(value * 100, MidpointRounding.AwayFromZero);
        }
    }
}