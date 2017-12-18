#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using Slice.Core.Underwriter.Common.Constants;

namespace Slice.Core.Underwriter.Risk.Models
{
    public interface IActivity
    {
        ActitityType Kind { get; set; }
    }

    public class Activity : IActivity
    {
        public ActitityType Kind { get; set; }
    }
}