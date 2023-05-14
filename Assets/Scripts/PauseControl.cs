using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseControl : MonoBehaviour
{
    public bool isPaused = false;
    private bool pauseButtonTriggered = false;
    public float pauseCD = 0.2f;
    private bool pauseAvailable = true;

    private float chooseOptionCD = 0.2f;
    private bool chooseOptionAvailable = false;
    private float moveOptionCD = 0.2f;
    private bool moveOptionAvailable = false;
    public GameEvent PauseEvent;
    public GameEvent UnpauseEvent;
    public GameEvent PauseOptionChanged;
    public GameEvent PauseOptionChosen;
    private void Update()
    {
        if (pauseButtonTriggered && pauseAvailable)
        {
            pauseAvailable = false;
            isPaused = !isPaused;
            PauseGame();

            StartCoroutine(pause());
        }
    }

    private void PauseGame()
    {
        if (isPaused)
        {
            PauseEvent.Raise(this, null);
            moveOptionAvailable = true;
            chooseOptionAvailable = true;
        }
        else
        {
            UnpauseEvent.Raise(this, null);
            moveOptionAvailable = false;
            chooseOptionAvailable = false;
        }
    }

    public void onPause(InputAction.CallbackContext context)
    {
        pauseButtonTriggered = context.action.triggered;
    }

    private IEnumerator pause()
    {
        yield return new WaitForSecondsRealtime(pauseCD);
        pauseAvailable = true;
    }

    public void onPauseOptionChanged(InputAction.CallbackContext context)
    {
        float val = context.ReadValue<Vector2>().y;
        if (val != 0)
        {
            pauseOptionChanged(Mathf.Sign(val));
        }
    }

    private void pauseOptionChanged(float d)
    {
        Debug.Log("isPaused : " + isPaused + ", moveAvail: " + moveOptionAvailable);
        if(isPaused && moveOptionAvailable){
            moveOptionAvailable = false;
            PauseOptionChanged.Raise(this, d);
            StartCoroutine(pauseOption());
        }
    }

    private IEnumerator pauseOption()
    {
        yield return new WaitForSecondsRealtime(moveOptionCD);
        moveOptionAvailable = true;
    }

    public void onPauseOptionChosen(InputAction.CallbackContext context)
    {
        if(context.action.triggered) pauseOptionChosen();
    }

    private void pauseOptionChosen()
    {
        if(isPaused && chooseOptionAvailable)
        {
            chooseOptionAvailable = false;
            PauseOptionChosen.Raise(this, null);
            StartCoroutine(selectPause());
        }
    }
    private IEnumerator selectPause()
    {
        yield return new WaitForSecondsRealtime(chooseOptionCD);
        chooseOptionAvailable = true;
    }
}
