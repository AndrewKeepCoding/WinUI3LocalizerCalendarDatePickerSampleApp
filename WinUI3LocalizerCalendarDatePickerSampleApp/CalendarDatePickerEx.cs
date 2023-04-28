using Microsoft.UI.Xaml.Controls;

namespace WinUI3LocalizerCalendarDatePickerSampleApp;

public sealed class CalendarDatePickerEx : CalendarDatePicker
{
    public CalendarDatePickerEx()
    {
        DefaultStyleKey = typeof(CalendarDatePickerEx);
    }

    public CalendarViewEx? CalendarView { get; private set; }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        CalendarView = GetTemplateChild(nameof(CalendarView)) as CalendarViewEx;
    }
}