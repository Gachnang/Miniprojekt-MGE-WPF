using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ch.hsr.wpf.gadgeothek.admin.View.Control.Animation {
    /// <summary>
    ///     Animates a Brush to another Brush.
    ///     <para>Source: https://stackoverflow.com/questions/8096852/brush-to-brush-animation </para>
    /// </summary>
    public class BrushAnimation : AnimationTimeline {
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(Brush), typeof(BrushAnimation));

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(Brush), typeof(BrushAnimation));

        public override Type TargetPropertyType => typeof(Brush);

        //we must define From and To, AnimationTimeline does not have this properties
        public Brush From {
            get => (Brush) GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public Brush To {
            get => (Brush) GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public override object GetCurrentValue(object defaultOriginValue,
            object defaultDestinationValue,
            AnimationClock animationClock) {
            return GetCurrentValue(defaultOriginValue as Brush,
                defaultDestinationValue as Brush,
                animationClock);
        }

        public object GetCurrentValue(Brush defaultOriginValue,
            Brush defaultDestinationValue,
            AnimationClock animationClock) {
            if (!animationClock.CurrentProgress.HasValue) {
                return Brushes.Transparent;
            }

            //use the standard values if From and To are not set 
            //(it is the value of the given property)
            defaultOriginValue = From ?? defaultOriginValue;
            defaultDestinationValue = To ?? defaultDestinationValue;

            if (animationClock.CurrentProgress.Value == 0) {
                return defaultOriginValue;
            }

            if (animationClock.CurrentProgress.Value == 1) {
                return defaultDestinationValue;
            }

            return new VisualBrush(new Border {
                Width = 1,
                Height = 1,
                Background = defaultOriginValue,
                Child = new Border {
                    Background = defaultDestinationValue,
                    Opacity = animationClock.CurrentProgress.Value
                }
            });
        }


        protected override Freezable CreateInstanceCore() {
            return new BrushAnimation();
        }

        public static void SetFrom(DependencyObject element, Brush value) {
            element.SetValue(FromProperty, value);
        }

        public static Brush GetFrom(DependencyObject element) {
            return (Brush) element.GetValue(FromProperty);
        }

        public static void SetTo(DependencyObject element, Brush value) {
            element.SetValue(ToProperty, value);
        }

        public static Brush GetTo(DependencyObject element) {
            return (Brush) element.GetValue(ToProperty);
        }
    }
}