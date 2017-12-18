#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;

namespace Slice.Core.Underwriter.Risk.Models
{
    public interface IPerson
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        DateTime DateOfBirth { get; set; }
    }

    public class Person : IPerson
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}