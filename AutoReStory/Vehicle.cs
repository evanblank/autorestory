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
    public class Vehicle
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int PercentagePaint { get; set; }
        public int PercentageBody { get; set; }
        public int PercentageGlass { get; set; }
        public int PercentageTrim { get; set; }
        public int PercentageUpholstery { get; set; }
        public int PercentageMechanical { get; set; }
        public int PercentageElectrical { get; set; }
        public int PercentageFrame { get; set; }
        public int PercentageTires { get; set; }
        public int PercentageOverall { get; set; }

        public static explicit operator Vehicle(Java.Lang.Object v)
        {
            throw new NotImplementedException();
        }
    }
}