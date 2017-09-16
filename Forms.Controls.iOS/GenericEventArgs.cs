using System;
using System.Collections.Generic;
using System.Text;

namespace Messier16.Forms.iOS.Controls
{
    public class GenericEventArgs<T> : EventArgs
    {
        public T Value { get;  }
        public GenericEventArgs(T value)
        {
            Value = value;
        }
    }
}
