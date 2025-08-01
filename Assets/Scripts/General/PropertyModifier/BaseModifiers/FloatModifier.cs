using System;

namespace And.Logic.ValueModifiers
{
    [Serializable]
    public class FloatModifier : SerializeFieldModifier<float>
    {
        private static readonly FloatOperators _operators = new FloatOperators();
        public override IValueOperatorContainer<float> Operators => _operators;

        public FloatModifier(float value = default)
            : base(value)
        {
        }

        private class FloatOperators : IValueOperatorContainer<float>
        {
            private readonly static IValueOperator<float>[] Operators = new IValueOperator<float>[]
            {
                new FloatOverrideOperator(),
                new FloatAddOperator(),
                new FloatMultiplyOperator(),
            };

            public IValueOperator<float> this[int index] => Operators[index];

            public int Count => Operators.Length;

            private class FloatOverrideOperator : IValueOperator<float>
            {
                public string Name { get; } = "Override";

                public float Evaluate(float a, float b)
                {
                    return a;
                }
            }

            private class FloatAddOperator : IValueOperator<float>
            {
                public string Name { get; } = "Add";

                public float Evaluate(float a, float b)
                {
                    return a + b;
                }
            }

            private class FloatMultiplyOperator : IValueOperator<float>
            {
                public string Name { get; } = "Multiply";

                public float Evaluate(float a, float b)
                {
                    return a * b;
                }
            }
        }
    }
}