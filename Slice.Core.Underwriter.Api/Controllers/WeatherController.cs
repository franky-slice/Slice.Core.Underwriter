#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Slice.Core.Underwriter.Api.Models.Request;
using Slice.Core.Underwriter.Business.Managers;
using Slice.Core.Underwriter.Data.Models;

namespace Slice.Core.Underwriter.Api.Controllers
{
    [Route("api/weather")]
    public class WeatherController : Controller
    {
        private readonly IWeatherManager _weatherManager;

        public WeatherController(IWeatherManager weatherManager)
        {
            _weatherManager = weatherManager;
        }

        #region Override

        // GET api/values
        [HttpGet]
        [Route("overrides")]
        public async Task<IEnumerable<Override>> GetOverrides()
        {
            var items = await _weatherManager.GetOverridesRepository().GetAllAsync().ConfigureAwait(false);
            return items;
        }

        #endregion

        #region Warning

        [HttpGet]
        [Route("warnings")]
        public async Task<IEnumerable<Warning>> GetWarnings()
        {
            var items = await _weatherManager.GetWarningsRepository().GetAllAsync().ConfigureAwait(false);
            return items;
        }

        [HttpGet("warnings/{id}")]
        public async Task<Warning> GetWarningById(int id)
        {
            var item = await _weatherManager.GetWarningsRepository().GetAsync(id).ConfigureAwait(false);
            return item;
        }

        [HttpPost]
        [Route("warnings")]
        public async Task<IActionResult> AddWarning([FromBody] AddWarningRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            var warning = await _weatherManager.AddWarningAsync(request.Country, request.Area, request.SearchedOn, request.StartsOn, request.EndsOn, request.Type).ConfigureAwait(false);

            return Ok(warning);
        }

        // PUT api/values/5
        [HttpPut("warnings/{id}")]
        public async Task<IActionResult> UpdateWarning(int id, [FromBody] AddWarningRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            var warning = await _weatherManager.UpdateWarningAsync(id, request.Country, request.Area, request.SearchedOn, request.StartsOn, request.EndsOn, request.Type).ConfigureAwait(false);

            return Ok(warning);
        }

        // DELETE api/values/5
        [HttpDelete("warnings/{id}")]
        public async Task<IActionResult> DeleteWarning(int id)
        {
            await _weatherManager.DeleteWarningById(id).ConfigureAwait(false);

            return Ok();
        }

        #endregion
    }
}