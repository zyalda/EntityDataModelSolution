using System;

namespace CommonLayer
{
    public class PerformedEventArgs : EventArgs
    {
        public PerformedEventArgs(EventsArgsTypes _eventsArgsTypes)
        {
            EventsArgsType = _eventsArgsTypes;
        }

        public EventsArgsTypes EventsArgsType { get; set; }
    }
    public enum EventsArgsTypes
    {
        loaded,
        added,
        founded,
        updated,
        deleted,
        notfound,
    }
}
