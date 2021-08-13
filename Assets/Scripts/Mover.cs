using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using System.Linq;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private RemoteInputController remoteInputController;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float rotationSpeed = 360f;

    private Animator animator;
    private Vector3 direction = Vector3.zero;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        direction = Vector3.zero;
        HandleKeyboardInput();
        HandleGamepadInput();
        HandleTouchInput();
        HandleMovement();
        UpdateAnimator();
    }

    private void HandleKeyboardInput()
    {
        Keyboard keyboard = remoteInputController.GetCurrentKeyboard();

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
    }

    private void HandleGamepadInput()
    {
        Gamepad gamepad = remoteInputController.GetCurrentGamepad();

        if (gamepad?.leftStick != null)
        {
            var axis = gamepad.leftStick.ReadValue();
            direction += new Vector3(axis.x, 0, axis.y);
        }
    }

    private void HandleTouchInput()
    {
        Touchscreen touchscreen = remoteInputController.GetCurrentTouchScreen();

        if (touchscreen != null)
        {
            var touches = EnhancedTouch.activeTouches.Where(touch => touch.screen == touchscreen);

            if (touches?.Count() == 1)
            {
                var touch = touches.First();
                float x = touch.startScreenPosition.x;
                float y = touch.startScreenPosition.y;

                if (x >= 0 && x <= 300 && y >= 200 && y <= 520)
                {
                    direction = Vector3.left;
                }
                if (x > 300 && x < 980 && y >= 0 && y <= 300)
                {
                    direction = Vector3.back;
                }
                if (x > 300 && x < 980 && y <= 720 && y >= 420)
                {
                    direction = Vector3.forward;
                }
                if (x >= 980 && x <= 1280 && y >= 200 && y <= 520)
                {
                    direction = Vector3.right;
                }
            }
        }
    }

    private void HandleMovement()
    {
        if (direction != Vector3.zero)
        {
            Quaternion destinationRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destinationRotation, rotationSpeed * Time.deltaTime);
            transform.position += Time.deltaTime * moveSpeed * direction.normalized;
        }
    }

    private void UpdateAnimator()
    {
        if (direction != Vector3.zero)
        {
            animator.SetFloat("speed", 0.5f, 0.1f, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("speed", 0f, 0.1f, Time.deltaTime);
        }
    }
}
