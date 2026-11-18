using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Counter4E_KS.Models;

namespace Counter4E_KS;

public partial class AllCountersPage : ContentPage
{
    public static ObservableCollection<Counter> Counters = new ObservableCollection<Counter>();
    private static string folderPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "saved"));
    private static string filePath = Path.Combine(folderPath, "counters.txt");

    public AllCountersPage()
    {
        InitializeComponent();
        LoadCounters();
        CountersCollectionView.ItemsSource = Counters;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CountersCollectionView.ItemsSource = null;
        CountersCollectionView.ItemsSource = Counters;
    }

    public static void SaveCounters()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var lines = new List<string>();

        foreach (var counter in Counters)
        {
            lines.Add(counter.Name);
            lines.Add(counter.Value.ToString());
        }

        File.WriteAllLines(filePath, lines);
    }

    private void LoadCounters()
    {
        Counters.Clear();

        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i += 2)
            {
                if (i + 1 < lines.Length)
                {
                    string name = lines[i];

                    if (int.TryParse(lines[i + 1], out int value))
                    {
                        Counters.Add(new Counter
                        {
                            Name = name,
                            Value = value
                        });
                    }
                }
            }
        }
    }


    private void OnPlusClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Counter counter)
        {
            counter.Value++;
            SaveCounters();
            CountersCollectionView.ItemsSource = null;
            CountersCollectionView.ItemsSource = Counters;
        }
    }

    private void OnMinusClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Counter counter)
        {
            counter.Value--;
            SaveCounters();
            CountersCollectionView.ItemsSource = null;
            CountersCollectionView.ItemsSource = Counters;
        }
    }

    private async void OnGoToAddCounterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddCounterPage());
    }
}