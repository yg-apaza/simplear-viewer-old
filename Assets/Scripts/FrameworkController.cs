using UnityEngine;

public abstract class FrameworkController : MonoBehaviour {
    
    void OnEnable()
    {
        EventManager.interactionsApprovedSubscribers += InteractionsApproved;
        EventManager.interactionsRejectedSubscribers += InteractionsRejected;
        EventManager.resourceCreatedSubscribers += ResourceCreated;
    }

    void Start () {
        Setup();
        // TODO: Wait Framework setup to send FRAMEWORK_READY event
        Controller.eventManager.Send(new ViewerEvent.FrameworkReady(ViewerEvent.FRAMEWORK_READY));
    }

    public abstract void Setup();
    
	public abstract void InteractionsApproved(Interaction[] interaction);

    public abstract void InteractionsRejected();

    public abstract void ResourceCreated(Resource resource);  

    void OnDisable()
    {
        EventManager.interactionsApprovedSubscribers -= InteractionsApproved;
        EventManager.interactionsRejectedSubscribers -= InteractionsRejected;
        EventManager.resourceCreatedSubscribers -= ResourceCreated;
    }

}
