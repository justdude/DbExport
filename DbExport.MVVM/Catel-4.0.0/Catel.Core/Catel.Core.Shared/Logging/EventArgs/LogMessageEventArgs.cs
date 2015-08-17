// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogMessageEventArgs.cs" company="Catel development team">
//   Copyright (c) 2011 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Catel.Logging
{
    using System;

    /// <summary>
    ///   Event args containing information about a message that has been written to a log.
    /// </summary>
    public class LogMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogMessageEventArgs" /> class.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="message">The message.</param>
        /// <param name="extraData">The extra data.</param>
        /// <param name="logEvent">The log event.</param>
        public LogMessageEventArgs(ILog log, string message, object extraData, LogEvent logEvent)
            : this(log, message, extraData, logEvent, DateTime.Now) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogMessageEventArgs" /> class.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="message">The message.</param>
        /// <param name="extraData">The extra data.</param>
        /// <param name="logEvent">The log event.</param>
        /// <param name="time">The time.</param>
        public LogMessageEventArgs(ILog log, string message, object extraData, LogEvent logEvent, DateTime time)
        {
            Log = log;
            Time = time;
            Message = message;
            ExtraData = extraData;
            LogEvent = logEvent;
        }

        /// <summary>
        ///   Gets the log the message was written to.
        /// </summary>
        /// <value>The log.</value>
        public ILog Log { get; private set; }

        /// <summary>
        /// Gets the tag, which is automatically retrieved via the <see cref="ILog"/>.
        /// </summary>
        /// <value>The tag.</value>
        public object Tag { get { return Log.Tag; } }

        /// <summary>
        ///   Gets the message that was written to the log.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the extra data.
        /// </summary>
        /// <value>The extra data.</value>
        public object ExtraData { get; private set; }

        /// <summary>
        ///   Gets the log event.
        /// </summary>
        /// <value>The log event.</value>
        public LogEvent LogEvent { get; private set; }

        /// <summary>
        /// Gets the time at which the message was written to the log.
        /// </summary>
        /// <value>The time.</value>
        public DateTime Time { get; private set; }
    }
}