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
        EditText txtrole;
        
        Button btnInsert;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            btnInsert = FindViewById<Button>(Resource.Id.btnRegistro2);
            txtname = FindViewById<EditText>(Resource.Id.txtNombreR);
            txtcedula = FindViewById<EditText>(Resource.Id.txtCedulaR);
            txtemail = FindViewById<EditText>(Resource.Id.txtemailR);
            txtrole= FindViewById<EditText>(Resource.Id.txtRoleR);
            txtpassword = FindViewById<EditText>(Resource.Id.txtPasswordR);
            btnInsert.Click += BtnInsert_Click; 


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
                tbl.role = txtrole.Text;


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