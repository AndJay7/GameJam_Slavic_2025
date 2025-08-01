using System;
using System.Collections.Generic;

namespace And.Logic.ValueModifiers
{
    [Serializable]
    public class FieldListModifier<T> : SerializeFieldModifier<List<T>>
    {
        private static readonly FieldListOperators _operators = new FieldListOperators();
        public override IValueOperatorContainer<List<T>> Operators => _operators;
        public FieldListModifier(List<T> value = default)
            : base(value)
        {
        }

        private class FieldListOperators : IValueOperatorContainer<List<T>>
        {
            private static readonly IValueOperator<List<T>>[] Operators = new IValueOperator<List<T>>[]
            {
                new FieldListOverrideOperator(),
                new FieldListAddOperator(),
            };

            public IValueOperator<List<T>> this[int index] => Operators[index];

            public int Count => Operators.Length;

            private class FieldListAddOperator : IValueOperator<List<T>>
            {
                public string Name { get; } = "Add";

                public List<T> Evaluate(List<T> a, List<T> b)
                {
                    var newList = new List<T>(a);
                    newList.AddRange(b);
                    return newList;
                }
            }

            private class FieldListOverrideOperator : IValueOperator<List<T>>
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