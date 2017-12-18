#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slice.Core.Underwriter.Data;
using Slice.Core.Underwriter.Data.Models;

namespace Slice.Core.Underwriter.Api.Controllers.Dwellings
{
    [Route("api/dwellings/blacklisted")]
    public class BlacklistedController : BaseController
    {
        private readonly DwellingContext _context;

        public BlacklistedController(DwellingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<BlacklistedDwellings>> GetAll()
        {
            try
            {
                var items = await _context.Set<BlacklistedDwellings>().Take(100).ToListAsync().ConfigureAwait(false);
                return items;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}