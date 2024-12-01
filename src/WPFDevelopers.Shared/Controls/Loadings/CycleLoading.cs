﻿using System.Windows;
using System.Windows.Controls;

namespace WPFDevelopers.Controls
{
    public class CycleLoading : LoadingBase
    {
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(CycleLoading), new PropertyMetadata(100d));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(CycleLoading),
                new PropertyMetadata(0d, OnValuePropertyChangedCallBack));

        internal static readonly DependencyProperty ValueDescriptionProperty =
            DependencyProperty.Register("ValueDescription", typeof(string), typeof(CycleLoading),
                new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LoadTitleProperty =
            DependencyProperty.Register("LoadTitle", typeof(string), typeof(CycleLoading),
                new PropertyMetadata(default(string)));

        static CycleLoading()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CycleLoading),
                new FrameworkPropertyMetadata(typeof(CycleLoading)));
        }

        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        internal string ValueDescription
        {
            get => (string)GetValue(ValueDescriptionProperty);
            set => SetValue(ValueDescriptionProperty, value);
        }

        public string LoadTitle
        {
            get => (string)GetValue(LoadTitleProperty);
            set => SetValue(LoadTitleProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        private static void OnValuePropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is CycleLoading loading))
                return;
            if (!double.TryParse(e.NewValue?.ToString(), out var value))
                return;
            if (value >= loading.MaxValue)
            {
                value = loading.MaxValue;
                if (loading.IsLoading)
                    loading.IsLoading = false;
            }
            else
            {
                if (!loading.IsLoading)
                    loading.IsLoading = true;
            }
            var dValue = value / loading.MaxValue;
            loading.ValueDescription = dValue.ToString("P0");
        }
    }
}