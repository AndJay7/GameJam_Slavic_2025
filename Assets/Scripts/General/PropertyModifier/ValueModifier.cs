using System.Collections.Generic;
using UnityEngine;

namespace And.Logic.ValueModifiers
{
    public interface IValueOperator<Type>
    {
        string Name { get; }
        Type Evaluate(Type a, Type b);
    }

    public interface IValueOperatorContainer<Type>
    {
        int Count { get; }

        IValueOperator<Type> this[int index] { get; }
    }

    public abstract class ValueModifier
    {
        [SerializeField]
        private int _operatorIndex;
        public int OperatorIndex
        {
            get => _operatorIndex;
            set
            {
                _operatorIndex = Mathf.Clamp(value, -1, OperatorsCount - 1);
                _operatorIndex = value;
            }
        }
        public abstract int OperatorsCount { get; }
        public abstract List<string> GetOperatorsNames();
    }

    public abstract class ValueModifier<T> : ValueModifier
    {
        public sealed override int OperatorsCount => Operators.Count;
        public abstract IValueOperatorContainer<T> Operators { get; }
        public abstract T Value { get; set; }


        public ValueModifier(T value, int operatorIndex = -1)
        {
            Value = value;
            OperatorIndex = operatorIndex;
        }

        public T Evaluate(T baseValue)
        {
            T val = baseValue;

            if (OperatorIndex < 0 || OperatorIndex >= Operators.Count)
                return val;

            return Operators[OperatorIndex].Evaluate(Value, val);
        }

        public sealed override List<string> GetOperatorsNames()
        {
            var names = new List<string>();
            var operators = Operators;
            for (int i = 0; i < operators.Count; i++)
            {
                names.Add(operators[i].Name);
            }
            return names;
        }
    }
}