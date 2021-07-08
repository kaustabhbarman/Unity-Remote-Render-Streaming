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
        if (keyboard == null)
        {
            keyboard = remoteInputController.GetCurrentKeyboard();
        }

        if(keyboard != null)
        {
            keyboard.onTextInput += OnTextInput;
        }

        string timeText     = System.DateTime.Now.ToString() + "\n";
        string keyText      = lastKey.ToString();
        //Debug.Log("LAST KEY: " + lastKey.ToString());
        displayText.text    = timeText + keyText;
    }

    void OnTextInput(char c)
    {
        //Debug.Log("ONTEXTINPUT CHAR: " + c);
        lastKey = c;
    }

}
