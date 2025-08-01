using UnityEngine;
using UnityEngine.UIElements;

namespace And.Editor.UIToolkit
{
    public static class UIToolkitExtension
    {
        public static void SetBorderColorDefault(this IStyle style)
        {
            style.SetBorderColor(UIToolkitUtils.BorderColorDefault);
        }

        public static void SetBorderWidthDefault(this IStyle style)
        {
            style.SetBorderWidth(UIToolkitUtils.BorderWidthDefault);
        }

        public static void SetBorderRadiusDefault(this IStyle style)
        {
            style.SetBorderRadius(UIToolkitUtils.BorderRadiusDefault);
        }

        public static void SetMarginDefault(this IStyle style)
        {
            style.SetMargin(UIToolkitUtils.MarginDefault);
        }


        public static void SetPaddingDefault(this IStyle style)
        {
            style.SetMargin(UIToolkitUtils.PaddingDefault);
        }

        public static void SetBorderColor(this IStyle style, Color color)
        {
            style.borderBottomColor = color;
            style.borderLeftColor = color;
            style.borderRightColor = color;
            style.borderTopColor = color;
        }

        public static void SetBorderWidth(this IStyle style, float width)
        {
            style.borderBottomWidth = width;
            style.borderLeftWidth = width;
            style.borderRightWidth = width;
            style.borderTopWidth = width;
        }

        public static void SetBorderRadius(this IStyle style, float radius)
        {
            style.borderBottomLeftRadius = radius;
            style.borderBottomRightRadius = radius;
            style.borderTopLeftRadius = radius;
            style.borderTopRightRadius = radius;
        }

        public static void SetMargin(this IStyle style, float margin)
        {
            style.marginBottom = margin;
            style.marginLeft = margin;
            style.marginRight = margin;
            style.marginTop = margin;
        }

        public static void SetPadding(this IStyle style, float padding)
        {
            style.paddingBottom = padding;
            style.paddingLeft = padding;
            style.paddingRight = padding;
            style.paddingTop = padding;
        }

    }
}
