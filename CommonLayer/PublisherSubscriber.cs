namespace CommonLayer
{
    public sealed class PublisherSubscriber
    {
        private static readonly PublisherSubscriber _publisherSubscriber = new PublisherSubscriber();
        private PublisherSubscriber() { }

        public static PublisherSubscriber GetInstance()
        {
            return _publisherSubscriber;
        }
        public delegate void OnChange(object sender, PerformedEventArgs eventArgs);
        public OnChange _onChange;

        public void Raise(EventsArgsTypes eventsArgsTypes)
        {
            //Invoke OnChange Action
            if (_onChange != null)
                _onChange.Invoke(this, new PerformedEventArgs(eventsArgsTypes));
        }
    }
}
