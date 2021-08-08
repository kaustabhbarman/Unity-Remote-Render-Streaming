using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RenderStreaming;

public class AdjustableCameraStreamer : CameraStreamer
{
    public Vector2Int StreamingSize
    {
        get { return streamingSize; }
        set { streamingSize = value; }
    }
}
