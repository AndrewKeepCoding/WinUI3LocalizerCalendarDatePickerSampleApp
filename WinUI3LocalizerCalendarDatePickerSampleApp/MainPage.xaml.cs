using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

    private List<string> MonthNames { get; } = new()
    {
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec",
    };

    private IList<TextBlock>? MonthControls { get; set; }

    private static LanguageDictionary CreateEnglishDictionary()
    {
        LanguageDictionary englishDictionary = new("en-US");
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay1", DependencyPropertyName: "", Value: "Sun", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay2", DependencyPropertyName: "", Value: "Mon", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay3", DependencyPropertyName: "", Value: "Tue", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay4", DependencyPropertyName: "", Value: "Wed", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay5", DependencyPropertyName: "", Value: "Thu", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay6", DependencyPropertyName: "", Value: "Fri", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay7", DependencyPropertyName: "", Value: "Sat", StringResourceItemName: ""));

        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Jan", DependencyPropertyName: "", Value: "Janu", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Feb", DependencyPropertyName: "", Value: "Feb", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Mar", DependencyPropertyName: "", Value: "Mar", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Apr", DependencyPropertyName: "", Value: "Apr", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "May", DependencyPropertyName: "", Value: "May", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Jun", DependencyPropertyName: "", Value: "Jun", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Jul", DependencyPropertyName: "", Value: "Jul", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Aug", DependencyPropertyName: "", Value: "Aug", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Sep", DependencyPropertyName: "", Value: "Sep", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Oct", DependencyPropertyName: "", Value: "Oct", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Nov", DependencyPropertyName: "", Value: "Nov", StringResourceItemName: ""));
        englishDictionary.AddItem(new LanguageDictionary.Item(Uid: "Dec", DependencyPropertyName: "", Value: "Dec", StringResourceItemName: ""));
        return englishDictionary;
    }

    private static LanguageDictionary CreateFrenchDictionary()
    {
        LanguageDictionary frenchDictionary = new("fr-FR");
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay1", DependencyPropertyName: "", Value: "dim", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay2", DependencyPropertyName: "", Value: "lun", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay3", DependencyPropertyName: "", Value: "mar", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay4", DependencyPropertyName: "", Value: "mer", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay5", DependencyPropertyName: "", Value: "jeu", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay6", DependencyPropertyName: "", Value: "ven", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "WeekDay7", DependencyPropertyName: "", Value: "sam", StringResourceItemName: ""));

        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "January", DependencyPropertyName: "", Value: "janvier", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "February", DependencyPropertyName: "", Value: "février", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "March", DependencyPropertyName: "", Value: "mars", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "April", DependencyPropertyName: "", Value: "avril", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "May", DependencyPropertyName: "", Value: "mai", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "June", DependencyPropertyName: "", Value: "juin", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "July", DependencyPropertyName: "", Value: "juillet", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "August", DependencyPropertyName: "", Value: "aout", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "September", DependencyPropertyName: "", Value: "septembre", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "October", DependencyPropertyName: "", Value: "octobre", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "November", DependencyPropertyName: "", Value: "novembre", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "December", DependencyPropertyName: "", Value: "décembre", StringResourceItemName: ""));

        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Jan", DependencyPropertyName: "", Value: "janvier", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Feb", DependencyPropertyName: "", Value: "février", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Mar", DependencyPropertyName: "", Value: "mars", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Apr", DependencyPropertyName: "", Value: "avril", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "May", DependencyPropertyName: "", Value: "mai", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Jun", DependencyPropertyName: "", Value: "juin", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Jul", DependencyPropertyName: "", Value: "juillet", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Aug", DependencyPropertyName: "", Value: "aout", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Sep", DependencyPropertyName: "", Value: "septembre", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Oct", DependencyPropertyName: "", Value: "octobre", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Nov", DependencyPropertyName: "", Value: "novembre", StringResourceItemName: ""));
        frenchDictionary.AddItem(new LanguageDictionary.Item(Uid: "Dec", DependencyPropertyName: "", Value: "décembre", StringResourceItemName: ""));
        return frenchDictionary;
    }

    private async void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        if (Localizer is NullLocalizer)
        {
            Localizer = await new LocalizerBuilder()
                .AddLanguageDictionary(CreateEnglishDictionary())
                .AddLanguageDictionary(CreateFrenchDictionary())
                .Build();

            if (this.CalendarDatePickerExControl.CalendarView is not null)
            {
                this.CalendarDatePickerExControl.Opened += CalendarDatePickerExControl_Opened;
                this.CalendarDatePickerExControl.CalendarView.HeaderButtonTextChanged += CalendarView_HeaderButtonTextChanged;
            }
        }
    }

    private void CalendarView_HeaderButtonTextChanged(object? sender, string e)
    {
        LocalizeCalendarDatePicker();
    }

    private void LocalizeCalendarDatePicker()
    {
        LocalizeHeaderControls();
        LocalizeDayControls();
        // Weird behavior when trying to localize month controls.
        // For example, after just localized "January", somehow "February" is changed to "April".
        //LocalizeMonthControls();
    }

    private async void CalendarDatePickerExControl_Opened(object? sender, object e)
    {
        await Task.Delay(3000);
        LocalizeCalendarDatePicker();
    }

    private void LocalizeHeaderControls()
    {
        if (this.CalendarDatePickerExControl.CalendarView is CalendarView calendarView &&
            calendarView.FindDescendant<Button>(x => x.Name is "HeaderButton") is Button headerButton)
        {
            string requiredHeaderText = calendarView.TemplateSettings.HeaderText;

            if (calendarView.DisplayMode is CalendarViewDisplayMode.Month &&
                MonthNames.FirstOrDefault(x => requiredHeaderText.Contains(x)) is string monthName &&
                Localizer.GetLocalizedString(monthName) is string localizedString)
            {
                // Extract a year of 4 digits.
                string year = Regex.Match(requiredHeaderText, @"\d{4}").Value;
                headerButton.Content = $"{localizedString} {year}";
                return;
            }

            headerButton.Content = requiredHeaderText;
        }
    }

    private void LocalizeDayControls()
    {
        if (this.CalendarDatePickerExControl.CalendarView is CalendarView calendarView &&
            calendarView.DisplayMode is CalendarViewDisplayMode.Month &&
            calendarView
                .FindDescendants()
                .OfType<TextBlock>()
                .Where(x => x.Name.Contains("WeekDay"))
                .ToList() is IEnumerable<TextBlock> daysTextBlocks)
        {
            foreach (TextBlock dayTextBlock in daysTextBlocks)
            {
                if (Localizer.GetLocalizedString(dayTextBlock.Name) is string localizedString)
                {
                    dayTextBlock.Text = localizedString;
                }
            }
        }
    }

    private void LocalizeMonthControls()
    {
        MonthControls ??= GetMonthControls();

        if (MonthControls is null)
        {
            return;
        }

        foreach (TextBlock monthTextBlock in MonthControls)
        {
            if (WinUI3Localizer.Uids.GetUid(monthTextBlock) is not string uid)
            {
                uid = monthTextBlock.Text;
                WinUI3Localizer.Uids.SetUid(monthTextBlock, monthTextBlock.Text);
            }

            if (Localizer.GetLocalizedString(uid) is string localizedString)
            {
                string prev = monthTextBlock.Text;
                monthTextBlock.Text = localizedString;
                monthTextBlock.Tag = localizedString;
            }
        }
    }

    private IList<TextBlock>? GetMonthControls()
    {
        return this.CalendarDatePickerExControl.CalendarView is CalendarView calendarView &&
            calendarView.DisplayMode is CalendarViewDisplayMode.Year
                ? calendarView.FindDescendants()
                    .OfType<TextBlock>()
                    .Where(x => MonthNames.Contains(x.Text))
                    .ToList()
                : null;
    }

    private void LanguagesRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        string? selectedLanguage = (sender as RadioButton)?.Tag as string;

        if (string.IsNullOrEmpty(selectedLanguage) is false)
        {
            Localizer.SetLanguage(selectedLanguage);
            LocalizeCalendarDatePicker();
        }
    }
}