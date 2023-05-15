using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChoosePlayerManager : MonoBehaviour
{
    public PlayerTypes Player1;
    public PlayerTypes Player2;
    public PlayerInput Player1Input;
    public PlayerInput Player2Input;
    public bool P1Chosen = false;
    public bool P2Chosen = false;

    public GameEvent GameStart;

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
            Player2Input.SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
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
        if(P2Chosen & P1Chosen)
        {
            PlayerTypes[] players = { Player1, Player2 };
            GameStart.Raise(this, players);
            SceneManager.LoadScene("Hacker Map");
        }

    }

    public void ChosenChara(Component sender, object data)
    {
        if(sender is ChoosePlayerController)
        {
            bool p1 = ((ChoosePlayerController)sender).isPlayer1;
            if (p1)
            {
                P1Chosen = (bool) data;
                Player1 = ((ChoosePlayerController)sender).player;
            }
            else
            {
                P2Chosen = (bool) data;
                Player2 = ((ChoosePlayerController)sender).player;
            }
        }
    }
    
}
