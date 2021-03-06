﻿using System;

namespace DotBoy.Utils
{
    public class StringValue : Attribute
    {
        public StringValue(string value)
        {
            mValue = value;
        }

        public string Value { get => mValue; }

        readonly string mValue;
    }
}
