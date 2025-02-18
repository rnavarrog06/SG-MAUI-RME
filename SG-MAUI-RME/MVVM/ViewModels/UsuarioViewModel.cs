using PropertyChanged;
using SG_MAUI_RME.MVVM.Models;
using System.Windows.Input;

namespace SG_MAUI_RME.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class UsuarioViewModel
    {
        public Usuario UsuarioLog { get; set; } = new Usuario();
        public ICommand LoginCommand { get; set; }
        public ICommand LimpiarLoginCommand { get; set; }

        public UsuarioViewModel()
        {
            LoginCommand = new Command(async () =>
            {
                if (App.UsuarioRepositorio.GetItems().Count == 0)
                {
                    App.UsuarioRepositorio.SaveItemCascada(UsuarioLog);
                    await Application.Current.MainPage.DisplayAlert("Información", "Usuario guardado", "OK");

                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    App.UsuarioRepositorio.Login(UsuarioLog.Name, UsuarioLog.Passwd);

                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
            });
            LimpiarLoginCommand = new Command(() =>
            {
                UsuarioLog = new Usuario();
            });
        }
    }
}
