using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using System.Linq;
using WinUI3Localizer;

namespace WinUI3LocalizerCalendarDatePickerSampleApp;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        this.Loaded += MainPage_Loaded;
    }

    private ILocalizer Localizer { get; set; } = WinUI3Localizer.Localizer.Get();

    private static CalendarView? GetCalendarViewFormCalendarDatePicker(CalendarDatePicker calendarDatePicker)
    {
        return VisualTreeHelper.GetOpenPopupsForXamlRoot(calendarDatePicker.XamlRoot)
            .FirstOrDefault() is Popup popup &&
            popup.FindChild<CalendarView>() is CalendarView calendarView
                ? calendarView
                : null;
    }

    private async void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        LanguageDictionary englishDictionary = new("en-US");
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Su", DependencyPropertyName: "", Value: "SUN", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Mo", DependencyPropertyName: "", Value: "MON", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Tu", DependencyPropertyName: "", Value: "TUE", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "We", DependencyPropertyName: "", Value: "WED", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Th", DependencyPropertyName: "", Value: "THU", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Fr", DependencyPropertyName: "", Value: "FRI", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Sa", DependencyPropertyName: "", Value: "SAT", StringResourceItemName: ""));

        LanguageDictionary frenchDictionary = new("fr-FR");
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Su", DependencyPropertyName: "", Value: "DIM", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Mo", DependencyPropertyName: "", Value: "LUN", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Tu", DependencyPropertyName: "", Value: "MAR", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "We", DependencyPropertyName: "", Value: "MER", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Th", DependencyPropertyName: "", Value: "JEU", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Fr", DependencyPropertyName: "", Value: "VEN", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Sa", DependencyPropertyName: "", Value: "SAM", StringResourceItemName: ""));

        Localizer = await new LocalizerBuilder()
            .AddLanguageDictionary(englishDictionary)
            .AddLanguageDictionary(frenchDictionary)
            .Build();
    }

    private void CalendarDatePicker_Opened(object sender, object e)
    {
        if (sender is CalendarDatePicker calendarDatePicker &&
            GetCalendarViewFormCalendarDatePicker(calendarDatePicker) is CalendarView calendarView)
        {
            calendarView.Loaded += CalendarView_Loaded;
        }
    }

    private void CalendarView_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is CalendarView calendarView)
        {
            calendarView.Loaded -= CalendarView_Loaded;

            foreach (TextBlock textBlock in calendarView.FindDescendants().OfType<TextBlock>())
            {
                var ui = WinUI3Localizer.Uids.GetUid(textBlock);

                if (string.IsNullOrEmpty(ui) is true)
                {
                    WinUI3Localizer.Uids.SetUid(textBlock, textBlock.Text);
                }

                if (Localizer.GetLocalizedString(WinUI3Localizer.Uids.GetUid(textBlock)) is string localizedString &&
                    localizedString.Count() > 0)
                {
                    textBlock.Text = localizedString;
                }
            }
        }
    }

    private void LanguageRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string? selectedLanguage = ((sender as RadioButtons)?.SelectedItem as RadioButton)?.Tag as string;

        if (string.IsNullOrEmpty(selectedLanguage) is false)
        {
            Localizer.SetLanguage(selectedLanguage);
        }
    }
}