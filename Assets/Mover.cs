using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity.RenderStreaming
{
public class Mover : MonoBehaviour
{
        [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField] private InputChannelReceiverBase receiver;

    [SerializeField] private UserInterfaceController userInterfaceController;

    private Keyboard remote_keyboard;

    void Awake()
        {
            if (receiver == null) 
                receiver = GetComponent<InputChannelReceiverBase>();
            receiver.onDeviceChange += OnDeviceChange;
        }

    void Update() 
    {
        Vector3 direction = Vector3.zero;
        
        //Debug.Log(remote_keyboard);

        if (remote_keyboard != null)
        {
            if (remote_keyboard.dKey.isPressed)
                direction = Vector3.right;
            if (remote_keyboard.sKey.isPressed)
                direction = Vector3.back;
            if (remote_keyboard.aKey.isPressed)
                direction = Vector3.left;
            if (remote_keyboard.wKey.isPressed)
                direction = Vector3.forward;
        }
        transform.position += Time.deltaTime * _moveSpeed * direction;
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
            userInterfaceController?.SetDevice(device, add);
            
            switch (device){
                case Keyboard keyboard:
                        remote_keyboard = add ? keyboard : null;
                    return;
            }
        }
}
}
