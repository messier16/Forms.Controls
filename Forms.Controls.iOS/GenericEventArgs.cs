using System;

namespace Messier16.Forms.iOS.Controls
{
    public class GenericEventArgs<T> : EventArgs
    {
        public GenericEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}