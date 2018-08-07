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

    public override void InteractionsApproved(Interaction[] interaction)
    {
        Debug.Log("> INTERACTIONS APPROVED > " + interaction.Length);
    }

    public override void InteractionsRejected()
    {
        Debug.Log("> INTERACTIONS REJECTED > ");
    }

    public override void ResourceCreated(Resource resource)
    {
        Debug.Log("> RESOURCE CREATED > " + resource.id);
    }
}
