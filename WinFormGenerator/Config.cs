using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class Config
    {
        public int DefaultControlWidth { get; set; }
        public int MinimumFormWidth { get; set; }
        public int PixelPerCharacter { get; set; }
        public int FormMargin { get; set; }
        public int ControlMargin { get; set; }


        public Config()
        {
            DefaultControlWidth = 180;
            MinimumFormWidth = 200; //Ok + Cancel button and 10px margin on sides
            PixelPerCharacter = 10;
            FormMargin = 25;
            ControlMargin = 10; 
        }
    }
}
