//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
//------------------------------------------------------
using System;

namespace WinFormGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class FormTextAttribute : Attribute
    {
        public bool Multiline { get; set; }
        public int Rows { get; set; }
    }
}
