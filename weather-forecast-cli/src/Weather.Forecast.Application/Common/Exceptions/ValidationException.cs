﻿namespace Weather.Forecast.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(string message)
           : base(message)
        {
            Errors = new Dictionary<string, string[]> { { "Error", new string[] { message } } };
        }
    }
}
