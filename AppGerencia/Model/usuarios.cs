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

namespace AppGerencia
{
    [Serializable]
    public class usuarios
    {
        List <usuarios> user = new List<usuarios>();
        
        public usuarios()
        {
            user.Add(new usuarios() { id=1,nombre="Mauricio",
            apellidos="Parrales",email="mauricio.parrales@ulatina.net",telefo=837373333
            ,cedula="1213133",direcc="San Jose, Costa Rica",rol=1});

            user.Add(new usuarios()
            {
                id = 2,
                nombre = "Jorge",
                apellidos = "Barrates",
                email = "jg.es@ulatina.net",
                telefo = 837373333
            ,
                cedula = "1213133",
                direcc = "San Jose, Costa Rica,Tres Rios",
                rol = 2
            });

            user.Add(new usuarios()
            {
                id = 3,
                nombre = "cristian",
                apellidos = "pomares",
                email = "cris.p@ulatina.net",
                telefo = 837373333
            ,
                cedula = "1213133",
                direcc = "Cartago, Costa Rica",
                rol = 3
            });
        }


        public static List<usuarios> us = new List<usuarios>()
        {
            new usuarios()
            {
                id = 4,
                nombre = "Luis",
                apellidos = "Barrates",
                email = "ls.es@ulatina.net",
                telefo = 837373333
            ,
                cedula = "1213133",
                direcc = "San Jose, Costa Rica,Tres Rios",
                rol = 4
            }
        };

        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public int telefo { get; set; }
        public string cedula { get; set; }
        public string direcc { get; set; }
        public int rol { get; set; }
        
    }
}