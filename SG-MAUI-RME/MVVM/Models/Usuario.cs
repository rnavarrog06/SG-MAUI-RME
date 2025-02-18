using SG_MAUI_RME.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SG_MAUI_RME.MVVM.Models
{
    [Table("Usuario")]
    public class Usuario : TableData
    {
        [Column("Name"), Indexed, NotNull]
        public string Name { get; set; }

        [MaxLength(20), NotNull]
        public string Passwd { get; set; }
    }
}
