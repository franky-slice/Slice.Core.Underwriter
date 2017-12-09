#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;

namespace Slice.Core.Underwriter.Data.Interfaces
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }

        DateTime? CreatedOn { get; set; }

        string UpdatedBy { get; set; }

        DateTime? UpdatedOn { get; set; }
    }
}