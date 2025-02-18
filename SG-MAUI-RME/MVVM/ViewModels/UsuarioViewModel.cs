using PropertyChanged;
using SG_MAUI_RME.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SG_MAUI_RME.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class UsuarioViewModel
    {
        public List<Usuario> Usuarios { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand LimpiarLoginCommand { get; set; }

        public Usuario Usuario { get; set; }

        public UsuarioViewModel()
        {
            LoginCommand = new Command(async () =>
            {
                App.UsuarioRepositorio.Login(Usuario.Name, Usuario.Passwd);
                Console.WriteLine(App.UsuarioRepositorio.StatusMessage);
            });

            LimpiarLoginCommand = new Command(() =>
            {
                //Limpiar los dos campos
                Usuario.Name = string.Empty;
            });
        }
    }
}
