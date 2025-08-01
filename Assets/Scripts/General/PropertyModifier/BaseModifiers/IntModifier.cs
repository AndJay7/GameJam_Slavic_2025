using System;

namespace And.Logic.ValueModifiers
{
    [Serializable]
    public class IntModifier : SerializeFieldModifier<int>
    {
        private static readonly IntOperators _operators = new IntOperators();
        public override IValueOperatorContainer<int> Operators => _operators;
        public IntModifier(int value = default)
            : base(value)
        {
        }

        private class IntOperators : IValueOperatorContainer<int>
        {
            private static readonly IValueOperator<int>[] Operators = new IValueOperator<int>[]
            {
                new IntOverrideOperator(),
                new IntAddOperator(),
                new IntMultiplyOperator(),
            };

            public IValueOperator<int> this[int index] => Operators[index];

            public int Count => Operators.Length;

            private class IntAddOperator : IValueOperator<int>
            {
                public string Name { get; } = "Add";

                public int Evaluate(int a, int b)
                {
                    return a + b;
                }
            }

            private class IntOverrideOperator : IValueOperator<int>
            {
                public string Name { get; } = "Override";

                public int Evaluate(int a, int b)
                {
                    return a;
                }
            }

            private class IntMultiplyOperator : IValueOperator<int>
            {
                public string Name { get; } = "Multiply";

                public int Evaluate(int a, int b)
                {
                    return a * b;
                }
            }
        }
    }
}