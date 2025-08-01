using System;

namespace And.Logic.ValueModifiers
{

    [Serializable]
    public class BoolModifier : SerializeFieldModifier<bool>
    {
        private static readonly BoolOperators _operators = new BoolOperators();
        public override IValueOperatorContainer<bool> Operators => _operators;

        public BoolModifier(bool value = default)
            : base(value)
        {
        }

        private class BoolOperators : IValueOperatorContainer<bool>
        {
            private static readonly IValueOperator<bool>[] Operators = new IValueOperator<bool>[]
            {
                new BoolOverrideOperator(),
                new BoolAddOperator(),
                new BoolMultiplyOperator(),
            };

            public IValueOperator<bool> this[int index] => Operators[index];

            public int Count => Operators.Length;

            private class BoolOverrideOperator : IValueOperator<bool>
            {
                public string Name { get; } = "Override";

                public bool Evaluate(bool a, bool b)
                {
                    return a;
                }
            }

            private class BoolAddOperator : IValueOperator<bool>
            {
                public string Name { get; } = "Add";

                public bool Evaluate(bool a, bool b)
                {
                    return a || b;
                }
            }

            private class BoolMultiplyOperator : IValueOperator<bool>
            {
                public string Name { get; } = "Multiply";

                public bool Evaluate(bool a, bool b)
                {
                    return a && b;
                }
            }
        }
    }
}