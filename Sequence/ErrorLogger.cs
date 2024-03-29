﻿using Microsoft.Extensions.Logging;
using Sequence.Core.Internal.Errors;

namespace Sequence;

/// <summary>
/// Logs SCL errors
/// </summary>
public static class ErrorLogger
{
    /// <summary>
    /// Logs an error
    /// </summary>
    public static void LogError(ILogger logger, IError error)
    {
        foreach (var singleError in error.GetAllErrors())
        {
            if (singleError.Exception != null)
                logger.LogError(
                    singleError.Exception,
                    "{Error} - {StepName} {Location}",
                    singleError.Message,
                    singleError.Location.StepName,
                    singleError.Location.TextLocation
                );
            else
                logger.LogError(
                    "{Error} - {StepName} {Location}",
                    singleError.Message,
                    singleError.Location.StepName,
                    singleError.Location.TextLocation
                );
        }
    }
}
