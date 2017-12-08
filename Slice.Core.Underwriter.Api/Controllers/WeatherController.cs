#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slice.Core.Underwriter.Data;
using Slice.Core.Underwriter.Data.Models;

namespace Slice.Core.Underwriter.Api.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly WeatherContext _context;

        public WeatherController(WeatherContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        [Route("overrides")]
        public async Task<IEnumerable<Overrides>> GetOverrides()
        {
            var items = await _context.Override.ToListAsync().ConfigureAwait(false);
            return items;
            // return new[] {"value1", "value2"};
        }

        [HttpGet]
        [Route("warnings")]
        public async Task<IEnumerable<Warnings>> GetWarnings()
        {
            var items = await _context.Warnings.ToListAsync().ConfigureAwait(false);
            return items;
            // return new[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}