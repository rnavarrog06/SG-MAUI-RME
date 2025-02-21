using PropertyChanged;
using SG_MAUI_RME.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SG_MAUI_RME.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GestionUsuariosViewModel
    {
        public ObservableCollection<Usuario> Usuarios { get; set; }

        public ICommand GuardarCommand { get; set; }

        public ICommand EliminarCommand { get; set; }

        public ICommand AddEmailCommand { get; set; }

        public ICommand EliminarEmailCommand { get; set; }

        public Usuario UsuarioSeleccionado { get; set; }

        public List<Emails> EmailEliminados = new List<Emails>();

        public Emails EmailSeleccionado { get; set; }

        public GestionUsuariosViewModel()
        {
            RefreshView();
            EmailSeleccionado = new Emails();

            GuardarCommand = new Command(async () =>
            {
                App.UsuarioRepositorio.SaveItemCascada(UsuarioSeleccionado);
                Console.WriteLine(App.UsuarioRepositorio.StatusMessage);
                if (EmailEliminados.Count > 0)
                {
                    List<Emails> EmailBd = App.EmailRepositorio.GetItems();
                    foreach (var email in EmailEliminados)
                    {
                        if (EmailBd.Contains(email))
                        {
                            App.EmailRepositorio.DeleteItem(email);
                        }
                        
                    }
                    EmailEliminados.Clear();
                }
                
                RefreshView();

            });

            EliminarCommand = new Command(async () =>
            {
                App.UsuarioRepositorio.DeleteItem(UsuarioSeleccionado);
                Console.WriteLine(App.UsuarioRepositorio.StatusMessage);
                RefreshView();

            });

            AddEmailCommand = new Command(async () =>
            {
                if (UsuarioSeleccionado.Emails == null)
                {
                    UsuarioSeleccionado.Emails = new ObservableCollection<Emails>();
                }

                if (!string.IsNullOrWhiteSpace(EmailSeleccionado.correo))
                {
                    // Expresión regular para validar el formato de correo electrónico
                    string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                    if (Regex.IsMatch(EmailSeleccionado.correo, emailPattern))
                    {
                        UsuarioSeleccionado.Emails.Add(EmailSeleccionado);
                        EmailSeleccionado = new Emails();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "El correo ingresado no es válido.", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El campo de correo no puede estar vacío.", "OK");
                }
            });

            EliminarEmailCommand = new Command(async () =>
            {
                if (UsuarioSeleccionado != null && EmailSeleccionado != null)
                {
                    UsuarioSeleccionado.Emails.Remove(EmailSeleccionado);
                    EmailEliminados.Add(EmailSeleccionado);
                    EmailSeleccionado = new Emails(); // Desvincular después de eliminar
                    
                }
            });

        }

        private void RefreshView()
        {
            Usuarios  = new ObservableCollection<Usuario>(App.UsuarioRepositorio.GetItemsCascada());
            UsuarioSeleccionado = new Usuario();
            
        }

    }
}
