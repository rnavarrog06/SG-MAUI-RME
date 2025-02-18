using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_MAUI_RME.Abstractions
{
    public class TableData
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }




    }
}
