#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Slice.Core.Underwriter.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ILogger Logger;

        protected BaseController()
        {
            var loggerFactory = new LoggerFactory();

            Logger = loggerFactory.CreateLogger(GetType().Name);
        }
    }
}