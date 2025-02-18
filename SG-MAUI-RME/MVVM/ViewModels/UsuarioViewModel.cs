using PropertyChanged;
using SG_MAUI_RME.MVVM.Models;
using SG_MAUI_RME.MVVM.Views;
using System.Windows.Input;

namespace SG_MAUI_RME.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class UsuarioViewModel
    {
        public Usuario UsuarioLog { get; set; } = new Usuario();
        public ICommand LoginCommand { get; set; }
        public ICommand LimpiarLoginCommand { get; set; }

        /*
         * Commands:
         * - LoginCommand: Valida el usuario y contraseña. Si no hay usuarios registrados, guarda el usuario y loguea
         * - LimpiarLoginCommand: Limpia los campos
         */
        public UsuarioViewModel()
        {
            LoginCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(UsuarioLog.Name) || string.IsNullOrEmpty(UsuarioLog.Passwd))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ponga los campos mamahuevo", "OK");
                    return;
                }
                else
                {
                    if (App.UsuarioRepositorio.GetItems().Count == 0)
                    {
                        App.UsuarioRepositorio.SaveItemCascada(UsuarioLog);
                        await Application.Current.MainPage.DisplayAlert("Información", "Usuario guardado. Bienvenido " + UsuarioLog.Name, "OK");

                        App.Current.MainPage = new NavigationPage(new PaginaPrincipal());
                    }
                    else
                    {
                        var usuario = App.UsuarioRepositorio.Login(UsuarioLog.Name, UsuarioLog.Passwd);

                        if (usuario != null)
                        {
                            await Application.Current.MainPage.DisplayAlert("Éxito", "Bienvenido " + UsuarioLog.Name, "OK");
                            App.Current.MainPage = new NavigationPage(new PaginaPrincipal());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario no encontrado", "OK");
                        }
                    }
                }
            });

            LimpiarLoginCommand = new Command(() =>
            {
                UsuarioLog = new Usuario();
            });
        }
    }
}
