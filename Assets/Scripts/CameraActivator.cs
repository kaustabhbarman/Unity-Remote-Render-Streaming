using UnityEngine;

public class CameraActivator : MonoBehaviour
{
    [SerializeField]
    private Camera activeCamera;
    void Awake()
    {
        activeCamera.enabled = true;
    }

    void LateUpdate()
    {
        activeCamera.Render();
        RenderTexture.active = activeCamera.targetTexture;
    }

    public void renderCamera()
    {
        activeCamera.Render();
        RenderTexture.active = activeCamera.targetTexture;
    }
}
