#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Slice.Core.Underwriter.Api.Models.Request;
using Slice.Core.Underwriter.Data.Models;
using Slice.Core.Underwriter.Data.Models.Weather;
using Slice.Core.Underwriter.Weather.Managers;

namespace Slice.Core.Underwriter.Api.Controllers.Weather
{
    [Route("api/weather/warnings")]
    public class WarningsController : BaseController
    {
        private readonly IWeatherWarningManager _manager;

        public WarningsController(IWeatherWarningManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("query")]
        public async Task<IEnumerable<Warning>> Query(GetWarningRequest request)
        {
            var items = await _manager.GetByAreaOn(request.Country, request.Area, request.EffectiveOn).ConfigureAwait(false);
            return items;
        }

        #region CRUD

        [HttpGet]
        public async Task<IEnumerable<Warning>> GetAll()
        {
            var items = await _manager.GetAllAsync().ConfigureAwait(false);
            return items;
        }

        [HttpGet("{id}")]
        public async Task<Warning> Get(Guid id)
        {
            var item = await _manager.GetByIdAsync(id).ConfigureAwait(false);
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddWarningRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            var warning = await _manager.AddAsync(request.Country, request.Area, request.SearchedOn, request.StartsOn, request.EndsOn, request.Type).ConfigureAwait(false);

            return Ok(warning);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AddWarningRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            var warning = await _manager.UpdateAsync(id, request.Country, request.Area, request.SearchedOn, request.StartsOn, request.EndsOn, request.Type).ConfigureAwait(false);

            return Ok(warning);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _manager.DeleteById(id).ConfigureAwait(false);

            return Ok();
        }

        #endregion
    }
}