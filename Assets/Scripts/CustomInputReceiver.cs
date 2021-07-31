using System;
using Unity.WebRTC;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.RenderStreaming;

[Serializable]
public class CustomInputReceiver : InputChannelReceiverBase
{
    //TESTING START
    [SerializeField]
    private Camera playerCamera;
    //TESTING END
    [SerializeField, Tooltip("Array to set your own click event")]
    private ButtonClickElement[] arrayButtonClickEvent;

    public override event Action<InputDevice, InputDeviceChange> onDeviceChange;

    private CustomRemoteInput remoteInput;

    private string mainConnectionID = String.Empty;

    public override void SetChannel(string connectionId, RTCDataChannel channel)
    {
        Debug.Log("ANFANG SETCHANNEL: " + connectionId + " | CHANNEL LABEL: " + channel);
        if (channel == null)
        {
            Debug.Log("CHANNEL NULL SETCHANNEL: " + connectionId);
            if (remoteInput != null && connectionId == mainConnectionID)
            {
                Debug.Log("REMOVE | " + "INPUT ID: " + remoteInput.RemoteKeyboard.deviceId + " | CONNECTION: " + connectionId + " | MAIN: " + mainConnectionID);
                onDeviceChange.Invoke(remoteInput.RemoteGamepad, InputDeviceChange.Removed);
                onDeviceChange.Invoke(remoteInput.RemoteKeyboard, InputDeviceChange.Removed);
                onDeviceChange.Invoke(remoteInput.RemoteMouse, InputDeviceChange.Removed);
                onDeviceChange.Invoke(remoteInput.RemoteTouchscreen, InputDeviceChange.Removed);
                onDeviceChange.Invoke(remoteInput.RemoteGyroscope, InputDeviceChange.Removed);
                remoteInput.Dispose();
                remoteInput = null;
                mainConnectionID = String.Empty;
            }
        }
        else
        {
            Debug.Log("CHANNEL NOT NULL SETCHANNEL: " + connectionId);

            if(mainConnectionID.Equals(String.Empty))
            {
                Debug.Log("CHANGING MAIN ID");
                mainConnectionID = connectionId;
            }

            if (connectionId == mainConnectionID)
            {
                remoteInput = RemoteInputReceiver.Create();
                Debug.Log("ADD | " + "INPUT ID: " + remoteInput.RemoteKeyboard.deviceId + " | CONNECTION: " + connectionId + " | MAIN: " + mainConnectionID);
                remoteInput.ActionButtonClick = OnButtonClick;
                channel.OnMessage += remoteInput.ProcessInput;
                onDeviceChange.Invoke(remoteInput.RemoteGamepad, InputDeviceChange.Added);
                onDeviceChange.Invoke(remoteInput.RemoteKeyboard, InputDeviceChange.Added);
                onDeviceChange.Invoke(remoteInput.RemoteMouse, InputDeviceChange.Added);
                onDeviceChange.Invoke(remoteInput.RemoteTouchscreen, InputDeviceChange.Added);
                onDeviceChange.Invoke(remoteInput.RemoteGyroscope, InputDeviceChange.Added);
            }
        }
        base.SetChannel(connectionId, channel);
    }

    public virtual void OnButtonClick(int elementId)
    {
        foreach (var element in arrayButtonClickEvent)
        {
            if (element.elementId == elementId)
            {
                element.click.Invoke(elementId);
            }
        }
    }

    public virtual void OnDestroy()
    {
        remoteInput?.Dispose();
    }

/*
    TODO: REMOVE
*/
    private void Update() 
    {
        if (remoteInput != null && playerCamera != null)
        {
            playerCamera.transform.rotation = remoteInput.GetCamRotation();
        }
    }
}

