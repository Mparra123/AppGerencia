using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Database;
using static Android.Resource;
using System.Collections.Generic;
using System;
using System.IO;
using SQLite;

namespace AppGerencia
{


    //shit just practice
    [Activity(Label = "App Avaluos 2.0", MainLauncher = true, Icon = "@drawable/Document")]
    public class MainActivity : Activity
    {

        private Button loginbtn;
        private Button registrobtn;
        private EditText txtsign;
        private EditText txtpassw;
             
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            FindViews();
            HandleEvents();
            CreateDB();
        }

        private string CreateDB()
        {


            var output = "";
            output += "Creacion base datos si no existe";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "User2.db3");// create new database
            var db = new SQLite.SQLiteConnection(dpPath);
            
            output += "\n DataBase Created....";
            
            return output;
        }

        private void FindViews()
        {

            loginbtn = FindViewById<Button>(Resource.Id.btnLogin);
            registrobtn = FindViewById<Button>(Resource.Id.btnRegistroInicio);
            txtsign = FindViewById<EditText>(Resource.Id.txtUserName);
            txtpassw = FindViewById<EditText>(Resource.Id.txtPassword);
                
        }

        private void HandleEvents()
        {
            loginbtn.Click += Loginbtn_Click;
            registrobtn.Click += Registrobtn_Click;
        }

        private void Registrobtn_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this,typeof(registroActivity));
            StartActivity(intent);
        }

        private void Loginbtn_Click(object sender, System.EventArgs e)
        {
            //    var intent = new Intent(this, typeof(homeActivity));
            //  StartActivity(intent);

            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)
                    , "User2.db3");

                var db = new SQLite.SQLiteConnection(dpPath);
                var data = db.Table<LoginTable>();
                var data1 = data.Where(x => x.cedula == txtsign.Text &&
                x.userpassw == txtpassw.Text &&x.role.StartsWith("1")|| x.role.StartsWith("2")).FirstOrDefault();// validacion
                
                if (data1 !=null)// perito
                {
                    Console.WriteLine("Si hay gente");
                    if (data1.role=="1")
                    {
                        Toast.MakeText(this, "Login Success Perito", ToastLength.Short).Show();
                        var intent = new Intent(this, typeof(homeActivity));
                        StartActivity(intent);
                    }
                    else
                    {
                        Toast.MakeText(this, "Login Success Usuario Normal", ToastLength.Short).Show();
                        //var intent = new Intent(this, typeof(homeActivity));
                        //StartActivity(intent);
                        Console.WriteLine("Usuario Normal");
                    }
                    
                    
                }
                else
                {

                    Toast.MakeText(this, "UserName or Password Invalid", ToastLength.Short).Show();
                    
                }



            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }


        }


        

    }
}

