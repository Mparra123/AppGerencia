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
using System.Collections;
using Java.Util;
using System.IO;

namespace AppGerencia
{
    [Activity(Label = "Bienvenidos al Registro")]
    public class registroActivity : Activity
    {
        
        EditText txtname;
        EditText txtcedula;
        EditText txtemail;
        EditText txtpassword;
        ///EditText txtrole;
        Spinner spi;
        Button btnInsert;
        string toast="";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            btnInsert = FindViewById<Button>(Resource.Id.btnRegistro2);
            txtname = FindViewById<EditText>(Resource.Id.txtNombreR);
            txtcedula = FindViewById<EditText>(Resource.Id.txtCedulaR);
            txtemail = FindViewById<EditText>(Resource.Id.txtemailR);
            //txtrole= FindViewById<EditText>(Resource.Id.txtRoleR);
            txtpassword = FindViewById<EditText>(Resource.Id.txtPasswordR);
            btnInsert.Click += BtnInsert_Click;
            spi = FindViewById<Spinner>(Resource.Id.spiRegistro);
            spi.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this,Resource.Array.planet_array,Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spi.Adapter = adapter;
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            // sent the value of spinner
            toast = string.Format("{0}",spi.GetItemAtPosition(e.Position));
            //Toast.MakeText(this,toast,ToastLength.Short).Show();// if you want to show user the selection value
            
 
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {

            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "User2.db3");
                var db = new SQLite.SQLiteConnection(dpPath);
                db.CreateTable<LoginTable>();
                LoginTable tbl = new LoginTable();

                tbl.cedula = txtcedula.Text;
                tbl.name = txtname.Text;
                tbl.email = txtemail.Text;
                tbl.userpassw = txtpassword.Text;
                //tbl.role = txtrole.Text;

                tbl.role = toast; 

                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully....", ToastLength.Short).Show();

                
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }

        }

        
    }
}