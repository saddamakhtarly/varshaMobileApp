using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileAppAPI.Models
{
    public class MusicResponse:Response
    {
        public MusicResponse()
        {
            Musics = new List<Music>();
        }
        public List<Music> Musics { get; set; }
    }
    public class Music
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string MusicName { get; set; }
        public string Duration { get; set; }
        public int LessonId { get; set; }
        
    }
}
