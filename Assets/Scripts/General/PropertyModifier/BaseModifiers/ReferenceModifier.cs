using System;

namespace And.Logic.ValueModifiers
{
    [Serializable]
    public class ReferenceModifier<Type> : SerializeReferenceModifier<Type>
    {
        private static readonly ReferenceOperators _operators = new ReferenceOperators();
        public override IValueOperatorContainer<Type> Operators => _operators;

        public ReferenceModifier(Type value = default)
            : base(value)
        {
        }

        private class ReferenceOperators : IValueOperatorContainer<Type>
        {
            private readonly static IValueOperator<Type>[] Operators = new IValueOperator<Type>[]
            {
                new ReferenceOverrideOperator(),
            };

            public IValueOperator<Type> this[int index] => Operators[index];

            public int Count => Operators.Length;

            private class ReferenceOverrideOperator : IValueOperator<Type>
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