using UnityEngine;
using Unity.RenderStreaming;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class RemoteInputController : MonoBehaviour
{
    [SerializeField] private InputChannelReceiverBase receiver;

    [SerializeField] private bool enhancedTouchSupport;

    private Keyboard remote_keyboard;

    private Touchscreen remote_touchscreen;

    private Gyroscope remote_gyroscope;

    void Awake() 
    {
        if (receiver == null)
        {
            receiver = GetComponent<InputChannelReceiverBase>();
        }

        receiver.onDeviceChange += OnDeviceChange;

        if (enhancedTouchSupport) 
        {
            EnhancedTouchSupport.Enable();
        }
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
            {
                case InputDeviceChange.Added:
                    SetDevice(device);
                    return;
                case InputDeviceChange.Removed:
                    SetDevice(device, false);
                    return;
            }

    }

    void SetDevice(InputDevice device, bool add=true)
    {
        switch (device){
            case Keyboard keyboard:
                    remote_keyboard = add ? keyboard : null;
                return;
            case Touchscreen screen:
                    remote_touchscreen = add ? screen : null;
                return;
            case Gyroscope gyro:
                    remote_gyroscope = add ? gyro : null;
                return;
        }
    }

    //
    // Returns the current active keyboard.
    //
    public Keyboard GetCurrentKeyboard()
    {
        return remote_keyboard;
    }

    //
    // Returns the current active touch screen.
    //
    public Touchscreen GetCurrentTouchScreen()
    {
        return remote_touchscreen;
    }

    //
    // Returns the current active gyroscope.
    //
    public Gyroscope GetCurrentGyroscope()
    {
        return remote_gyroscope;
    }
}

