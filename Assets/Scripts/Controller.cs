using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {

    public static Controller INSTANCE;
    public GameObject gameObjectController;
    public static EventManager eventManager;
    
    void Awake()
    {
        if (!INSTANCE)
        {
            INSTANCE = this;
            DontDestroyOnLoad(gameObjectController);
            EventManager.projectOpenedSubscribers += GotoFrameworkScene;
            EventManager.projectClosedSubscribers += GotoMainScene;
            UnityThread.InitUnityThread();
        }
        else
            Destroy(gameObjectController);
    }

    void Start()
    {
        #if UNITY_STANDALONE
            Debug.Log("> UNITY_STANDALONE");
            eventManager = new SocketManager();
        #endif
        #if UNITY_EDITOR
            Debug.Log("> UNITY_EDITOR");
            eventManager = new SocketManager();
        #endif
        #if UNITY_ANDROID
            Debug.Log("> UNITY_ANDROID");
            eventManager = new EventManager();
        #endif

        eventManager.Setup();
    }

    void OnDisable()
    {
        // TODO: Stop Socket ONLY before closing the application
        eventManager.Stop();
        EventManager.projectOpenedSubscribers -= GotoFrameworkScene;
        EventManager.projectClosedSubscribers -= GotoMainScene;
    }

    public void GotoFrameworkScene(string framework)
    {
        // TODO: Use static strings or enums
        switch (framework)
        {
            case "artoolkit":
                break;
            case "vuforia":
                SceneManager.LoadScene("VuforiaScene");
                break;
        }
    }

    public void GotoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
