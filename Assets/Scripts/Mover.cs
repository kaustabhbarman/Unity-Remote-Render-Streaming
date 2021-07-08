using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using System.Linq;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private RemoteInputController remoteInputController;
        [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField] private UserInterfaceController userInterfaceController;

    void Update() 
    {
        Keyboard keyboard = remoteInputController.GetCurrentKeyboard();
        //Debug.Log("MOVER KEYBOARD: " + keyboard);
        
        Vector3 direction = Vector3.zero;
        
        //Debug.Log("MOVER DEVICE: " + keyboard);

        if (keyboard != null)
        {
            if (keyboard.dKey.isPressed)
                direction += Vector3.right;
            if (keyboard.sKey.isPressed)
                direction += Vector3.back;
            if (keyboard.aKey.isPressed)
                direction += Vector3.left;
            if (keyboard.wKey.isPressed)
                direction += Vector3.forward;
        }

        Touchscreen touchscreen = remoteInputController.GetCurrentTouchScreen();
        //Debug.Log("MOVER TOUCHSCREEN: " + touchscreen);

        if (touchscreen != null)
        {
            var touches = EnhancedTouch.activeTouches.Where(touch => touch.screen == touchscreen);

            if (touches?.Count() == 1)
            {
                var touch = touches.First();
                float x = touch.startScreenPosition.x;
                float y = touch.startScreenPosition.y;

                //Debug.Log("MOVER X: " + x + " |||||||||||||| " + "Y: " + y);

                if(x >= 0 && x <= 300 && y >= 200 && y <= 520)
                {
                    direction = Vector3.left;
                }
                if(x > 300 && x < 980 && y >= 0 && y <= 300)
                {
                    direction = Vector3.back;
                }
                if(x > 300 && x < 980 && y <= 720 && y >= 420)
                {
                    direction = Vector3.forward;
                }
                if(x >= 980 && x <= 1280 && y >= 200 && y <= 520)
                {
                    direction = Vector3.right;
                }
            }

        }

        transform.position += Time.deltaTime * _moveSpeed * direction.normalized;
    }
}