using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    void Awake()
    {
        PlayerInput.Instantiate(playerPrefab,
            controlScheme: "Keyboard1", pairWithDevices: new InputDevice[] { Keyboard.current, Gamepad.current }).gameObject
            .GetComponent<PlayerController>()
            .setIsPlayer1(true)
            .setPosition(new Vector3(-5, 0, 0));

        PlayerInput.Instantiate(playerPrefab,
            controlScheme: "Keyboard2", pairWithDevice: Keyboard.current).gameObject
            .GetComponent<PlayerController>()
            .setIsPlayer1(false)
            .setPosition(new Vector3(5, 0, 0));
    }
}