using System;
using Unity.WebRTC;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.RenderStreaming;

[Serializable]
public class CustomInputReceiver : InputChannelReceiverBase
{
    [SerializeField] private Camera playerCamera;

    [SerializeField, Tooltip("Array to set your own click event")]
    private ButtonClickElement[] arrayButtonClickEvent;

    public override event Action<InputDevice, InputDeviceChange> onDeviceChange;

    private CustomRemoteInput remoteInput;

    private string mainConnectionID = String.Empty;

    public override void SetChannel(string connectionId, RTCDataChannel channel)
    {
        if (channel == null)
        {
            if (remoteInput != null && connectionId == mainConnectionID)
            {
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

            if (mainConnectionID.Equals(String.Empty))
            {
                mainConnectionID = connectionId;
            }

            if (connectionId == mainConnectionID)
            {
                remoteInput = RemoteInputReceiver.Create();
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

    private void Update()
    {
        if (remoteInput != null && playerCamera != null)
        {
            playerCamera.transform.rotation = remoteInput.GetCamRotation();
        }
    }
}

