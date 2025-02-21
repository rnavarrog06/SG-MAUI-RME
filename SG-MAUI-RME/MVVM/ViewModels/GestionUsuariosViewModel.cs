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

        public ICommand CambiarImagenCommand { get; set; }
        public ICommand GuardarCommand { get; set; }

        public ICommand EliminarCommand { get; set; }

        public ICommand AddEmailCommand { get; set; }

        public ICommand EliminarEmailCommand { get; set; }

        //public Usuario UsuarioSeleccionado { get; set; }

        private Usuario _usuarioSeleccionado;
        public Usuario UsuarioSeleccionado
        {
            get => _usuarioSeleccionado;
            set
            {
                _usuarioSeleccionado = value;

                if (_usuarioSeleccionado != null)
                {
                    // Si la imagen no es nula, la cargamos desde el byte[]
                    if (_usuarioSeleccionado.Image != null)
                    {
                     
                        ImagenSeleccionada = ImageSource.FromStream(() => new MemoryStream(_usuarioSeleccionado.Image));
                    }
                    else
                    {
                        ImagenSeleccionada = ImageSource.FromFile("dotnet_bot.png"); // Imagen por defecto
                    }
                }
            }
        }

        public ImageSource ImagenSeleccionada { get; set; }

        public ImageSource ImagenUsuario { get; set; }

        public List<Emails> EmailEliminados = new List<Emails>();

        public Emails EmailSeleccionado { get; set; }

        public GestionUsuariosViewModel()
        {
            ImagenSeleccionada = "dotnet_bot.png"; // Imagen por defecto
            var cambiado = false;
            byte[] imageBytes = new byte[0];
            RefreshView();
            EmailSeleccionado = new Emails();

            CambiarImagenCommand = new Command(async () =>
            {
                try
                {
                    var file = await FilePicker.PickAsync(new PickOptions
                    {
                        FileTypes = FilePickerFileType.Images, 
                        PickerTitle = "Selecciona una imagen"
                    });

                    if (file != null)
                    {
                        
                        using (var stream = await file.OpenReadAsync())
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await stream.CopyToAsync(memoryStream);
                                imageBytes = memoryStream.ToArray();
                            }
                        }

                        if (UsuarioSeleccionado != null)
                        {
                            ImagenSeleccionada = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                            cambiado = true;
                        }
                        else
                        {
                            Console.WriteLine("No hay un usuario seleccionado.");
                            return;
                        }

                        
                        

                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine($"Error al seleccionar imagen: {ex.Message}");
                }

            });


            GuardarCommand = new Command(async () =>
            {

                try
                {

                    bool respuesta = await Application.Current.MainPage.DisplayAlert(
                        "Alerta","¿Estás seguro de que quieres guardar este usuario?",
                        "Sí", "No");
                    if (respuesta)
                    {
                        if (UsuarioSeleccionado.Emails != null && UsuarioSeleccionado.Emails.Count > 0)
                        {
                            foreach (var email in UsuarioSeleccionado.Emails)
                            {
                                App.EmailRepositorio.SaveItem(email);
                            }
                        }

                        App.UsuarioRepositorio.SaveItemCascada(UsuarioSeleccionado);
                        Console.WriteLine(App.UsuarioRepositorio.StatusMessage);

                        // Para guardar la imagen dentro de la base de datos
                        if (cambiado)
                        {
                            // Si ha cambiado la imagen, se guarda en la base de datos y reiniciamos la variable bool
                            UsuarioSeleccionado.Image = imageBytes;
                            App.UsuarioRepositorio.SaveItem(UsuarioSeleccionado);
                            cambiado = false;
                        }


                        if (EmailEliminados.Count > 0)
                        {
                            List<Emails> EmailBd = App.EmailRepositorio.GetItems();

                            foreach (var email in EmailEliminados)
                            {
                                try
                                {
                                    if (email != null && email.Id != null)
                                    {
                                        var existingEmail = EmailBd.FirstOrDefault(e => e.Id == email.Id);
                                        if (existingEmail != null)
                                        {
                                            App.EmailRepositorio.DeleteItem(email);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error al eliminar email con ID {email.Id}: {ex.Message}");
                                }
                            }

                            EmailEliminados.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Guardado cancelado");
                    }

                    RefreshView();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar: {ex.Message}");
                }

                RefreshView();

            });

            EliminarCommand = new Command(async () =>
            {
                bool respuesta = await Application.Current.MainPage.DisplayAlert(
                    "Alerta", "¿Estás seguro de que quieres borrar este usuario?",
                    "Sí", "No");

                if (respuesta)
                {
                    App.UsuarioRepositorio.DeleteItem(UsuarioSeleccionado);
                    Console.WriteLine(App.UsuarioRepositorio.StatusMessage);
                    RefreshView();
                }
                else
                {
                    
                    Console.WriteLine("Eliminación cancelada");
                }

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
            Usuarios = new ObservableCollection<Usuario>(App.UsuarioRepositorio.GetItemsCascada());
            UsuarioSeleccionado = new Usuario();

            if (UsuarioSeleccionado != null && UsuarioSeleccionado.Image != null)
            {
                // Convertir el byte[] a ImageSource usando un MemoryStream
                using (var stream = new MemoryStream(UsuarioSeleccionado.Image))
                {
                    ImagenSeleccionada = ImageSource.FromStream(() => stream);  // Asignar la imagen a ImagenSeleccionada
                }
            }
            else
            {
                // Si no hay imagen, asignamos una imagen por defecto
                ImagenSeleccionada = ImageSource.FromFile("dotnet_bot.png");
            }




        }

    }
}
