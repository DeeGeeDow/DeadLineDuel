using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WinningManager : MonoBehaviour
{
    public bool QuitTriggered = false;
    public bool CharSelectionTriggered = false;
    public bool RestartTriggered = false;
    private StateManager stateMan;
    public PlayerInput playerInput;
    public void onQuit(InputAction.CallbackContext ctx)
    {
        QuitTriggered = ctx.action.triggered;
    }

    public void onCharSelect(InputAction.CallbackContext ctx)
    {
        CharSelectionTriggered = ctx.action.triggered;
    }

    public void onRestartTriggered(InputAction.CallbackContext ctx)
    {
        RestartTriggered = ctx.action.triggered;
    }

    public void Start()
    {
        stateMan = GameObject.Find("State").GetComponent<StateManager>();
    }

    public void Awake()
    {
        stateMan = GameObject.Find("State").GetComponent<StateManager>();
        if (stateMan.isPlayer1Win)
        {
            playerInput.SwitchCurrentControlScheme(stateMan.controlScheme1, stateMan.device1);
        }
        else
        {
            playerInput.SwitchCurrentControlScheme(stateMan.controlScheme2, stateMan.device2);
        }
    }

    public void Update()
    {
        if (QuitTriggered)
        {
            Quit();
        }
        else if (CharSelectionTriggered)
        {
            CharSelect();
        }
        else if (RestartTriggered)
        {
            Restart();
        }
    }

    public void Quit()
    {
        Destroy(stateMan.gameObject);
        LoadingData.sceneToLoad = "Start Menu";
        SceneManager.LoadScene("Loading Screen");
    }

    public void CharSelect()
    {
        Destroy(stateMan.gameObject);
        LoadingData.sceneToLoad = "Player Selection";
        SceneManager.LoadScene("Loading Screen");
    }

    public void Restart()
    {
        stateMan.isGameFinished = false;
        stateMan.isGameStarted = true;
        LoadingData.sceneToLoad = "Hacker Map";
        SceneManager.LoadScene("Loading Screen");
    }
}
