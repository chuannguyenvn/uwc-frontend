using UnityEngine;

public class RootController : Singleton<RootController>
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}