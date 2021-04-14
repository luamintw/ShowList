using System;
using System.Globalization;

namespace ShowList.Service.Exceptions
{
    public class ShowOperationException : Exception
    {
        public ShowOperationException() : base() { }

        public ShowOperationException(string message) : base(message) { }

        public ShowOperationException(string message, params object[] args) :
            base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}