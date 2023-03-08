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

        List<float> p1_bound = new List<float>(Boundaries.HACKER_BOUND);
        List<float> p2_bound = new List<float>(Boundaries.HACKER_BOUND);

        float p2b2 = Mathf.Abs(p2_bound[3]);
        float p2b3 = Mathf.Abs(p2_bound[2]);
        p2_bound[2] = p2b2;
        p2_bound[3] = p2b3;

        Player1.gameObject
            .GetComponent<PlayerController>()
            .setIsPlayer1(true)
            .setPosition(new Vector3(-10, 0, 0))
            .setBoundaries(p1_bound);

        Player2.gameObject
            .GetComponent<PlayerController>()
            .setIsPlayer1(false)
            .setPosition(new Vector3(10, 0, 0))
            .setBoundaries(p2_bound);

        Player2.GetComponent<SpriteRenderer>().flipX = true;
    }
}

public static class Boundaries {
    public static List<float> HACKER_BOUND = new List<float>() { 6.0f, -8.0f, -9.0f, -12.0f } ;
}