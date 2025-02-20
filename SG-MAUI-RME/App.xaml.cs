using SG_MAUI_RME.MVVM.Models;
using SG_MAUI_RME.Repositories;

using SG_MAUI_RME.MVVM.Views;

namespace SG_MAUI_RME
{
    public partial class App : Application
    {
        public static BaseRepository<Usuario> UsuarioRepositorio { get; private set; }

        public static BaseRepository<Emails> EmailRepositorio { get; private set; }

        public App(BaseRepository<Usuario> objUsuarioRepo, BaseRepository<Emails> objEmailRepo)
        {
            InitializeComponent();


            UsuarioRepositorio = objUsuarioRepo;
            EmailRepositorio = objEmailRepo;


            MainPage = new NavigationPage(new LoginView());

        }
    }
}
