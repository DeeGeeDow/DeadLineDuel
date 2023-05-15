using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private GameObject Player1;
    private GameObject Player2;
    private Sprite P1Head;
    private Sprite P2Head;

    [Header("Prefabs")]
    public GameObject Scientist;
    public GameObject Hacker;
    public GameObject Engineer;
    public GameObject Painter;

    [Header("Sprite")]
    public Sprite ScientistHead;
    public Sprite HackerHead;
    public Sprite EngineerHead;
    public Sprite PainterHead;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public GameObject GetPlayer1()
    {
        return Player1;
    }

    public GameObject GetPlayer2()
    {
        return Player2;
    }

    public Sprite GetPlayer1Head()
    {
        return P1Head;
    }

    public Sprite GetPlayer2Head()
    {
        return P2Head;
    }

    public void AssignPlayer(Component sender, object data)
    {
        if (sender is ChoosePlayerManager)
        {
            PlayerTypes[] players = (PlayerTypes[])data;
            switch (players[0])
            {
                case PlayerTypes.SCIENTIST:
                    Player1 = Scientist;
                    P1Head = ScientistHead;
                    break;
                case PlayerTypes.HACKER:
                    Player1 = Hacker;
                    P1Head = HackerHead;
                    break;
                case PlayerTypes.ENGINEER:
                    Player1 = Engineer;
                    P1Head = EngineerHead;
                    break;
                case PlayerTypes.PAINTER:
                    Player1 = Painter;
                    P1Head = PainterHead;
                    break;
                default:
                    Player1 = Scientist;
                    P1Head = ScientistHead;
                    break;
            }
            switch (players[1])
            {
                case PlayerTypes.SCIENTIST:
                    Player2 = Scientist;
                    P2Head = ScientistHead;
                    break;
                case PlayerTypes.HACKER:
                    Player2 = Hacker;
                    P2Head = HackerHead;
                    break;
                case PlayerTypes.ENGINEER:
                    Player2 = Engineer;
                    P2Head = EngineerHead;
                    break;
                case PlayerTypes.PAINTER:
                    Player2 = Painter;
                    P2Head = PainterHead;
                    break;
                default:
                    Player2 = Scientist;
                    P2Head = ScientistHead;
                    break;
            }
        }
    }
}
