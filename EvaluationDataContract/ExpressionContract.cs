using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EvaluationDataContract
{
    [DataContract]
    public class ExpressionContract
    {
        [DataMember]
        public List<ExpressionContract> Childs { get; private set; } = null;
        [DataMember]
        public string Value { get; private set; }
        [DataMember]
        public ExpressionContract Parent { get; private set; } = null;

        public ExpressionContract(string value, ExpressionContract parent)
            : this(value)
        {
            this.Parent = parent;
        }
        public ExpressionContract(string value)
        {
            this.Value = value;
        }

        public ExpressionContract() { }
    }
}
