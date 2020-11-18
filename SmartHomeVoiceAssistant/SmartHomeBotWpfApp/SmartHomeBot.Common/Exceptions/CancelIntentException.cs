using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHomeBot.Common.Exceptions
{

    [Serializable]
    public class CancelIntentException : BotException
    {
        public CancelIntentException() { }
        public CancelIntentException(string message) : base(message) { }
        public CancelIntentException(string message, Exception inner) : base(message, inner) { }
        protected CancelIntentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
