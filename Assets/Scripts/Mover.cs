using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using System.Linq;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

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
        Gyroscope gyro = remoteInputController.GetCurrentGyroscope();

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

        if (gyro != null)
        {
            Debug.Log(gyro);
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

                //Debug.Log("MOVER X: " + x + " |||||||||||||| " + "Y: " + y);

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

//    private void FixedUpdate()
//    {
        
//        if (IsMouseDragged(mouse, false))
//        {
//            UpdateTargetCameraStateFromInput(mouse.delta.ReadValue());
//        }

//    }
//    static bool IsMouseDragged(Mouse m, bool useLeftButton)
//    {
//        if (null == m)
//            return false;

//        if (Screen.safeArea.Contains(m.position.ReadValue()))
//        {
//            //check left/right click
//            if ((useLeftButton && m.leftButton.isPressed) || (!useLeftButton && m.rightButton.isPressed))
//            {
//                return true;
//            }
//        }

//        return false;
//    }

//    void UpdateTargetCameraStateFromInput(Vector2 input)
//    {

//        m_TargetCameraState.yaw += input.x * 1;
//        m_TargetCameraState.pitch += input.y * 1;
//    }
//}

//class CameraState
//{
//    public float yaw;
//    public float pitch;
//    public float roll;
//    public float x;
//    public float y;
//    public float z;

//    public void SetFromTransform(Transform t)
//    {
//        pitch = t.eulerAngles.x;
//        yaw = t.eulerAngles.y;
//        roll = t.eulerAngles.z;
//        x = t.position.x;
//        y = t.position.y;
//        z = t.position.z;
//    }

//    public void Translate(Vector3 translation)
//    {
//        Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

//        x += rotatedTranslation.x;
//        y += rotatedTranslation.y;
//        z += rotatedTranslation.z;
//    }

//    public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
//    {
//        yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
//        pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
//        roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);

//        x = Mathf.Lerp(x, target.x, positionLerpPct);
//        y = Mathf.Lerp(y, target.y, positionLerpPct);
//        z = Mathf.Lerp(z, target.z, positionLerpPct);
//    }

//    public void UpdateTransform(Transform t)
//    {
//        t.eulerAngles = new Vector3(pitch, yaw, roll);
//        t.position = new Vector3(x, y, z);
//    }
}
