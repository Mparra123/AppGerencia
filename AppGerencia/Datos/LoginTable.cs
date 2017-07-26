using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace AppGerencia
{
    public class LoginTable
    {


        [PrimaryKey, AutoIncrement, Column("_idTabla")]

        public int idTabla { get; set; }
        [MaxLength(25)]

        public string cedula { get; set; }
        [MaxLength(15)]

        public string name { get; set; }
        [MaxLength(25)]

        public string userpassw { get; set; }
        [MaxLength(15)]

        public string email { get; set; }
        [MaxLength(25)]

        public string role { get; set; }
        



    }
}



    