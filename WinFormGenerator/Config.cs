//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
//------------------------------------------------------
namespace WinFormGenerator
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
