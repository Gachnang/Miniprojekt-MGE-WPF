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

        #region InnerBorderProperties
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
    }
}