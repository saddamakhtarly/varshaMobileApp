using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
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