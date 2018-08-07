public class ViewerEvent {
    public string _type;
    public const string VIEWER_READY = "viewer_ready";
    public const string FRAMEWORK_READY = "framework_ready";

    public ViewerEvent(string _type)
    {
        this._type = _type;
    }

    public class ViewerReady : ViewerEvent
    {
        public ViewerReady(string _type) : base(_type)
        {
        }
    }

    public class FrameworkReady : ViewerEvent
    {
        public FrameworkReady(string _type) : base(_type)
        {
        }
    }
}
