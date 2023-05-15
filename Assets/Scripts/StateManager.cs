using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    private Sprite P1Head;
    private Sprite P2Head;
    private Sprite WinnerBody;
    private PlayerTypes p1;
    private PlayerTypes p2;

    [Header("Prefabs")]
    public GameObject Scientist;
    public GameObject Hacker;
    public GameObject Engineer;
    public GameObject Painter;

    [Header("Head Sprite")]
    public Sprite ScientistHead;
    public Sprite HackerHead;
    public Sprite EngineerHead;
    public Sprite PainterHead;

    [Header("Full Body Sprite")]
    public Sprite ScientistBody;
    public Sprite HackerBody;
    public Sprite EngineerBody;
    public Sprite PainterBody;

    [Header("Game State")]
    public bool isGameFinished = false;
    public bool isGameStarted = false;
    public bool isPlayer1Win = false;

    [Header("Input Scheme and Device")]
    public string controlScheme1;
    public string controlScheme2;
    public InputDevice device1;
    public InputDevice device2;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Start()
    {
        p1 = PlayerTypes.SCIENTIST;
        p2 = PlayerTypes.HACKER;
        Player1 = Scientist;
        Player2 = Hacker;
        P1Head = ScientistHead;
        P2Head = HackerHead;
        Debug.Log(Player2);
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

    public Sprite GetWinnerBody()
    {
        if (isPlayer1Win)
        {
            switch (p1)
            {
                case PlayerTypes.SCIENTIST:
                    WinnerBody = ScientistBody;
                    break;
                case PlayerTypes.HACKER:
                    WinnerBody = HackerBody;
                    break;
                case PlayerTypes.ENGINEER:
                    WinnerBody = EngineerBody;
                    break;
                case PlayerTypes.PAINTER:
                    WinnerBody = PainterBody;
                    break;
                default:
                    WinnerBody = ScientistBody;
                    break;
            }
        }
        else
        {
            switch (p2)
            {
                case PlayerTypes.SCIENTIST:
                    WinnerBody = ScientistBody;
                    break;
                case PlayerTypes.HACKER:
                    WinnerBody = HackerBody;
                    break;
                case PlayerTypes.ENGINEER:
                    WinnerBody = EngineerBody;
                    break;
                case PlayerTypes.PAINTER:
                    WinnerBody = PainterBody;
                    break;
                default:
                    WinnerBody = ScientistBody;
                    break;
            }
        }
        return WinnerBody;
    }

    public PlayerTypes GetWinner()
    {
        return (isPlayer1Win) ? p1 : p2;
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
            p1 = players[0];
            p2 = players[1];
        }
    }
}
