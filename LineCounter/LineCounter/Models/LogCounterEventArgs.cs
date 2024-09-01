namespace LineCounter.Models
{
    /// <summary>
    /// The custom event args used by the CounterControl.
    /// Extends the basic EventArgs.
    /// </summary>
    public class LogCounterEventArgs: EventArgs
    {
        /// <summary>
        /// Returns the event message.
        /// </summary>
        public string Message { get { return _message; } }
        /// <summary>
        /// Creates a new log event with a given message.
        /// </summary>
        /// <param name="message"></param>
        public LogCounterEventArgs(string message)
        {
            _message = message;
        }
        /// <summary>
        /// Be a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _message;
        }
        /// <summary>
        /// Message in a bottle...
        /// </summary>
        private string _message = "";
    }
}
