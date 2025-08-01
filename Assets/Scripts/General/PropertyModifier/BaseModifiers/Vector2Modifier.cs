using System;
using UnityEngine;

namespace And.Logic.ValueModifiers
{
    [Serializable]
    public class Vector2Modifier : SerializeFieldModifier<Vector2>
    {
        private static readonly Vector2Operators _operators = new Vector2Operators();
        public override IValueOperatorContainer<Vector2> Operators => _operators;

        public Vector2Modifier(Vector2 value = default)
            : base(value)
        {
        }

        private class Vector2Operators : IValueOperatorContainer<Vector2>
        {
            private readonly static IValueOperator<Vector2>[] Operators = new IValueOperator<Vector2>[]
            {
                new Vector2OverrideOperator(),
                new Vector2AddOperator(),
                new Vector2MultiplyOperator(),
            };

            public IValueOperator<Vector2> this[int index] => Operators[index];

            public int Count => Operators.Length;

            private class Vector2AddOperator : IValueOperator<Vector2>
            {
                public string Name { get; } = "Add";

                public Vector2 Evaluate(Vector2 a, Vector2 b)
                {
                    return a + b;
                }
            }

            private class Vector2OverrideOperator : IValueOperator<Vector2>
            {
                public string Name { get; } = "Override";

                public Vector2 Evaluate(Vector2 a, Vector2 b)
                {
                    return a;
                }
            }

            private class Vector2MultiplyOperator : IValueOperator<Vector2>
            {
                public string Name { get; } = "Multiply";

                public Vector2 Evaluate(Vector2 a, Vector2 b)
                {
                    return a * b;
                }
            }
        }
    }
}