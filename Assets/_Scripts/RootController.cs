using UnityEngine;
using UnityEngine.UIElements;

public class RootController : Singleton<RootController>
{
    public UIDocument RootDocument;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}