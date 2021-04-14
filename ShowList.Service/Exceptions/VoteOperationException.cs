using System;
using System.Globalization;

namespace ShowList.Service.Exceptions
{
    public class VoteOperationException : Exception
    {
        public VoteOperationException() : base() { }

        public VoteOperationException(string message) : base(message) { }

        public VoteOperationException(string message, params object[] args) :
            base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}