using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Diagnostics;

namespace WinUI3LocalizerCalendarDatePickerSampleApp;

public sealed class CalendarViewEx : CalendarView
{
    //public event EventHandler<CalendarViewDisplayMode>? DisplayModeChanged;

    public event EventHandler<string>? HeaderButtonTextChanged;

    public event RoutedEventHandler? HeaderButtonClick;

    public event RoutedEventHandler? PreviousButtonClick;

    public event RoutedEventHandler? NextButtonClick;

    public Button? HeaderButton { get; private set; }

    private Button? PreviousButton { get; set; }

    private Button? NextButton { get; set; }

    private CalendarPanel? MonthViewPanel { get; set; }

    private CalendarPanel? YearViewPanel { get; set; }

    private string HeaderText { get; set; } = string.Empty;

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild(nameof(MonthViewPanel)) is CalendarPanel monthViewPanel)
        {
            MonthViewPanel = monthViewPanel;
            MonthViewPanel.LayoutUpdated += MonthViewPanel_LayoutUpdated;
        }

        if (GetTemplateChild(nameof(YearViewPanel)) is CalendarPanel yearViewPanel)
        {
            YearViewPanel = yearViewPanel;
            YearViewPanel.LayoutUpdated += YearViewPanel_LayoutUpdated;
        }

        if (HeaderButton is not null)
        {
            HeaderButton.Click -= HeaderButton_Click;
        }

        if (GetTemplateChild(nameof(HeaderButton)) is Button headerButton)
        {
            HeaderButton = headerButton;
        }

        if (PreviousButton is not null)
        {
            PreviousButton.Click -= PreviousButton_Click;
        }

        if (GetTemplateChild(nameof(PreviousButton)) is Button previousButton)
        {
            PreviousButton = previousButton;
        }

        if (NextButton is not null)
        {
            NextButton.Click -= NextButton_Click;
        }

        if (GetTemplateChild(nameof(NextButton)) is Button nextButton)
        {
            NextButton = nextButton;
        }
    }

    private void MonthViewPanel_LayoutUpdated(object? sender, object e)
    {
        if (HeaderButtonTextChanged is not null &&
            TemplateSettings.HeaderText != HeaderText)
        {
            HeaderText = TemplateSettings.HeaderText;
            HeaderButtonTextChanged.Invoke(HeaderButton, HeaderText);
        }
    }

    private void YearViewPanel_LayoutUpdated(object? sender, object e)
    {
        int i = 1;

        foreach (UIElement? child in YearViewPanel?.Children)
        {
            TextBlock descendants = child.FindDescendant<TextBlock>();
            descendants.Name = $"YearViewPanel_{i++}";
            descendants.RegisterPropertyChangedCallback(TextBlock.TextProperty, OnTextPropertyChanged);
        }
    }

    private void OnTextPropertyChanged(DependencyObject sender, DependencyProperty dp)
    {
        var t = sender as TextBlock;
        Debug.WriteLine($"{t.Name} OnTextPropertyChanged: {t.Text}");
    }

    private void HeaderButton_Click(object sender, RoutedEventArgs e)
    {
        HeaderButtonClick?.Invoke(sender, e);
    }

    private void PreviousButton_Click(object sender, RoutedEventArgs e)
    {
        PreviousButtonClick?.Invoke(sender, e);
    }

    private void NextButton_Click(object sender, RoutedEventArgs e)
    {
        NextButtonClick?.Invoke(sender, e);
    }
}