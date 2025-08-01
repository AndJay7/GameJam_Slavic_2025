using System;
using System.Collections.Generic;

namespace And.Logic.ValueModifiers
{
    [Serializable]
    public class ReferenceListModifier<T> : SerializeReferenceModifier<List<T>>
    {
        private static readonly ReferenceListOperators _operators = new ReferenceListOperators();
        public override IValueOperatorContainer<List<T>> Operators => _operators;
        public ReferenceListModifier(List<T> value = default)
            : base(value)
        {
        }

        private class ReferenceListOperators : IValueOperatorContainer<List<T>>
        {
            private static readonly IValueOperator<List<T>>[] Operators = new IValueOperator<List<T>>[]
            {
                new ReferenceListOverrideOperator(),
                new ReferenceListAddOperator(),
            };

            public IValueOperator<List<T>> this[int index] => Operators[index];

            public int Count => Operators.Length;

            private class ReferenceListAddOperator : IValueOperator<List<T>>
            {
                public string Name { get; } = "Add";

                public List<T> Evaluate(List<T> a, List<T> b)
                {
                    var newList = new List<T>(a);
                    newList.AddRange(b);
                    return newList;
                }
            }

            private class ReferenceListOverrideOperator : IValueOperator<List<T>>
            {
                public string Name { get; } = "Override";

                public List<T> Evaluate(List<T> a, List<T> b)
                {
                    return a;
                }
            }
        }
    }
}