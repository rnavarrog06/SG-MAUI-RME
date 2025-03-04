using SG_MAUI_RME.Abstractions;
using SG_MAUI_RME.MVVM.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Linq.Expressions;

namespace SG_MAUI_RME.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T : TableData, new()
    {
        void SaveItem(T item);
        void SaveItemCascada(T item, bool isCascada = true);
        T GetItem(int id);
        T GetItem(Expression<Func<T, bool>> predicate);
        List<T> GetItems();
        List<T> GetItemsCascada();
        List<T> GetItems(Expression<Func<T, bool>> predicate);
        void DeleteItem(T item);

    }

    public class BaseRepository<T> :
     IBaseRepository<T> where T : TableData, new()
    {
        SQLiteConnection connection;
        public string StatusMessage { get; set; }
        public BaseRepository()
        {
            connection =
            new SQLiteConnection(Constantes.DataBasePath,
            Constantes.Flags);
            connection.CreateTable<T>();
        }
        public Usuario Login(string name, string password)
        {
            try
            {
                var usuarios = GetItems()?.Cast<Usuario>().ToList();


                if (usuarios != null && usuarios.Count > 0)
                {
                    foreach (var usuario in usuarios)
                    {
                        if (usuario.Name == name && usuario.Passwd == password)
                        {
                            StatusMessage = "Inicio de sesión exitoso.";
                            return usuario;
                        }
                    }

                    if (usuarios.Any(u => u.Name == name))
                    {
                        StatusMessage = "Contraseña incorrecta.";
                    }
                    else
                    {
                        StatusMessage = "Usuario no encontrado.";
                    }
                }
                else
                {
                    StatusMessage = "No se encontraron usuarios en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }

            return null;
        }

        public void DeleteItem(T item)
        {
            try
            {
                //connection.Delete(item);
                connection.Delete(item, true);
            }
            catch (Exception ex)
            {
                StatusMessage =
                $"Error: {ex.Message}";
            }
        }
        public void Dispose()
        {
            connection.Close();
        }
        public T GetItem(int id)
        {
            try
            {
                return
                connection.Table<T>()
                .FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage =
                $"Error: {ex.Message}";
            }
            return null;
        }
        public T GetItem(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return connection.Table<T>()
                .Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                StatusMessage =
                $"Error: {ex.Message}";
            }
            return null;
        }
        public List<T> GetItems()
        {
            try
            {
                return connection.Table<T>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage =
                $"Error: {ex.Message}";
            }
            return null;
        }

        public List<T> GetItems(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return connection.Table<T>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage =
                $"Error: {ex.Message}";
            }
            return null;
        }

        //GetItems con cascada
        public List<T> GetItemsCascada()
        {
            try
            {
                return connection.GetAllWithChildren<T>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage =
                $"Error: {ex.Message}";
            }
            return null;
        }

        public void SaveItem(T item)
        {
            int result = 0;
            try
            {
                if (item.Id != 0)
                {
                    result =
                    connection.Update(item);
                    StatusMessage =
                    $"{result} row(s) updated";
                }
                else
                {
                    result = connection.Insert(item);
                    StatusMessage =
                     $"{result} row(s) added";
                }
            }
            catch (Exception ex)
            {
                StatusMessage =
                $"Error: {ex.Message}";
            }
        }

        //SaveItems con cascada
        public void SaveItemCascada(T item, bool isCascada = true)
        {
            //De momento solo inserta
            

            int result = 0;
            try
            {
                if (item.Id != 0)
                {
                    
                    connection.UpdateWithChildren(item);
                   
                }
                else
                {
                    connection.InsertWithChildren(item, isCascada);

                }
            }
            catch (Exception ex)
            {
                StatusMessage =
                $"Error: {ex.Message}";
            }

        }

    }
}
