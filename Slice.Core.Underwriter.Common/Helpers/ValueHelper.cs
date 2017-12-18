#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Threading.Tasks;

namespace Slice.Core.Underwriter.Common.Helpers
{
    public static class ValueHelper
    {
        public static async Task<double> GetRandomDouble()
        {
            var rnd = new Random();
            return await Task.Run(() =>
            {
                var value = rnd.Next(0, 100);
                return (double) value / 100;
            }).ConfigureAwait(false);
        }
    }
}