#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Threading.Tasks;
using Slice.Core.Underwriter.Common.Constants;
using Slice.Core.Underwriter.Risk.Models;

namespace Slice.Core.Underwriter.Risk.Estimators
{
    public class PropertyEstimator : BaseEstimator
    {
        public PropertyEstimator()
        {
        }

        public override Task<int> GetValue(IItemOfValue subject)
        {
            if (subject.Kind != ItemType.Property)
            {
                throw new Exception($"Unable to estimate value of invalid type: {subject.Kind}");
            }

            return Task.FromResult(subject.Value);
        }
    }
}