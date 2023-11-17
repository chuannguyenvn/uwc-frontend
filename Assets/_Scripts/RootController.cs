using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

public class RootController : Singleton<RootController>
{
    public UIDocument RootDocument;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}