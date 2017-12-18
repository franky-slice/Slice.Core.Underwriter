#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Collections.Generic;
using Slice.Core.Underwriter.Common.Constants;
using Slice.Core.Underwriter.Common.Interfaces;
using Slice.Core.Underwriter.Common.Models;
using Slice.Core.Underwriter.Risk.Interfaces;

namespace Slice.Core.Underwriter.Risk.Models
{
    public abstract class InsurableEvent : IInsurableEvent, ILocation, ITimePeriod, IHasOwner, IHasValuables, IHasActivity
    {
        public IActivity Activity { get; set; }

        public IPerson Owner { get; set; }

        public IEnumerable<IItemOfValue> Valuables { get; set; }

        public int BaseRisk { get; set; }

        public Threshold AcceptableRisk { get; set; }

        #region Methods

        public bool IsInsurable(int risk)
        {
            return AcceptableRisk.InRange(risk);
        }

        #endregion

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }

    public class HomeshareEvent : InsurableEvent
    {
        public HomeshareEvent()
        {
            BaseRisk = 25;

            Activity = new Activity { Kind = ActitityType.Rent };
            AcceptableRisk = new Threshold {Min = 0, Max = 80};
        }
    }
}