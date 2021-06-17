using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField]
    private Text displayText;

    private char lastKey = ' ';

    private Keyboard remote_keyboard;

    // Update is called once per frame
    void Update()
    {
        string timeText     = System.DateTime.Now.ToString() + "\n";
        string keyText      = lastKey.ToString();
        Debug.Log("LAST KEY: " + lastKey.ToString());
        displayText.text    = timeText + keyText;
    }

    public void SetDevice(InputDevice device, bool add=false)
    {
        switch (device)
        {
            case Keyboard keyboard:
                remote_keyboard = add ? keyboard : null;
                if(add)
                    remote_keyboard.onTextInput += OnTextInput;
                return;
        }
    }
    void OnTextInput(char c)
    {
        Debug.Log("ONTEXTINPUT CHAR: " + c);
        lastKey = c;
    }

}
