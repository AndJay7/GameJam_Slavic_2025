using System;

namespace And.Logic.ValueModifiers
{
    [Serializable]
    public class FieldModifier<Type> : SerializeFieldModifier<Type>
    {
        private static readonly ValueOperators _operators = new ValueOperators();
        public override IValueOperatorContainer<Type> Operators => _operators;

        public FieldModifier(Type value = default)
            : base(value)
        {
        }

        private class ValueOperators : IValueOperatorContainer<Type>
        {
            private readonly static IValueOperator<Type>[] Operators = new IValueOperator<Type>[]
            {
                new FieldOverrideOperator(),
            };

            public IValueOperator<Type> this[int index] => Operators[index];

            public int Count => Operators.Length;

            private class FieldOverrideOperator : IValueOperator<Type>
            {
                public string Name { get; } = "Override";

                public Type Evaluate(Type a, Type b)
                {
                    return a;
                }
            }
        }
    }
}