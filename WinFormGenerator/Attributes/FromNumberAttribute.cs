//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
//------------------------------------------------------
using System;

namespace WinFormGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromNumberAttribute : Attribute
    {
        public int Max { get; set; }
        public int Min { get; set; }
    }
}
