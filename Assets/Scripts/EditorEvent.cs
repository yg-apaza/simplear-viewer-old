[System.Serializable]
public class EditorEvent {
    public string _type;
    public const string WAITING_VIEWER = "waiting_viewer";
    public const string PROJECT_OPENED = "project_opened";
    public const string PROJECT_CLOSED = "project_closed";
    public const string INTERACTIONS_APPROVED = "interactions_approved";
    public const string INTERACTIONS_REJECTED = "interactions_rejected";
    public const string RESOURCE_CREATED = "resource_created";
    public const string RESOURCE_DELETED = "resource_deleted";
    public const string RESOURCE_RENAMED = "resource_renamed";
    
    public EditorEvent(string _type)
    {
        this._type = _type;
    }

    [System.Serializable]
    public class WaitingViewer : EditorEvent
    {
        public WaitingViewer(string _type) : base(_type)
        {
        }
    }

    [System.Serializable]
    public class ProjectOpened : EditorEvent
    {
        public string framework;

        public ProjectOpened(string _type, string framework) : base(_type)
        {
            this.framework = framework;
        }
    }

    [System.Serializable]
    public class ProjectClosed : EditorEvent
    {
        public ProjectClosed(string _type) : base(_type)
        {
        }
    }

    [System.Serializable]
    public class InteractionsApproved : EditorEvent
    {
        public Interaction[] interactions;

        public InteractionsApproved(string _type, Interaction[] interactions) : base(_type)
        {
            this.interactions = interactions;
        }
    }

    [System.Serializable]
    public class InteractionsRejected : EditorEvent
    {
        public InteractionsRejected(string _type) : base(_type)
        {
        }
    }

    [System.Serializable]
    public class ResourceCreated : EditorEvent
    {
        public Resource resource;

        public ResourceCreated(string _type, Resource resource) : base(_type)
        {
            this.resource = resource;
        }
    }

}
