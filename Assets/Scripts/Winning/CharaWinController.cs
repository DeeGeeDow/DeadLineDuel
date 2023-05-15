using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaWinController : MonoBehaviour
{
    public Image img;
    public Text player;
    public Text playerName;

    [Header("Names")]
    public string ScientistName = "CHEMMI";
    public string HackerName = "BYTE";
    public string EngineerName = "BILLDER";
    public string PainterName = "FRUSH";
    public void Start()
    {
        var stateMan = GameObject.Find("State").GetComponent<StateManager>();
        img.sprite = stateMan.GetWinnerBody();
        player.text = (stateMan.isPlayer1Win) ? "PLAYER 1" : "PLAYER 2";
        PlayerTypes winner = stateMan.GetWinner();
        switch (winner)
        {
            case PlayerTypes.SCIENTIST:
                playerName.text = ScientistName;
                break;
            case PlayerTypes.HACKER:
                playerName.text = HackerName;
                break;
            case PlayerTypes.ENGINEER:
                playerName.text = EngineerName;
                break;
            case PlayerTypes.PAINTER:
                playerName.text = PainterName;
                break;
            default:
                break;
        }
    }
}
