using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject player1Prefab, player2Prefab;
    private PlayerInput Player1, Player2;
    public PauseState pauseState = PauseState.UNPAUSED;
    public Image Player1Head, Player2Head;
    public bool gameEnded = false;
    public bool isPlayer1Win = false;
    private StateManager stateMan;
    public GameEvent ShowVictory;
    public GameEvent ShowReady;
    public float victoryShowDuration = 5f;
    public AudioSource audioSource;
    public AudioClip ReadyGo;
    public AudioClip GameBGM;
    public AudioClip VictoryAudio;
    public float ReadyGoDuration = 4f;
    public float VictoryAudioDelay = 3f;
    void Awake()
    {
        Time.timeScale = 1f;
        var gamepadCount = Gamepad.all.Count;
        stateMan = GameObject.Find("State").GetComponent<StateManager>();
        player1Prefab = stateMan.GetPlayer1();
        player2Prefab = stateMan.GetPlayer2();
        Player1Head.sprite = stateMan.GetPlayer1Head();
        Player2Head.sprite = stateMan.GetPlayer2Head();
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

    public void PauseGame(Component sender, object data)
    {
        if (pauseState == PauseState.UNPAUSED)
        {
            Time.timeScale = 0;
            Player1.GetComponent<PlayerMovementController>().enabled = false;
            Player1.GetComponent<PlayerShootController>().enabled =    false;
            Player1.GetComponent<PlayerSkillController>().enabled =    false;
            Player2.GetComponent<PlayerMovementController>().enabled = false;
            Player2.GetComponent<PlayerShootController>().enabled =    false;
            Player2.GetComponent<PlayerSkillController>().enabled =    false;
            Player1.GetComponent<PauseControl>().isPaused = true;
            Player2.GetComponent<PauseControl>().isPaused = true;
            if (sender.GetComponent<PlayerController>().isPlayer1) pauseState = PauseState.PAUSED_BY_P1;
            else pauseState = PauseState.PAUSED_BY_P2;
        }
    }

    public void UnpauseGame(Component sender, object data)
    {
        bool unpausedByP1 = false;
        if (sender is PauseControl) unpausedByP1 = sender.GetComponent<PlayerController>().isPlayer1;
        else if (sender is PauseUIController) unpausedByP1 = ((PauseUIController)sender).isPlayer1;
        if((unpausedByP1 && pauseState == PauseState.PAUSED_BY_P1) || (!unpausedByP1 && pauseState == PauseState.PAUSED_BY_P2))
        {
            Time.timeScale = 1;
            Player1.GetComponent<PlayerMovementController>().enabled = true;
            Player1.GetComponent<PlayerShootController>().enabled =    true;
            Player1.GetComponent<PlayerSkillController>().enabled =    true;
            Player2.GetComponent<PlayerMovementController>().enabled = true;
            Player2.GetComponent<PlayerShootController>().enabled =    true;
            Player2.GetComponent<PlayerSkillController>().enabled =    true;
            Player1.GetComponent<PauseControl>().isPaused = false;
            Player2.GetComponent<PauseControl>().isPaused = false;
            pauseState = PauseState.UNPAUSED;
        }
    }

    public void PauseByGame(bool pause)
    {
        if (pause) 
        {
            Time.timeScale = 0;
            pauseState = PauseState.PAUSED_BY_GAME;
        }
        else
        {
            Time.timeScale = 1;
            pauseState = PauseState.UNPAUSED;
        }
    }

    public void Victory()
    {
        PauseByGame(true);
        audioSource.Pause();
        audioSource.clip = VictoryAudio;
        audioSource.PlayDelayed(VictoryAudioDelay);
        ShowVictory.Raise(this, null);
        StartCoroutine(VictoryCoroutine());
    }

    public void Start()
    {
        PauseByGame(true);
        audioSource.PlayOneShot(ReadyGo);
        StartCoroutine(ReadyGoCoroutine());
    }

    public IEnumerator ReadyGoCoroutine()
    {
        yield return new WaitForSecondsRealtime(ReadyGoDuration);
        PauseByGame(false);
        audioSource.clip = GameBGM;
        audioSource.Play();
    }

    public void Update()
    {
        if (gameEnded) Victory();
    }
    public void onChangedHeart(Component sender, object data)
    {
        // someone is dying
        if(sender is PlayerHeartController && (int)data <= 0 && !gameEnded)
        {
            gameEnded = true;
            isPlayer1Win = !(sender.GetComponent<PlayerController>().isPlayer1);
            stateMan.isGameFinished = true;
            stateMan.isPlayer1Win = isPlayer1Win;
        }
    }
    private IEnumerator VictoryCoroutine()
    {
        yield return new WaitForSecondsRealtime(victoryShowDuration + VictoryAudioDelay);
        SceneManager.LoadScene("Winning Scene");
    }
}

public enum PauseState
{
    UNPAUSED,
    PAUSED_BY_P1,
    PAUSED_BY_P2,
    PAUSED_BY_GAME
}