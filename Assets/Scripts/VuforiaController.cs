using UnityEngine;
using Vuforia;

public class VuforiaController : FrameworkController
{
    public GameObject arCamera;

    public override void Setup()
    {
        arCamera.GetComponent<VuforiaBehaviour>().enabled = true;
        VuforiaRuntime.Instance.InitVuforia();
    }
    
    public override void MarkerIsDetected_AugmentResource(string[] event_inputs, string[] action_inputs)
    {
        throw new System.NotImplementedException();
    }

    public override void InteractionsRejected()
    {
        Debug.Log("> INTERACTIONS REJECTED > ");
    }

    public override void AddPredefinedFiducialMarkerResource(Resource resource)
    {
        Debug.Log("> NOT SUPPORTED");
    }

    public override void AddPredefinedNaturalMarkerResource(Resource resource)
    {
        throw new System.NotImplementedException();
    }

    public override void AddPolyResource(Resource resource)
    {
        throw new System.NotImplementedException();
    }
}
