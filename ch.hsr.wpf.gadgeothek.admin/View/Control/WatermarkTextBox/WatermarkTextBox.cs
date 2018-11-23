using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ch.hsr.wpf.gadgeothek.admin.View.Control.WatermarkTextBox {
    /// <summary>
    /// A TextBox with a Watermark (Placeholder/Label).
    /// <para>
    /// I could not figure out how to calcualte / Bind the distance the Watermark has to move when <see cref="WatermarkBehavior"/> is MoveOnFocus.
    /// It is HardCoded! A change of Margin / Padding / Aligment will have wrong behavior!!
    /// </para>
    /// </summary>
    class WatermarkTextBox : TextBox
    {
        /// <summary>
        /// Identifies the WatermarkTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty WatermarkTemplateProperty =
            DependencyProperty.Register("WatermarkTemplate", typeof(DataTemplate), typeof(WatermarkTextBox), null);

        /// <summary>
        /// Identifies the WatermarkContent dependency property.
        /// </summary>
        public static readonly DependencyProperty WatermarkContentProperty =
            DependencyProperty.Register("WatermarkContent", typeof(object), typeof(WatermarkTextBox), null);

        /// <summary>
        /// Identifies the CurrentText dependency property.
        /// </summary>
        public static readonly DependencyProperty CurrentTextProperty =
            DependencyProperty.Register("CurrentText", typeof(string), typeof(WatermarkTextBox), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnCurrentTextChanged)));
        
        /// <summary>
        /// Identifies the WatermarkBehavior property.
        /// </summary>
        public static readonly DependencyProperty WatermarkBehaviorProperty =
            DependencyProperty.Register("WatermarkBehavior", typeof(WatermarkBehavior), typeof(WatermarkTextBox), new PropertyMetadata(WatermarkBehavior.HiddenWhenFocused));

        /// <summary>
        /// Identifies the IsWatermarkVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty IsWatermarkVisibleProperty;
        private static readonly DependencyPropertyKey IsWatermarkVisiblePropertyKey =
            DependencyProperty.RegisterReadOnly("IsWatermarkVisible", typeof(bool), typeof(WatermarkTextBox), new PropertyMetadata(OnIsWatermarkVisibleChanged));

        /// <summary>
        /// Identifies the IsWatermarkMoved dependency property.
        /// </summary>
        public static readonly DependencyProperty IsWatermarkMovedProperty;
        private static readonly DependencyPropertyKey IsWatermarkMovedPropertyKey =
            DependencyProperty.RegisterReadOnly("IsWatermarkMoved", typeof(bool), typeof(WatermarkTextBox), new PropertyMetadata(OnIsWatermarkVisibleChanged));

        private const string WatermarkHiddenState = "WatermarkHidden";
        private const string WatermarkVisibleState = "WatermarkVisible";
        private const string WatermarkMovedState = "WatermarkMoved";

        private bool _isTextPropertiesChanging;
        private string _textPreInitValue = string.Empty;

        static WatermarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkTextBox), new FrameworkPropertyMetadata(typeof(WatermarkTextBox)));
            IsWatermarkVisibleProperty = IsWatermarkVisiblePropertyKey.DependencyProperty;
            IsWatermarkMovedProperty = IsWatermarkMovedPropertyKey.DependencyProperty;
        }

        /// <summary>
        /// Initializes a new instance of the RadWatermarkTextBox class.
        /// </summary>
        public WatermarkTextBox()
        {
            this.GotFocus += this.OnGotFocus;
            this.LostFocus += this.OnLostFocus;
            this.Loaded += this.OnLoaded;
            this.TextChanged += this.OnTextChanged;

            this.AddHandler(WatermarkTextBox.MouseLeftButtonDownEvent, new System.Windows.Input.MouseButtonEventHandler(this.OnMouseLeftButtonDown), true);
        }

        /// <summary>
        /// Gets or sets the content to be shown when the TextBox is empty and not focused.
        /// </summary>
        public object WatermarkContent
        {
            get => GetValue(WatermarkContentProperty);
            set => SetValue(WatermarkContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the template for presenting the content, shown when the TextBox is empty and not focused.
        /// </summary>
        public DataTemplate WatermarkTemplate
        {
            get => (DataTemplate)GetValue(WatermarkTemplateProperty);
            set => SetValue(WatermarkTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the text of the TextBox.
        /// This property is meant to be used for TwoWay binding in order to be 
        /// updated on each change of the text and not when the focus is lost.
        /// </summary>
        public string CurrentText
        {
            get => (string)GetValue(CurrentTextProperty);
            set => this.SetCurrentValue(CurrentTextProperty, value);
        }

        /// <summary>
        /// Gets a value indicating whether the Watermark is visible or not.
        /// </summary>
        public bool IsWatermarkVisible
        {
            get => (bool)GetValue(IsWatermarkVisibleProperty);
            private set => this.SetValue(IsWatermarkVisiblePropertyKey, value);
        }

        /// <summary>
        /// Gets a value indicating whether the Watermark is moved or not.
        /// </summary>
        public bool IsWatermarkMoved
        {
            get => (bool)GetValue(IsWatermarkMovedProperty);
            private set => this.SetValue(IsWatermarkMovedPropertyKey, value);
        }

        /// <summary>
        /// Gets a value that specifies when the watermark content of control will be hidden.
        /// </summary>
        public WatermarkBehavior WatermarkBehavior
        {
            get => (WatermarkBehavior)GetValue(WatermarkBehaviorProperty);
            set => this.SetValue(WatermarkBehaviorProperty, value);
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code
        /// or internal processes (such as a rebuilding layout pass) call System.Windows.Controls.Control.ApplyTemplate().
        /// In simplest terms, this means the method is called just before a UI element
        /// displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateCurrentText();
            this.UpdateWatermarkVisibility();

            this.UpdateVisualState(false);
        }

        private void OnMouseLeftButtonDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.WatermarkBehavior == WatermarkBehavior.HideOnClick)
            {
                this.IsWatermarkVisible = false;
            }
        }

        private void UpdateCurrentText()
        {
            if (!this._isTextPropertiesChanging)
            {
                this._isTextPropertiesChanging = true;
                try
                {
                    this.CurrentText = this.Text;
                }
                finally
                {
                    this._isTextPropertiesChanging = false;
                }
            }
        }

        /// <summary>
        /// Indicates that the initialization process for the element is complete.
        /// </summary>
        public override void EndInit()
        {
            base.EndInit();
            if (this._textPreInitValue != this.Text)
            {
                this.SetCurrentValue(TextProperty, this._textPreInitValue);
            }

        }

        private static void OnIsWatermarkVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WatermarkTextBox)d).UpdateVisualState(true);
        }

        private static void OnCurrentTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WatermarkTextBox watermark)
            {
                if (watermark.CurrentText == null && !watermark._isTextPropertiesChanging)
                {
                    if (watermark.IsInitialized)
                    {
                    watermark._isTextPropertiesChanging = true;
                    try
                    {
                        watermark.SetCurrentValue(TextProperty, watermark.CurrentText);
                    }
                    finally
                    {
                        watermark._isTextPropertiesChanging = false;
                    }
                }
                }
                watermark._textPreInitValue = watermark.CurrentText;
                watermark.UpdateWatermarkVisibility();
            }
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (this.WatermarkBehavior == WatermarkBehavior.HiddenWhenFocused)
            {
                this.IsWatermarkVisible = false;
            }
            if (this.WatermarkBehavior == WatermarkBehavior.MoveWhenFocused)
            {
                this.IsWatermarkMoved = true;
            }
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            this.UpdateWatermarkVisibility();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateCurrentText();
            this.UpdateWatermarkVisibility();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateCurrentText();
            this.UpdateWatermarkVisibility();
        }

        private void UpdateWatermarkVisibility()
        {
            if (this.WatermarkBehavior == WatermarkBehavior.HiddenWhenFocused) {
                this.IsWatermarkVisible = string.IsNullOrEmpty(this.CurrentText) && !this.IsKeyboardFocusWithin;
            } else if (this.WatermarkBehavior != WatermarkBehavior.MoveWhenFocused) {
                this.IsWatermarkVisible = string.IsNullOrEmpty(this.CurrentText);
            } else {
                this.IsWatermarkVisible = true;
                this.IsWatermarkMoved = !string.IsNullOrEmpty(this.CurrentText) || this.IsKeyboardFocusWithin;
            }
        }

        private void UpdateVisualState(bool useTransitions)
        {
            if (this.IsWatermarkMoved) {
                VisualStateManager.GoToState(this, WatermarkMovedState, useTransitions);
            } else if (this.IsWatermarkVisible) {
                VisualStateManager.GoToState(this, WatermarkVisibleState, useTransitions);
            } else {
                VisualStateManager.GoToState(this, WatermarkHiddenState, useTransitions);
            }
        }
    }
}
