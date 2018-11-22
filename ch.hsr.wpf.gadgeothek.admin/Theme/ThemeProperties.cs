using System.Windows;
using System.Windows.Media;

namespace ch.hsr.wpf.gadgeothek.admin.Theme {

    public class ThemeProperties {
        #region ShadowDepthProperty
        /// <summary>
        ///     Define the <see cref="ShadowDepthProperty" /> for the <see cref="Shadow"/>. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty ShadowDepthProperty = DependencyProperty.RegisterAttached(
            "ShadowDepth",
            typeof(ShadowDepth),
            typeof(ThemeProperties),
            new FrameworkPropertyMetadata(default(ShadowDepth), FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        ///     Sets the <see cref="ShadowDepth" /> of the <see cref="Shadow" />.
        /// </summary>
        public static void SetShadowDepth(DependencyObject element, ShadowDepth value)
        {
            element.SetValue(ShadowDepthProperty, value);
        }

        /// <summary>
        ///     Gets the <see cref="ShadowDepth" /> of the <see cref="Shadow" />, if one is present in the template of the
        ///     specified element.
        /// </summary>
        public static ShadowDepth GetShadowDepth(DependencyObject element)
        {
            return (ShadowDepth)element.GetValue(ShadowDepthProperty);
        }
        #endregion

        #region InnerBorderThicknessProperty
        /// <summary>
        ///     Define the <see cref="InnerBorderThicknessProperty" /> for the outer <see cref="System.Windows.Controls.Border"/>. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty InnerBorderThicknessProperty = DependencyProperty.RegisterAttached(
            "InnerBorderThickness",
            typeof(Thickness),
            typeof(ThemeProperties),
            new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        ///     Sets the <see cref="Thickness" /> of the inner <see cref="System.Windows.Controls.Border"/>.
        /// </summary>
        public static void SetInnerBorderThickness(DependencyObject element, Thickness value)
        {
            element.SetValue(InnerBorderThicknessProperty, value);
        }

        /// <summary>
        ///     Gets the <see cref="Thickness" /> of the inner <see cref="System.Windows.Controls.Border"/>, if one is present in the template of the
        ///     specified element.
        /// </summary>
        public static Thickness GetInnerBorderThickness(DependencyObject element)
        {
            return (Thickness)element.GetValue(InnerBorderThicknessProperty);
        }
        #endregion

        #region InnerBorderBrushProperty
        /// <summary>
        ///     Define the <see cref="InnerBorderBrushProperty" /> for the inner <see cref="System.Windows.Controls.Border"/>. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty InnerBorderBrushProperty = DependencyProperty.RegisterAttached(
            "InnerBorderBrush",
            typeof(Brush),
            typeof(ThemeProperties),
            new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        ///     Sets the <see cref="Brush" /> of the inner <see cref="System.Windows.Controls.Border"/>.
        /// </summary>
        public static void SetInnerBorderBrush(DependencyObject element, Brush value)
        {
            element.SetValue(InnerBorderBrushProperty, value);
        }

        /// <summary>
        ///     Gets the <see cref="Brush" /> of the inner <see cref="System.Windows.Controls.Border"/>, if one is present in the template of the
        ///     specified element.
        /// </summary>
        public static Brush GetInnerBorderBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(InnerBorderBrushProperty);
        }
        #endregion

        #region HoverBrush
        /// <summary>
        ///     Define the <see cref="HoverBrushProperty" /> for the control. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty HoverBrushProperty = DependencyProperty.RegisterAttached(
            "HoverBrush",
            typeof(Brush),
            typeof(ThemeProperties),
            new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        ///     Sets the <see cref="Brush" /> of the control which will apply when mouseOver control.
        /// </summary>
        public static void SetHoverBrush(DependencyObject element, Brush value)
        {
            element.SetValue(HoverBrushProperty, value);
        }

        /// <summary>
        ///     Gets the <see cref="Brush" /> of the control which will apply when mouseOver control, if one is present in the template of the
        ///     specified element.
        /// </summary>
        public static Brush GetHoverBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(HoverBrushProperty);
        }
        #endregion

        #region PressedBrush
        /// <summary>
        ///     Define the <see cref="PressedBrushProperty" /> for the control. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty PressedBrushProperty = DependencyProperty.RegisterAttached(
            "PressedBrush",
            typeof(Brush),
            typeof(ThemeProperties));

        /// <summary>
        ///     Sets the <see cref="Brush" /> of the control which will apply when control is pressed.
        /// </summary>
        public static void SetPressedBrush(DependencyObject element, Brush value)
        {
            element.SetValue(PressedBrushProperty, value);
        }

        /// <summary>
        ///     Gets the <see cref="Brush" /> of the control which will apply when control is pressed, if one is present in the template of the
        ///     specified element.
        /// </summary>
        public static Brush GetPressedBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(PressedBrushProperty);
        }
        #endregion

        #region FocusBrush
        /// <summary>
        ///     Define the <see cref="FocusBrushProperty" /> for the control. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty FocusBrushProperty = DependencyProperty.RegisterAttached(
            "FocusBrush",
            typeof(Brush),
            typeof(ThemeProperties));

        /// <summary>
        ///     Sets the <see cref="Brush" /> of the control which will apply when control has focus.
        /// </summary>
        public static void SetFocusBrush(DependencyObject element, Brush value)
        {
            element.SetValue(FocusBrushProperty, value);
        }

        /// <summary>
        ///     Gets the <see cref="Brush" /> of the control which will apply when control has focus, if one is present in the template of the
        ///     specified element.
        /// </summary>
        public static Brush GetFocusBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusBrushProperty);
        }
        #endregion

        #region CheckedBrush
        /// <summary>
        ///     Define the <see cref="CheckedBrushProperty" /> for the control. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty CheckedBrushProperty = DependencyProperty.RegisterAttached(
            "CheckedBrush",
            typeof(Brush),
            typeof(ThemeProperties));

        /// <summary>
        ///     Sets the <see cref="Brush" /> of the control which will apply when control is checked.
        /// </summary>
        public static void SetCheckedBrush(DependencyObject element, Brush value)
        {
            element.SetValue(CheckedBrushProperty, value);
        }

        /// <summary>
        ///     Gets the <see cref="Brush" /> of the control which will apply when control is checked, if one is present in the template of the
        ///     specified element.
        /// </summary>
        public static Brush GetCheckedBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(CheckedBrushProperty);
        }

        #endregion
    }
}