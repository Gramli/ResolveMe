using System;
using System.Runtime.Serialization;

namespace EvaluationDataContract
{
    [DataContract]
    public class Request
    {
        [DataMember]
        public object Argument { get; set;}
    }
}
