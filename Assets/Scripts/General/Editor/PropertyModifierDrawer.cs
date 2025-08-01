using And.Editor.UIToolkit;
using And.Logic.ValueModifiers;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace And.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ValueModifier), true)]
    public class PropertyModifierDrawer : PropertyDrawer
    {
        private SerializedProperty _property;
        private List<string> _operators = new List<string>();
        private VisualElement _propertyField;
        private VisualElement _rootField;

        private int OperatorIndex
        {
            get => _property.FindPropertyRelative("_operatorIndex").intValue;
            set => _property.FindPropertyRelative("_operatorIndex").intValue = value;
        }

        //offset by one because dropdown is indexing from 0, where operator disabled is -1
        private int DropdownIndex
        {
            get => OperatorIndex + 1;
            set => OperatorIndex = value - 1;
        }

        private bool IsActive => OperatorIndex >= 0;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _property = property;
            var rawProperty = _property.boxedValue as ValueModifier;
            _operators.Clear();
            _operators.Add("Original");
            _operators.AddRange(rawProperty.GetOperatorsNames());

            var root = new VisualElement();
            _rootField = root;
            root.style.SetPaddingDefault();
            root.style.SetBorderRadiusDefault();

            _propertyField = BuildValueContainer();
            root.Add(BuildHeader());
            root.Add(_propertyField);
            return root;
        }

        private VisualElement BuildHeader()
        {
            var root = new VisualElement();
            root.style.SetPaddingDefault();
            root.style.flexDirection = FlexDirection.Row;

            var labelField = new Label(_property.displayName);
            labelField.style.flexGrow = 1;
            root.Add(labelField);

            var operatorIndexField = new DropdownField(_operators, DropdownIndex);
            SetRootBackgroundColor(OperatorIndex >= 0);
            operatorIndexField.style.flexGrow = 1;
            operatorIndexField.RegisterValueChangedCallback(OnOperatorChanged);
            root.Add(operatorIndexField);

            return root;
        }

        private void OnOperatorChanged(ChangeEvent<string> evt)
        {
            DropdownIndex = _operators.IndexOf(evt.newValue);
            SetRootBackgroundColor(OperatorIndex >= 0);
            _propertyField.SetEnabled(IsActive);
            _property.serializedObject.ApplyModifiedProperties();
        }

        private VisualElement BuildValueContainer()
        {
            var root = new VisualElement();
            root.style.backgroundColor = UIToolkitUtils.BackgroundColorIndent1;
            root.style.SetMarginDefault();
            root.SetEnabled(IsActive);

            var property = _property.FindPropertyRelative("_value");
            var propertyLabel = String.Empty;
            if (property.isArray)
                propertyLabel = "List";

            var propertyField = new PropertyField(property, propertyLabel);
            root.Add(propertyField);
            return root;
        }

        private void SetRootBackgroundColor(bool isActive)
        {
            var color = UIToolkitUtils.BackgroundColorIndent1;
            if (isActive)
                color = UIToolkitUtils.BackgroundColorDefault * UIToolkitUtils.BackgroundColorSelected;
            _rootField.style.backgroundColor = color;
        }
    }
}
