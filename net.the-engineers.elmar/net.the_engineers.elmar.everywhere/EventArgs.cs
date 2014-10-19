using System;

namespace net.the_engineers.elmar.everywhere
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
