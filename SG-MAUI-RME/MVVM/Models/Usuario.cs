﻿using PropertyChanged;
using SG_MAUI_RME.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SG_MAUI_RME.MVVM.Models
{
    [Table("Usuario")]
    [AddINotifyPropertyChangedInterface]
    public class Usuario : TableData
    {
        [Column("Name"), Indexed, NotNull]
        public string Name { get; set; }

        [MaxLength(20), NotNull]
        public string Passwd { get; set; }

        [MaxLength(200)]
        public Byte[] Image { get; set; }

        [Ignore]
        public ImageSource ImagenUsuario => Image == null ? "dotnet_bot.png" : ImageSource.FromStream(() => new MemoryStream(Image));


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Emails> Emails { get; set; }

        

    }
}
