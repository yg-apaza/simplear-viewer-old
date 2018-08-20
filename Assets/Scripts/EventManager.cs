using UnityEngine;

public class EventManager
{
    public delegate void ProjectOpened(string framework);
    public static event ProjectOpened projectOpenedSubscribers;
    public delegate void ProjectClosed();
    public static event ProjectClosed projectClosedSubscribers;
    public delegate void InteractionsApproved(Interaction[] interactions);
    public static event InteractionsApproved interactionsApprovedSubscribers;
    public delegate void InteractionsRejected();
    public static event InteractionsRejected interactionsRejectedSubscribers;
    public delegate void ResourceCreated(Resource resource);
    public static event ResourceCreated resourceCreatedSubscribers;

    public virtual void Setup()
    {
    }

    protected void Receive(string jsonData)
    {
        Debug.Log("> RECEIVING > " + jsonData);
        EditorEvent editorEvent = new EditorEvent("");
        JsonUtility.FromJsonOverwrite(jsonData, editorEvent);

        switch(editorEvent._type)
        {
            case EditorEvent.WAITING_VIEWER:
                Send(new ViewerEvent.ViewerReady(ViewerEvent.VIEWER_READY));
                break;
            case EditorEvent.PROJECT_OPENED:
                EditorEvent.ProjectOpened projectOpened = new EditorEvent.ProjectOpened("", "");
                JsonUtility.FromJsonOverwrite(jsonData, projectOpened);
                UnityThread.ExecuteInUpdate(() =>
                {
                    if (projectOpenedSubscribers != null)
                        projectOpenedSubscribers(projectOpened.framework);
                });
                break;
            case EditorEvent.PROJECT_CLOSED:
                UnityThread.ExecuteInUpdate(() =>
                {
                    if (projectClosedSubscribers != null)
                        projectClosedSubscribers();
                });
                break;
            case EditorEvent.INTERACTIONS_APPROVED:
                EditorEvent.InteractionsApproved interactionsApproved = new EditorEvent.InteractionsApproved("", null);
                JsonUtility.FromJsonOverwrite(jsonData, interactionsApproved);
                UnityThread.ExecuteInUpdate(() =>
                {
                    if (interactionsApprovedSubscribers != null)
                        interactionsApprovedSubscribers(interactionsApproved.interactions);
                });
                break;
            case EditorEvent.INTERACTIONS_REJECTED:
                UnityThread.ExecuteInUpdate(() =>
                {
                    if (interactionsRejectedSubscribers != null)
                        interactionsRejectedSubscribers();
                });
                break;
            case EditorEvent.RESOURCE_CREATED:
                EditorEvent.ResourceCreated resourceCreated = new EditorEvent.ResourceCreated("", new Resource("", "", "", ""));
                JsonUtility.FromJsonOverwrite(jsonData, resourceCreated);
                UnityThread.ExecuteInUpdate(() =>
                {
                    if (resourceCreatedSubscribers != null)
                        resourceCreatedSubscribers(resourceCreated.resource);
                });
                break;
        }
    }

    public virtual void Send(ViewerEvent viewerEvent)
    {
        Debug.Log("> ANDROID SEND");
    }

    public virtual void Stop()
    {
    }
}
