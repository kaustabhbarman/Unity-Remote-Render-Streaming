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
