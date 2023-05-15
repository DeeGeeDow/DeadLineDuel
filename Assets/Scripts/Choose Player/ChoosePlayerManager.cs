using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChoosePlayerManager : MonoBehaviour
{
    public PlayerTypes Player1;
    public PlayerTypes Player2;
    public PlayerInput Player1Input;
    public PlayerInput Player2Input;


    private void Awake()
    {
        var gamepad = Gamepad.all.Count;
        if (gamepad >= 2)
        {
            Player1Input.SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
            Player2Input.SwitchCurrentControlScheme("Gamepad", Gamepad.all[1]);
        }
        else if (gamepad == 1)
        {
            Player1Input.SwitchCurrentControlScheme("Keyboard1", Keyboard.current);
            Player2Input.SwitchCurrentControlScheme("Gamepad", Gamepad.all[1]);
        }
        else
        {
            Player1Input.SwitchCurrentControlScheme("Keyboard1", Keyboard.current);
            Player2Input.SwitchCurrentControlScheme("Keyboard2", Keyboard.current);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
