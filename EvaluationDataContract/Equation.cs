using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EvaluationDataContract
{
    [DataContract]
    public class Equation
    {
        [DataMember]
        public List<Equation> Childs { get; private set; } = null;
        [DataMember]
        public string Value { get; private set; }
        [DataMember]
        public Equation Parent { get; private set; } = null;

        public Equation(string value, Equation parent)
            : this(value)
        {
            this.Parent = parent;
        }
        public Equation(string value)
        {
            this.Value = value;
        }

        public Equation() { }
    }
}
