using PropertyChanged;
using SG_MAUI_RME.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_MAUI_RME.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class UsuarioViewModel
    {
        public List<Usuario> Usuarios { get; set; }

        public UsuarioViewModel()
        {
        }
    }
}
