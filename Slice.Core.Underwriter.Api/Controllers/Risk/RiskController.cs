#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Slice.Core.Underwriter.Common.Constants;
using Slice.Core.Underwriter.Common.Extensions;
using Slice.Core.Underwriter.Risk.Models;
using Slice.Core.Underwriter.Risk.Services;

namespace Slice.Core.Underwriter.Api.Controllers.Risk
{
    [Route("api/risk")]
    public class RiskController : BaseController
    {
        private readonly IRiskService _service;

        public RiskController(IRiskService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("calculate")]
        public async Task<double> GetRiskValueAsync()
        {
            var model = new HomeshareEvent();
            var value = await _service.GetRiskBase(model).ConfigureAwait(false);

            return value;
        }

        [HttpPost]
        [Route("insurable")]
        public async Task<bool> CheckIfInsurableAsync()
        {
            var valuables = new List<ItemOfValue>
            {
                new ItemOfValue {Kind = ItemType.Property, Value = 10},
                new ItemOfValue {Kind = ItemType.Vehicle, Value = 50}
            };

            var model = new HomeshareEvent
            {
                Valuables = valuables
            };

            var activityRisk = await _service.GetRiskFromActivity(model).ConfigureAwait(false);
            var baseRisk = await _service.GetRiskBase(model).ConfigureAwait(false);
            var personalRisk = await _service.GetRiskFromOwner(model).ConfigureAwait(false);
            var valueRisk = await _service.GetRiskFromValuables(model).ConfigureAwait(false);

            var value = await _service.GetRiskCompounded(new List<double>
            {
                baseRisk,
                activityRisk,
                personalRisk,
                valueRisk
            }).ConfigureAwait(false);

            var isInsurable = model.IsInsurable(value.AsPercent());

            return isInsurable;
        }
    }
}