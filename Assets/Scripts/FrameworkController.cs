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
    
	public void InteractionsApproved(Interaction[] interactions)
    {
        foreach(Interaction i in interactions)
        {
            switch (i._event)
            {
                case 1:
                    switch (i._action)
                    {
                        case 1:
                            MarkerIsDetected_AugmentResource(i.event_inputs, i.action_inputs);
                            break;
                    }
                    break;
            }
        }
    }

    public abstract void MarkerIsDetected_AugmentResource(string[] event_inputs, string[] action_inputs);

    public abstract void InteractionsRejected();

    public void ResourceCreated(Resource resource)
    {
        // TODO: Use static strings or enums
        switch (resource.type)
        {
            case "pfmarker":
                AddPredefinedFiducialMarkerResource(resource);
                break;
            case "pnmarker":
                AddPredefinedNaturalMarkerResource(resource);
                break;
            case "poly":
                AddPolyResource(resource);
                break;
        }
    }

    public abstract void AddPredefinedFiducialMarkerResource(Resource resource);

    public abstract void AddPredefinedNaturalMarkerResource(Resource resource);

    public abstract void AddPolyResource(Resource resource);

    void OnDisable()
    {
        EventManager.interactionsApprovedSubscribers -= InteractionsApproved;
        EventManager.interactionsRejectedSubscribers -= InteractionsRejected;
        EventManager.resourceCreatedSubscribers -= ResourceCreated;
    }

}
