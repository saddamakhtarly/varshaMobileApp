using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Interfaces
{
    
    public interface IHud
    {
        void Show();
        void Show(string message);
        void Dismiss();


    }
}
