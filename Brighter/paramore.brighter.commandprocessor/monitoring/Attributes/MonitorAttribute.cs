﻿// ***********************************************************************
// Assembly         : paramore.brighter.commandprocessor
// Author           : ian
// Created          : 04-30-2015
//
// Last Modified By : ian
// Last Modified On : 04-30-2015
// ***********************************************************************
// <copyright file="MonitorAttribute.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region Licence
/* The MIT License (MIT)
Copyright © 2015 Ian Cooper <ian_hammond_cooper@yahoo.co.uk>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

#endregion

using System;
using System.Configuration;
using paramore.brighter.commandprocessor.monitoring.Handlers;

/// <summary>
/// The Attributes namespace.
/// </summary>
namespace paramore.brighter.commandprocessor.monitoring.Attributes
{
    /// <summary>
    /// Class MonitorAttribute.
    /// </summary>
    public class MonitorAttribute: RequestHandlerAttribute
    {
        bool _monitoringEnabled = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHandlerAttribute" /> class.
        /// </summary>
        /// <param name="step">The step.</param>
        /// <param name="timing">The timing.</param>
        public MonitorAttribute(int step, HandlerTiming timing)
            : base(step, timing)
        {
            var monitoringSetting = ConfigurationManager.AppSettings["IsMonitoringEnabled"];
            _monitoringEnabled = Convert.ToBoolean(monitoringSetting);
        }

        /// <summary>
        /// Gets the type of the handler.
        /// </summary>
        /// <returns>Type.</returns>
        public override Type GetHandlerType()
        {
            return typeof(MonitorHandler<>);
        }
    }
}
