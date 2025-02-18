using SG_MAUI_RME.MVVM.Models;
using SG_MAUI_RME.Repositories;

namespace SG_MAUI_RME
{
    public partial class App : Application
    {
        public static BaseRepository<Usuario> UsuarioRepositorio { get; private set; }
        public App(BaseRepository<Usuario> objUsuarioRepo)
        {
            InitializeComponent();

            UsuarioRepositorio = objUsuarioRepo;

            MainPage = new AppShell();
        }
    }
}
