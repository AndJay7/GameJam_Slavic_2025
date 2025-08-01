using UnityEngine;

namespace And.Logic.ValueModifiers
{
    public abstract class SerializeFieldModifier<T> : ValueModifier<T>
    {
        [SerializeField]
        private T _value;

        protected SerializeFieldModifier(T value, int operatorIndex = -1) : base(value, operatorIndex) { }

        public override T Value
        {
            get => _value;
            set => _value = value;
        }
    }

    public abstract class SerializeReferenceModifier<T> : ValueModifier<T>
    {
        [SerializeReference]
        private T _value;

        protected SerializeReferenceModifier(T value, int operatorIndex = -1) : base(value, operatorIndex) { }

        public override T Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
