﻿#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using Slice.Core.Underwriter.Common.Constants;
using Slice.Core.Underwriter.Risk.Interfaces;
using Slice.Core.Underwriter.Risk.Models;

namespace Slice.Core.Underwriter.Risk.ActivityExperts
{
    public static class ActivityExpertFactory
    {
        public static IActivityExpert GetExpert(IActivity item)
        {
            IActivityExpert expert;
            switch (item.Kind)
            {
                case ActitityType.Rent:
                    expert = new RentActivityExpert();
                    break;
                case ActitityType.Drive:
                    expert = new DriveActivityExpert();
                    break;
                default:
                    expert = new GenericActivityExpert();
                    break;
            }

            return expert;
        }
    }
}