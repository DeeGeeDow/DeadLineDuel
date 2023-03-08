using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private PlayerInput Player1, Player2;
    void Awake()
    {
        var gamepadCount = Gamepad.all.Count;
        if (gamepadCount >= 2)
        {
            Player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[0]);
            Player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[1]);
        }
        else if (gamepadCount == 1)
        {
            Player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[0]);
            Player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard2", pairWithDevice: Keyboard.current);
        }
        else
        {
            Player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard1", pairWithDevice: Keyboard.current);
            Player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard2", pairWithDevice: Keyboard.current);
        }

        Player1.gameObject
            .GetComponent<PlayerController>()
            .setIsPlayer1(true)
            .setPosition(new Vector3(-5, 0, 0));

        Player2.gameObject
            .GetComponent<PlayerController>()
            .setIsPlayer1(false)
            .setPosition(new Vector3(5, 0, 0));

        Player2.GetComponent<SpriteRenderer>().flipX = true;
    }
}