using System;
using UnityEngine;
using UnityEngine.UI;

public class CommandLineController : MonoBehaviour
{

    [SerializeField]
    private AdjustableCameraStreamer cameraStreamer;

    void Start()
    {
        if (cameraStreamer != null)
        {
            cameraStreamer.StreamingSize = GetResolutionFromCommandLine();
        }
    }

    private Vector2Int GetResolutionFromCommandLine()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        int streamingWidth = cameraStreamer.StreamingSize.x;
        int streamingHeight = cameraStreamer.StreamingSize.y;
        int i = 0;

        foreach (string s in args)
        {
            if (s.Contains("-streamingWidth"))
            {
                string[] split = s.Split('=');

                if (split.Length >= 2)
                {
                    streamingWidth = Int32.Parse(split[1]);
                }
            }

            if (s.Contains("-streamingHeight"))
            {
                string[] split = s.Split('=');

                if (split.Length >= 2)
                {
                    streamingHeight = Int32.Parse(split[1]);
                }
            }
        }

        return new Vector2Int(streamingWidth, streamingHeight);
    }
}
