#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using Slice.Core.Underwriter.Risk.Models;

namespace Slice.Core.Underwriter.Risk.Interfaces
{
    public interface IHasActivity
    {
        IActivity Activity { get; set; }
    }
}