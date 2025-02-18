using SG_MAUI_RME.MVVM.Models;
using SG_MAUI_RME.Repositories;

using SG_MAUI_RME.MVVM.Views;

namespace SG_MAUI_RME
{
    public partial class App : Application
    {
        public static BaseRepository<Usuario> UsuarioRepositorio { get; private set; }
        public App(BaseRepository<Usuario> objUsuarioRepo)
        {
            InitializeComponent();

<<<<<<< HEAD
=======
            UsuarioRepositorio = objUsuarioRepo;
>>>>>>> 45aa47d5884500ec5f981c9c259b0feabaf03f63

            MainPage = new NavigationPage(new LoginView());

        }
    }
}
