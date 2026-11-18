using System.Threading.Tasks;

namespace Counter4E_KS;

public partial class AddCounterPage : ContentPage
{
	public AddCounterPage()
	{
		InitializeComponent();
	}

    private async void OnAddCounterClicked(object sender, EventArgs e)
    {
		string name = CounterNameEntry.Text;
        int value = int.Parse(CounterValueEntry.Text);

        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlert("B³¹d", "WprowadŸ nazwê licznika", "OK");
            return;
        }

        AllCountersPage.Counters.Add(new Models.Counter
        {
            Name = name,
            Value = value
        });

        AllCountersPage.SaveCounters();
        await Navigation.PopAsync();
    }

    private void numbersFiltration(object sender, TextChangedEventArgs e)
    {

        if (sender is Entry entry && !string.IsNullOrEmpty(e.NewTextValue))
        {
            string text = e.NewTextValue;

            bool IsNegative = text.StartsWith("-");

            string digits = new string(text.Where(char.IsDigit).ToArray());

            string result = (IsNegative ? "-" : "") + digits;

            if (text != result)
            {
                entry.Text = result;
            }
        }
    }
}