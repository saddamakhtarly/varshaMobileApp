using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Droid;
using MobileApp.Interfaces;
using Xamarin.Forms;
using SQLite;
using MobileApp.Models;

[assembly: Xamarin.Forms.Dependency(typeof(Sqlite_Android))]
namespace MobileApp.Droid
{
    public class Sqlite_Android : ISqlite
    {
        public SQLiteConnection GetConnection()
        {
            string filename= "MobileAppDB.db3";
            string doc = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(doc, filename);
            SQLiteConnection con = new SQLiteConnection(path);
            con.CreateTable<User>();

            return con;
        }
    }
}