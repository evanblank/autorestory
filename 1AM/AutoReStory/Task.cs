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

namespace AutoReStory
{
    class Task
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }

        public static explicit operator Task(Java.Lang.Object v)
        {
            throw new NotImplementedException();
        }
    }
}