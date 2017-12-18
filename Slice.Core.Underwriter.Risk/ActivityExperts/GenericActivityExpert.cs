#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Threading.Tasks;
using Slice.Core.Underwriter.Risk.Models;

namespace Slice.Core.Underwriter.Risk.ActivityExperts
{
    public class GenericActivityExpert : BaseActivityExpert
    {
        public override Task<int> GetRiskValue(IActivity subject)
        {
            throw new InvalidOperationException();
        }
    }

    public class RentActivityExpert : BaseActivityExpert
    {
        public override Task<int> GetRiskValue(IActivity subject)
        {
            return Task.FromResult(RiskLevel);
        }
    }

    public class DriveActivityExpert : BaseActivityExpert
    {
        public override Task<int> GetRiskValue(IActivity subject)
        {
            return Task.FromResult(RiskLevel);
        }
    }
}