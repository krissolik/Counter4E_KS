namespace Counter4E_KS
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new AllCountersPage());
        }
    }
}