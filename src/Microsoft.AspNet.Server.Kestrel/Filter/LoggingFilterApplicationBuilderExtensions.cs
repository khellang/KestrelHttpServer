﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNet.Server.Kestrel.Filter
{
    public static class LoggingFilterApplicationBuilderExtensions
    {
        /// <summary>
        /// Emits verbose logs for bytes read from and written to the connection.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseKestrelConnectionLogging(this IApplicationBuilder app)
        {
            var serverInfo = app.ServerFeatures.Get<IKestrelServerInformation>();
            if (serverInfo != null)
            {
                var prevFilter = serverInfo.ConnectionFilter ?? new NoOpConnectionFilter();
                var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
                serverInfo.ConnectionFilter = new LoggingConnectionFilter(loggerFactory, prevFilter);
            }
            return app;
        }
    }
}
