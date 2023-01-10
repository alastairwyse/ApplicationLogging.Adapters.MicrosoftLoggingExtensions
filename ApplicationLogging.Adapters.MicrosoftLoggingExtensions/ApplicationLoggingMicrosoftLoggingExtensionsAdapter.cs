/*
 * Copyright 2023 Alastair Wyse (https://github.com/alastairwyse/ApplicationLogging.Adapters.MicrosoftLoggingExtensions/)
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ApplicationLogging.Adapters.MicrosoftLoggingExtensions
{
    /// <summary>
    /// Adapts the <see cref="IApplicationLogger"/> interface to an implementation of the <see cref="ILogger"/> interface.
    /// </summary>
    /// <remarks>The <see cref="IApplicationLogger.Log(object, LogLevel, string)">IApplicationLogger.Log()</see> method has overloads which contain a 'source' parameter which is designed to capture the object which called the Log() method.  However Microsoft.Extensions.Logging.ILogger methods don't contain an equivalent parameter... the closest is the TCategoryName type parameter.  The issue with trying to map/adapt these is that the TCategoryName type needs to be set and fixed at the construction/setup of the logger, whereas the IApplicationLogger.Log() 'source' parameter can vary at runtime.  However, assuming logger client classes are fairly granular and purpose-specific (i.e. the 'source' parameter is usually passed a 'this' reference from the client), and a dependency injection framework is used in preference to 'drilling' loggers down the object dependency chain, this can be worked around by creating specific instances of this adapter for each client class (and passing the client classes as the TCategoryName parameters).</remarks>
    public class ApplicationLoggingMicrosoftLoggingExtensionsAdapter : IApplicationLogger
    {
        /// <summary>Maps log levels between ApplicationLogging and Microsoft.Extensions.Logging namespaces.</summary>
        protected static Dictionary<ApplicationLogging.LogLevel, Microsoft.Extensions.Logging.LogLevel> logLevelMap = new Dictionary<ApplicationLogging.LogLevel, Microsoft.Extensions.Logging.LogLevel>()
        {
            { ApplicationLogging.LogLevel.Debug, Microsoft.Extensions.Logging.LogLevel.Debug },
            { ApplicationLogging.LogLevel.Information, Microsoft.Extensions.Logging.LogLevel.Information },
            { ApplicationLogging.LogLevel.Warning, Microsoft.Extensions.Logging.LogLevel.Warning },
            { ApplicationLogging.LogLevel.Error, Microsoft.Extensions.Logging.LogLevel.Error },
            { ApplicationLogging.LogLevel.Critical, Microsoft.Extensions.Logging.LogLevel.Critical },
        };

        /// <summary>The <see cref="ILogger"/> implementation to adapt to.</summary>
        protected ILogger logger;

        /// <summary>
        /// Initialises a new instance of the ApplicationLogging.Adapters.MicrosoftLoggingExtensions.ApplicationLoggingMicrosoftLoggingExtensionsAdapter class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> implementation to adapt to.</param>
        public ApplicationLoggingMicrosoftLoggingExtensionsAdapter(ILogger logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc/>
        public void Log(ApplicationLogging.LogLevel level, string text)
        {
            logger.Log(logLevelMap[level], text);
        }

        /// <inheritdoc/>
        public void Log(object source, ApplicationLogging.LogLevel level, string text)
        {
            logger.Log(logLevelMap[level], text);
        }

        /// <inheritdoc/>
        public void Log(int eventIdentifier, ApplicationLogging.LogLevel level, string text)
        {
            logger.Log(logLevelMap[level], new EventId(eventIdentifier), text);
        }

        /// <inheritdoc/>
        public void Log(object source, int eventIdentifier, ApplicationLogging.LogLevel level, string text)
        {
            logger.Log(logLevelMap[level], new EventId(eventIdentifier), text);
        }

        /// <inheritdoc/>
        public void Log(ApplicationLogging.LogLevel level, string text, Exception sourceException)
        {
            logger.Log(logLevelMap[level], sourceException, text);
        }

        /// <inheritdoc/>
        public void Log(object source, ApplicationLogging.LogLevel level, string text, Exception sourceException)
        {
            logger.Log(logLevelMap[level], sourceException, text);
        }

        /// <inheritdoc/>
        public void Log(int eventIdentifier, ApplicationLogging.LogLevel level, string text, Exception sourceException)
        {
            logger.Log(logLevelMap[level], new EventId(eventIdentifier), sourceException, text);
        }

        /// <inheritdoc/>
        public void Log(object source, int eventIdentifier, ApplicationLogging.LogLevel level, string text, Exception sourceException)
        {
            logger.Log(logLevelMap[level], new EventId(eventIdentifier), sourceException, text);
        }
    }
}
