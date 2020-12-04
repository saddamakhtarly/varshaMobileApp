using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Interfaces
{
   public interface ISqlite
    {
        SQLiteConnection GetConnection();
        
    }

}
