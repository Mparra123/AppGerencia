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
using System.IO;

namespace AppGerencia
{
    [Activity(Label = "Perfil de Perito, Bienvenido")]
    public class homeActivity : Activity
    {

        ListView Vistatxt2;
        string[] opt;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home);

            initialize();
        }


        public void initialize()
        {

           
            Vistatxt2 = FindViewById<ListView>(Resource.Id.listView2);
            opt = new string[] { "Bienvenido Irazu_App_Avaluos" };
            Vistatxt2.Adapter = new ArrayAdapter(this,Android.Resource.Layout.SimpleListItem1,opt);
            Vistatxt2.FastScrollEnabled = true;

            Vistatxt2.ItemClick += Vistatxt_ItemClick; ;
        }

        private void Vistatxt_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, opt[e.Position], ToastLength.Short).Show();
        }
    }
}