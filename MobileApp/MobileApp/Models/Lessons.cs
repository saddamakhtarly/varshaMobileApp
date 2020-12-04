using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class LessonsResponse : Response
    {
        public LessonsResponse()
        {
            Lessons = new List<Lessons>();
        }
        public List<Lessons> Lessons { get; set; }
    }
    public class Lessons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LessonName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
    public class Course
    {
        public string Name { get; set; }
        public string Time { get; set; }

    }
}
