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
using Slice.Core.Underwriter.Business.Managers;
using Slice.Core.Underwriter.Data;
using Slice.Core.Underwriter.Data.Interfaces;
using Slice.Core.Underwriter.Data.Models.Rce;

namespace Slice.Core.Underwriter.Api.Controllers.Dwellings
{
    [Route("api/dwellings/rce")]
    public class RceController : BaseController
    {
        private readonly IDwellingRceManager _manager;

        public RceController(IDwellingRceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<RceByAdministrativeLevel1> GetByArea(string country, string area)
        {
            try
            {
                var item = await _manager.GetByArea(country, area).ConfigureAwait(false);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
       
        }
    }
}