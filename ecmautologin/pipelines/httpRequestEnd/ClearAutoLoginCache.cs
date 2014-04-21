﻿using Galiana.Helpers;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galiana.pipelines.httpRequestEnd
{
    public class ClearAutoLoginCache: HttpRequestProcessor
    {
        public ClearAutoLoginCache(){}

        /// <summary>
        /// Runs the processor.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Process(HttpRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            SessionHelper.ClearCache();
        }
    }
}