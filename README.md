ApplicationLogging.Adapters.MicrosoftLoggingExtensions
---

An implementation of an [ApplicationLogging](https://github.com/alastairwyse/ApplicationLogging) [IApplicationLogger](https://github.com/alastairwyse/ApplicationLogging/blob/master/ApplicationLogging/IApplicationLogger.cs) which provides an adapter to write logs to a [Microsoft.Extensions.Logging.ILogger](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger?view=dotnet-plat-ext-7.0) instance (e.g. for integration into ASP.NET projects).

#### Handling of Log() Method 'source' Parameter

Some of the overrides of the IApplicationLogger.Log() method allow specifying the object which is generating the log data in the 'source' parameter.  It would be populated as follows if you want to set the current object as the one generating (first parameter containing 'this' reference in the below example)...

````C#
logger.Log(this, LogLevel.Information, $"Processed {processedEventCount} metric events in {processingTime} milliseconds.");
````

However, the Microsoft.Extensions.Logging.ILogger methods don't offer a directly equivalent parameter... the closest equivalent is  the TCategoryName type parameter.  The issue with trying to map/adapt the 'source' parameter to the TCategoryName type is that TCategoryName needs to be set and fixed at the construction/setup of the logger object, whereas the IApplicationLogger.Log() 'source' parameter can vary at runtime.  Assuming logger client classes are fairly granular and purpose-specific (i.e. the 'source' parameter is usually passed a 'this' reference like the above example), and a dependency injection framework is used in preference to 'drilling' loggers down the object dependency chain, this can be worked around by creating specific instances of the adapter for each distinct logger client class (and passing the client classes as the TCategoryName parameters).  E.g. the above example is taken from the [ApplicationMetrics SqlServerMetricLogger](https://github.com/alastairwyse/ApplicationMetrics.MetricLoggers.SqlServer/blob/1.2.1/ApplicationMetrics.MetricLoggers.SqlServer/SqlServerMetricLogger.cs) class... to follow the suggested workaround, the SqlServerMetricLogger would need to be passed an ApplicationLoggingMicrosoftLoggingExtensionsAdapter instance specifically created for and used by it, i.e...

````C#
var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger<SqlServerMetricLogger>();

var adapter = new ApplicationLoggingMicrosoftLoggingExtensionsAdapter(logger);

adapter.Log(this, LogLevel.Information, $"Processed {processedEventCount} metric events in {processingTime} milliseconds.");
````

Note that although the 'source' parameter is still available on the IApplicationLogger.Log() method, its value is ignored.

#### Links
The documentation below was written for version 1.* of ApplicationLogging.  Minor implementation details may have changed in versions 2.0.0 and above, however the basic principles and use cases documented are still valid.

A detailed sample implementation and use case...<br />
[http://www.alastairwyse.net/methodinvocationremoting/sample-application-4.html](http://www.alastairwyse.net/methodinvocationremoting/sample-application-4.html)

Code documentation...<br />
[http://www.alastairwyse.net/methodinvocationremoting/ndoc/~ApplicationLogging.html](http://www.alastairwyse.net/methodinvocationremoting/ndoc/~ApplicationLogging.html)<br />
[http://www.oraclepermissiongenerator.net/methodinvocationremoting/ndoc/~ApplicationLogging.Adapters.html](http://www.alastairwyse.net/methodinvocationremoting/ndoc/~ApplicationLogging.Adapters.html)<br />

#### Release History

<table>
  <tr>
    <td><b>Version</b></td>
    <td><b>Changes</b></td>
  </tr>
  <tr>
    <td valign="top">1.0.0</td>
    <td>
      First release.
    </td>
  </tr>
</table>