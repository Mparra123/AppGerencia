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
        
        EditText name;
        EditText cedula;
        EditText email;
        EditText password;
        EditText role;
        
        Button btnInsert;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            btnInsert = FindViewById<Button>(Resource.Id.btnRegistro2);
            name = FindViewById<EditText>(Resource.Id.txtNombreRegistro);
            cedula = FindViewById<EditText>(Resource.Id.txtCedulaR);
            email = FindViewById<EditText>(Resource.Id.txtemailR);
            role= FindViewById<EditText>(Resource.Id.txtRoleRegister);
            password = FindViewById<EditText>(Resource.Id.txtPasswordR);
            btnInsert.Click += BtnInsert_Click; 


        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {

            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "User1.db3");
                var db = new SQLite.SQLiteConnection(dpPath);
                db.CreateTable<LoginTable>();
                LoginTable tbl = new LoginTable();
                
                tbl.name = name.Text;
                tbl.cedula = cedula.Text;
                tbl.email = email.Text;
                tbl.userpassw = password.Text;
                tbl.role = role.Text;


                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }

        }


    }
}