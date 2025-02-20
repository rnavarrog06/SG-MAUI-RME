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
    public class GestionUsuariosViewModel
    {
        public List<Usuario> Usuarios { get; set; }

        public ICommand GuardarCommand { get; set; }

        public ICommand EliminarCommand { get; set; }

        public Usuario UsuarioSeleccionado { get; set; }

        public GestionUsuariosViewModel()
        {
            RefreshView();

            GuardarCommand = new Command(async () =>
            {
               

            });

            EliminarCommand = new Command(async () =>
            {
                

            });

        }

        private void RefreshView()
        {
            Usuarios = App.UsuarioRepositorio.GetItemsCascada();
            UsuarioSeleccionado = new Usuario();
        }

    }
}
