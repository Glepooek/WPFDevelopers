﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WPFDevelopers.Controls
{
    [TemplatePart(Name = ArcSegmentTemplateName, Type = typeof(ArcSegment))]
    [TemplatePart(Name = ArcSegmentAngleTemplateName, Type = typeof(ArcSegment))]
    [TemplatePart(Name = PathFigureTemplateName, Type = typeof(PathFigure))]
    [TemplatePart(Name = PathFigureAngleTemplateName, Type = typeof(PathFigure))]
    [TemplatePart(Name = TextBlockTemplateName, Type = typeof(TextBlock))]
    public class CircleProgressBar : ProgressBar
    {
        private const string ArcSegmentTemplateName = "PART_ArcSegment";
        private const string ArcSegmentAngleTemplateName = "PART_ArcSegmentAngle";
        private const string PathFigureTemplateName = "PART_PathFigure";
        private const string PathFigureAngleTemplateName = "PART_PathFigureAngle";
        private const string TextBlockTemplateName = "PART_TextBlock";
        private readonly Size _size = new Size(50,50);
        private ArcSegment _arcSegment, _arcSegmentAngle;
        private PathFigure _pathFigure, _pathFigureAngle;
        private TextBlock _textBlock;

        public static readonly DependencyProperty IsRoundProperty =
            DependencyProperty.Register("IsRound", typeof(bool), typeof(CircleProgressBar),
                new PropertyMetadata(false));

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(CircleProgressBar),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(CircleProgressBar),
                new PropertyMetadata(5.0));

        public static readonly DependencyProperty BrushStrokeThicknessProperty =
            DependencyProperty.Register("BrushStrokeThickness", typeof(double), typeof(CircleProgressBar),
                new PropertyMetadata(5.0));

        public CircleProgressBar()
        {
            ValueChanged += CircularProgressBar_ValueChanged;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _pathFigure = GetTemplateChild(PathFigureTemplateName) as PathFigure;
            _pathFigureAngle = GetTemplateChild(PathFigureAngleTemplateName) as PathFigure;
            _pathFigure.StartPoint = new Point(_size.Width, 0);
            _pathFigureAngle.StartPoint = new Point(_size.Width, 0);
            _arcSegment = GetTemplateChild(ArcSegmentTemplateName) as ArcSegment;
            _arcSegment.Size = _size;
            _arcSegment.Point = new Point(_size.Width - 0.000872664626, 7.61543361704753E-09);
            _arcSegmentAngle = GetTemplateChild(ArcSegmentAngleTemplateName) as ArcSegment;
            _arcSegmentAngle.Size = _size;
            _textBlock = GetTemplateChild(TextBlockTemplateName) as TextBlock;
        }

        public bool IsRound
        {
            get => (bool)GetValue(IsRoundProperty);
            set => SetValue(IsRoundProperty, value);
        }

        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            private set => SetValue(AngleProperty, value);
        }

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public double BrushStrokeThickness
        {
            get => (double)GetValue(BrushStrokeThicknessProperty);
            set => SetValue(BrushStrokeThicknessProperty, value);
        }

        private void CircularProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var bar = sender as CircleProgressBar;
            var currentAngle = bar.Angle;
            var targetAngle = e.NewValue / bar.Maximum * 359.999;
            var anim = new DoubleAnimation(currentAngle, targetAngle, TimeSpan.FromMilliseconds(500));
            bar.BeginAnimation(AngleProperty, anim, HandoffBehavior.SnapshotAndReplace);
        }
    }
}