using SG_MAUI_RME.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_MAUI_RME.MVVM.Models
{
    [Table("Email")]
    public class Emails : TableData
    {
        [ForeignKey(typeof(Usuario))]
        public int IdCliente { get; set; }

        public String correo { get; set; }
    }
}
