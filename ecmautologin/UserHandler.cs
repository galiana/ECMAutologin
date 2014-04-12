using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website
{
    public class UserHandler: HttpRequestProcessor
    {
        public UserHandler()
        {
        }

        /// <summary>
        /// Runs the processor.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Process(HttpRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            
        }
}