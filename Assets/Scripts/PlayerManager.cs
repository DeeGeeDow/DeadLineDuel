using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameObject player1Prefab, player2Prefab;
    private PlayerInput Player1, Player2;
    void Awake()
    {
        var gamepadCount = Gamepad.all.Count;
        if (gamepadCount >= 2)
        {
            Player1 = PlayerInput.Instantiate(player1Prefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[0]);
            Player2 = PlayerInput.Instantiate(player2Prefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[1]);
        }
        else if (gamepadCount == 1)
        {
            Player1 = PlayerInput.Instantiate(player1Prefab, controlScheme: "Keyboard1", pairWithDevice: Keyboard.current);
            Player2 = PlayerInput.Instantiate(player2Prefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[0]);
        }
        else
        {
            Player1 = PlayerInput.Instantiate(player1Prefab, controlScheme: "Keyboard1", pairWithDevice: Keyboard.current);
            Player2 = PlayerInput.Instantiate(player2Prefab, controlScheme: "Keyboard2", pairWithDevice: Keyboard.current);
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