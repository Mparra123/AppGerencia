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
using Xamarin.Auth;
using Newtonsoft.Json;
using Xamarin.Auth.AppGerencia;

namespace AppGerencia
{


    //shit just practice
    [Activity(Label = "App Avaluos 2.0", MainLauncher = true, Icon = "@drawable/Document")]
    public class MainActivity : Activity
    {

        private Button loginbtn;
        private Button registrobtn;
        public EditText txtsign;
        private EditText txtpassw;

        private Button twiterButton;
        private Button facebookButton;
        private Button linkendInButton;

        public string name;




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
            //normal button
            loginbtn = FindViewById<Button>(Resource.Id.btnLogin);
            registrobtn = FindViewById<Button>(Resource.Id.btnRegistroInicio);
            txtsign = FindViewById<EditText>(Resource.Id.txtUserName);
            txtpassw = FindViewById<EditText>(Resource.Id.txtPassword);

            //social button
            twiterButton = FindViewById<Button>(Resource.Id.btnTwitter);
            facebookButton = FindViewById<Button>(Resource.Id.btnFacebook);
            linkendInButton = FindViewById<Button>(Resource.Id.btnLonkedin);



        }

        private void HandleEvents()
        {
            loginbtn.Click += Loginbtn_Click;
            registrobtn.Click += Registrobtn_Click;
            twiterButton.Click += TwiterButton_Click;
            facebookButton.Click += FacebookButton_Click;
            linkendInButton.Click += LinkendInButton_Click;
        }




        private void LinkendInButton_Click(object sender, EventArgs e)
        {
            var auth = new OAuth2Authenticator(
                clientId: "78u2krltsr812d",
                clientSecret: "9lc0dAw0tAAwpuuX",
                scope: "r_basicprofile",
                authorizeUrl: new System.Uri("https://www.linkedin.com/uas/oauth2/authorization"),
                redirectUrl: new System.Uri("https://www.youtube.com/c/HoussemDellai"),
                accessTokenUrl: new System.Uri("https://www.linkendin.com/uas/oauth2/accessToken")
                );
            auth.Completed += LinkedinAuth_CompletedAsync;
            var ui = auth.GetUI(this);
            StartActivity(ui);
        }


        private async void LinkedinAuth_CompletedAsync(object sender, AuthenticatorCompletedEventArgs e)
        {

            try
            {
                if (e.IsAuthenticated)
                {
                    var request = new OAuth2Request("GET", new System.Uri("https://api.linkedin.com/v1/people/~:(id,firstName,lastName)?" 
                        + "format=json" 
                        + "&oauth2_access_token=" 
                        + e.Account.Properties["access_token"]),


                        null, e.Account);

                    var linkedinresponse = await request.GetResponseAsync();
                    var json = linkedinresponse.GetResponseText();
                    //var linkedinUser = JsonConvert.DeserializeObject<LinkedinUser>(json);
                    //var name = linkedinUser.FirstName + "" + linkedinUser.lastName;
                    //var id = linkedinUser.Id;
                    Toast.MakeText(this, "Bienvenido Usuario:", ToastLength.Long).Show();
                    Intent intent = new Intent(this, typeof(homeActivity));
                    StartActivity(intent);
                }
            }
            catch (Exception a)
            {
                Toast.MakeText(this, "** Lo sentimos Ocurrio un problema **", ToastLength.Long).Show();
            }
        }



        private void FacebookButton_Click(object sender, EventArgs e)
        {
            var auth = new OAuth2Authenticator(
                clientId: "1179568445482769",
                scope: "",
                authorizeUrl: new System.Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new System.Uri("http://www.facebook.com/connect/login_success.html"));

            auth.Completed += FaceBookAuth_CompletedAsync;
            var ui = auth.GetUI(this);
            StartActivity(ui);
        }

        private async void FaceBookAuth_CompletedAsync(object sender, AuthenticatorCompletedEventArgs e)
        {

            try
            {
                if (e.IsAuthenticated)
                {
                    var request = new OAuth2Request("GET", new System.Uri("https://graph.facebook.com/me?fields=name,picture"),

                        null, e.Account);

                    var response = await request.GetResponseAsync();
                    var json = response.GetResponseText();

                    //get the profile values
                    var faceUser = JsonConvert.DeserializeObject<FaceUser>(json);

                    name = faceUser.name;

                    Toast.MakeText(this, "Bienvenido:" + name, ToastLength.Long).Show();
                    Intent intent = new Intent(this, typeof(homeActivity));
                    StartActivity(intent);
                    
                }
            }
            catch (Exception a)
            {
                Toast.MakeText(this, "** Lo sentimos Ocurrio un problema **" , ToastLength.Long).Show();
            }
        }

        private void TwiterButton_Click(object sender, EventArgs e)
        {
            var auth = new OAuth1Authenticator(
                    consumerKey: "XJVrM50CAFG6BClyvKnFGut3u",
                    consumerSecret: "bAqZ2jskVoRRbwiamq4LqdgO0dQk1qIsCJ9KCxzpbDVzi0L0OI",
                    requestTokenUrl: new System.Uri("https://api.twitter.com/oauth/request_token"),
                    authorizeUrl: new System.Uri("https://api.twitter.com/oauth/authorize"),
                    accessTokenUrl: new System.Uri("https://api.twitter.com/oauth/access_token"),
                    callbackUrl: new System.Uri("http://mobile.twitter.com"));



            auth.Completed += Auth_CompletedAsync; ;
            var ui = auth.GetUI(this);
            StartActivity(ui);
        }

        private async void Auth_CompletedAsync(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var request = new OAuth1Request("GET", new System.Uri("http://mobile.twitter.com"),
                    null, e.Account);

                var response = await request.GetResponseAsync();
                var json = response.GetResponseText();
                //var twitteruser = JsonConvert.DeserializeObject<TwitterUser>(json);

                Toast.MakeText(this, "Bienvenido:", ToastLength.Long).Show();
                Intent intent = new Intent(this, typeof(homeActivity));
                StartActivity(intent);
            }
        }

        private void Registrobtn_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this,typeof(registroActivity));
            StartActivity(intent);
        }

        private void Loginbtn_Click(object sender, System.EventArgs e)
        {
            
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)
                    , "User2.db3");

                var db = new SQLite.SQLiteConnection(dpPath);
                var data = db.Table<LoginTable>();
                var data1 = data.Where(x => x.cedula == txtsign.Text &&
                x.userpassw == txtpassw.Text ).FirstOrDefault();// validacion
                
                if (data1 !=null)// perito
                {
                    Console.WriteLine("Si hay gente");
                    if (data1.role== "Perito")
                    {
                        Toast.MakeText(this, "Login Success Perito", ToastLength.Long).Show();
                        var intent = new Intent(this, typeof(homeActivity));
                        StartActivity(intent);
                    }
                    else
                    {
                        Toast.MakeText(this, "Login Success Usuario Normal", ToastLength.Long).Show();
                        var intent = new Intent(this, typeof(UserActivity));
                        StartActivity(intent);
                        Console.WriteLine("Usuario Normal");
                    }
                    
                    
                }
                else
                {

                    Toast.MakeText(this, "UserName or Password Invalid", ToastLength.Short).Show();
                    
                }

                data1 = null;

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error de Digitos", ToastLength.Short).Show();
            }


        }


        

    }

    
}

