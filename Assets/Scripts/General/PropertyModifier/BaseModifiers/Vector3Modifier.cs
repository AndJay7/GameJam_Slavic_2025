using System;
using UnityEngine;

namespace And.Logic.ValueModifiers
{
    [Serializable]
    public class Vector3Modifier : SerializeFieldModifier<Vector3>
    {
        private static readonly Vector3Operators _operators = new Vector3Operators();
        public override IValueOperatorContainer<Vector3> Operators => _operators;

        public Vector3Modifier(Vector3 value = default)
            : base(value)
        {
        }

        private class Vector3Operators : IValueOperatorContainer<Vector3>
        {
            private static readonly IValueOperator<Vector3>[] Operators = new IValueOperator<Vector3>[]
            {
                new Vector3OverrideOperator(),
                new Vector3AddOperator(),
                new Vector3MultiplyOperator(),
            };

            public IValueOperator<Vector3> this[int index] => Operators[index];

            public int Count => Operators.Length;

            private class Vector3AddOperator : IValueOperator<Vector3>
            {
                public string Name { get; } = "Add";

                public Vector3 Evaluate(Vector3 a, Vector3 b)
                {
                    return a + b;
                }
            }

            private class Vector3OverrideOperator : IValueOperator<Vector3>
            {
                public string Name { get; } = "Override";

                public Vector3 Evaluate(Vector3 a, Vector3 b)
                {
                    return a;
                }
            }

            private class Vector3MultiplyOperator : IValueOperator<Vector3>
            {
                public string Name { get; } = "Multiply";

                public Vector3 Evaluate(Vector3 a, Vector3 b)
                {
                    a.Scale(b);
                    return a;
                }
            }
        }
    }
}