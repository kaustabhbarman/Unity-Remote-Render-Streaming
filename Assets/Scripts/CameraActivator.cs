using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivator : MonoBehaviour
{
    [SerializeField]
    private Camera activeCamera;
    // Start is called before the first frame update
    void Awake()
    {
        activeCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        activeCamera.Render();
        RenderTexture.active = activeCamera.targetTexture;
    }
}
