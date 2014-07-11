//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
//------------------------------------------------------
using System;

namespace WinFormGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FormAttribute : Attribute
    {
        public string Text { get; set; }

        public int Height { get; set; }
    }
}
