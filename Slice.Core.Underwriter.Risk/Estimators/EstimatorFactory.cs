#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using Slice.Core.Underwriter.Common.Constants;
using Slice.Core.Underwriter.Risk.Interfaces;
using Slice.Core.Underwriter.Risk.Models;

namespace Slice.Core.Underwriter.Risk.Estimators
{
    public static class EstimatorFactory
    {
        public static IEstimator GetEstimator(IItemOfValue item)
        {
            IEstimator estimator;
            switch (item.Kind)
            {
                case ItemType.Property:
                    estimator = new PropertyEstimator();
                    break;
                case ItemType.Vehicle:
                    estimator = new VehicleEstimator();
                    break;
                default:
                    estimator = new GenericEstimator();
                    break;
            }

            return estimator;
        }
    }
}