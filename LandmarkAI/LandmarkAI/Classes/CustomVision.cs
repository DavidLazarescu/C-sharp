using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;


//This C# Classes got transformed from a JSON-String to C# Classes at: https://jsonutils.com/

namespace LandmarkAI.Classes
{
    public class Prediction
    {
        public double probability { get; set; }
        public string tagId { get; set; }
        public string tagName { get; set; }
    }


    public class CustomVision
    {
        public string id { get; set; }
        public string project { get; set; }
        public string iteration { get; set; }
        public DateTime created { get; set; }
        public List<Prediction> predictions { get; set; }
    }
}
