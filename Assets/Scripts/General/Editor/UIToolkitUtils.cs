using UnityEngine;
using UnityEngine.UIElements;

namespace And.Editor.UIToolkit
{
    public static class UIToolkitUtils
    {
        public static readonly Color BorderColorDefault = new(1, 1, 1, 0.3f);
        public static readonly Color BackgroundColorDefault = new(1, 1, 1, 0.3f);
        public static readonly Color BackgroundColorIndent1 = new(1, 1, 1, 0.1f);
        public static readonly Color BackgroundColorSelected = new(0, 0.5f, 0, 1f);
        public static readonly int MarginDefault = 4;
        public static readonly int PaddingDefault = 2;
        public static readonly int BorderWidthDefault = 2;
        public static readonly int BorderRadiusDefault = 2;

        public static VisualElement BuildEmpty(int size = 1)
        {
            var empty = new VisualElement();
            empty.style.flexBasis = size * 16;
            empty.style.flexGrow = 0;
            empty.style.flexShrink = 0;
            return empty;
        }
    }
}
