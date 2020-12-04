using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileAppAPI.Models
{
    public class SaveImageResponse : Response
    {
        public SaveImageResponse()
        {
            imageurl = new List<string>();
        }
        public List<string> imageurl { get; set; }
    }
}