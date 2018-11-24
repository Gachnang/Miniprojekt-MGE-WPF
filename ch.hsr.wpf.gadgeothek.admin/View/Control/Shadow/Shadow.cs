using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace ch.hsr.wpf.gadgeothek.admin.View.Control.Shadow {
    /// <summary>
    ///     Create a predefined Shadow.
    ///     <para>
    ///         It provides <see cref="System.Windows.Media.Effects.DropShadowEffect" /> from a preset of predefined effects
    ///         and animation for the transitions between each of them.
    ///     </para>
    /// </summary>
    public class Shadow : Border {
        private static readonly IDictionary<ShadowDepth, DropShadowEffect> shadowsDictionary;

        /// <summary>
        ///     Some code let the designer crash, so we detect if inDesignTime.
        /// </summary>
        private static bool isShadowInDesignTime;

        /// <summary>
        ///     Identifies the <see cref="AnimationDuration" /> dependency property.
        ///     Default Value: 40.0 .
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(double), typeof(Shadow),
                new FrameworkPropertyMetadata(40d));

        /// <summary>
        ///     Identifies the <see cref="ShadowDepth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowDepthProperty =
            DependencyProperty.Register("ShadowDepth", typeof(ShadowDepth), typeof(Shadow),
                new FrameworkPropertyMetadata(default(ShadowDepth), FrameworkPropertyMetadataOptions.AffectsRender,
                    OnShadowDepthPropertyChanged));

        private DoubleAnimation blurRadiusAnimation = new DoubleAnimation();
        private TimeSpan duration;
        private DropShadowEffect newEffect = new DropShadowEffect();

        private DropShadowEffect oldEffect = new DropShadowEffect();
        private DoubleAnimation opacityAnimation = new DoubleAnimation();
        private DoubleAnimation shadowDepthAnimation = new DoubleAnimation();

        static Shadow() {
            // Loads the predefined shadows.
            ResourceDictionary resourceDictionary = new ResourceDictionary {
                Source = new Uri(
                    "pack://application:,,,/ch.hsr.wpf.gadgeothek.admin;component/View/Control/Shadow/Shadows.xaml",
                    UriKind.Absolute)
            };

            shadowsDictionary = new Dictionary<ShadowDepth, DropShadowEffect> {
                {ShadowDepth.Depth0, (DropShadowEffect) resourceDictionary["PART_ShadowDepth0"]},
                {ShadowDepth.Depth1, (DropShadowEffect) resourceDictionary["PART_ShadowDepth1"]},
                {ShadowDepth.Depth2, (DropShadowEffect) resourceDictionary["PART_ShadowDepth2"]},
                {ShadowDepth.Depth3, (DropShadowEffect) resourceDictionary["PART_ShadowDepth3"]},
                {ShadowDepth.Depth4, (DropShadowEffect) resourceDictionary["PART_ShadowDepth4"]},
                {ShadowDepth.Depth5, (DropShadowEffect) resourceDictionary["PART_ShadowDepth5"]}
            };

            // Default style key so to receive an implicit style reference
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Shadow), new FrameworkPropertyMetadata(typeof(Shadow)));

            // check if in Desgin time
            if ((bool) DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue) {
                isShadowInDesignTime = true;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Shadow" /> class.
        /// </summary>
        public Shadow() {
            if (!isShadowInDesignTime) {
                SetShadowEffect();
            }
        }

        /// <summary>
        ///     Gets or sets the duration of the animation of the <see cref="DropShadowEffect" /> change in milliseconds.
        /// </summary>
        public double AnimationDuration {
            get => (double) GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value for the <see cref="ShadowDepth" />.
        /// </summary>
        public ShadowDepth ShadowDepth {
            get => (ShadowDepth) GetValue(ShadowDepthProperty);
            set => SetValue(ShadowDepthProperty, value);
        }

        private static void OnShadowDepthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Shadow shadow = d as Shadow;

            if (shadow != null && e != null) {
                if (e.OldValue == e.NewValue) {
                    return;
                }

                shadow.oldEffect = shadowsDictionary[(ShadowDepth) e.OldValue];
                shadow.newEffect = shadowsDictionary[(ShadowDepth) e.NewValue];
                shadow.duration = TimeSpan.FromMilliseconds(shadow.AnimationDuration);

                if (!isShadowInDesignTime) {
                    if (shadow.IsInitialized) {
                        shadow.shadowDepthAnimation.From = shadow.oldEffect.ShadowDepth;
                        shadow.shadowDepthAnimation.To = shadow.newEffect.ShadowDepth;
                        shadow.shadowDepthAnimation.Duration = shadow.duration;

                        shadow.blurRadiusAnimation.From = shadow.oldEffect.BlurRadius;
                        shadow.blurRadiusAnimation.To = shadow.newEffect.BlurRadius;
                        shadow.blurRadiusAnimation.Duration = shadow.duration;

                        shadow.opacityAnimation.From = shadow.oldEffect.Opacity;
                        shadow.opacityAnimation.To = shadow.newEffect.Opacity;
                        shadow.opacityAnimation.Duration = shadow.duration;

                        DropShadowEffect currentDropShadowEffect = shadow.Effect as DropShadowEffect;

                        currentDropShadowEffect.BeginAnimation(DropShadowEffect.ShadowDepthProperty,
                            shadow.shadowDepthAnimation);
                        currentDropShadowEffect.BeginAnimation(DropShadowEffect.BlurRadiusProperty,
                            shadow.blurRadiusAnimation);
                        currentDropShadowEffect.BeginAnimation(DropShadowEffect.OpacityProperty,
                            shadow.opacityAnimation);
                    } else {
                        DropShadowEffect currentDropShadowEffect = shadow.Effect as DropShadowEffect;
                        currentDropShadowEffect.BlurRadius = shadow.newEffect.BlurRadius;
                        currentDropShadowEffect.ShadowDepth = shadow.newEffect.ShadowDepth;
                        currentDropShadowEffect.Opacity = shadow.newEffect.Opacity;
                    }
                } else {
                    // set ShadowEffect directly in the Designer
                    shadow.SetValue(EffectProperty, CloneDropShadowEffect(shadow.newEffect));
                }
            }
        }

        private static DropShadowEffect CloneDropShadowEffect(DropShadowEffect dropShadowEffect) {
            if (dropShadowEffect == null) {
                return null;
            }

            return new DropShadowEffect {
                BlurRadius = dropShadowEffect.BlurRadius,
                Color = dropShadowEffect.Color,
                Direction = dropShadowEffect.Direction,
                Opacity = dropShadowEffect.Opacity,
                RenderingBias = dropShadowEffect.RenderingBias,
                ShadowDepth = dropShadowEffect.ShadowDepth
            };
        }

        internal void SetShadowEffect() {
            SetValue(EffectProperty, CloneDropShadowEffect(shadowsDictionary[ShadowDepth]));
        }
    }
}