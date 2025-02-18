using SG_MAUI_RME.MVVM.Views;

namespace SG_MAUI_RME
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            MainPage = new NavigationPage(new LoginView());

        }
    }
}
