using SG_MAUI_RME.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SG_MAUI_RME.MVVM.ViewModels
{
    public class PaginaPrincipalViewModel
    {
        
        public ICommand GestionUsuariosCommand => new Command(() =>
        {
            //recomendacion pasar el usuario logueado entre las ventanas
            App.Current.MainPage.Navigation.PushAsync(new GestionUsuariosView());
        });

        public ICommand SalirCommand => new Command(async () =>
        {
            bool salir = await App.Current.MainPage.DisplayAlert("Salir", "¿Estás seguro de que deseas salir?", "Sí", "No");
            if (salir)
            {
                System.Environment.Exit(0);
            }
        });
    }
}
