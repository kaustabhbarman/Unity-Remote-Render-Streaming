using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField]
    private Text displayText;

    [SerializeField]
    private RemoteInputController remoteInputController;

    private char lastKey = ' ';

    private Keyboard keyboard;

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = remoteInputController.GetCurrentKeyboard();
        if(keyboard != null)
        {
            keyboard.onTextInput += OnTextInput;
        }

        string timeText     = System.DateTime.Now.ToString() + ":" + System.DateTime.Now.Millisecond.ToString() + "\n";
        string keyText      = lastKey.ToString();
        displayText.text    = timeText + keyText;
    }

    void OnTextInput(char c)
    {
        lastKey = c;
    }

}
