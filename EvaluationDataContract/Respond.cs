using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EvaluationDataContract
{
    [DataContract]
    public class Respond<T>
    {
        [DataMember]
        public Request Request { get; private set; }
        [DataMember]
        public T RespondValue { get; private set; }

        public Respond() { }

        public Respond(T respond, Request request)
        {
            this.RespondValue = respond;
            this.Request = request;
        }
    }
}
