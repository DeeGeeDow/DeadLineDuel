using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUIController : MonoBehaviour
{
    public bool isPlayer1;
    public bool isPaused = false;
    public PauseOption option;
    public GameEvent UnpauseEvent;


    [Header("Sprites")]
    public Sprite ResumeSpriteIdle;
    public Sprite ResumeSpritePress;
    public Sprite RestartSpriteIdle;
    public Sprite RestartSpritePress;
    public Sprite ExitSpriteIdle;
    public Sprite ExitSpritePress;

    public Image resumeImage;
    public Image restartImage;
    public Image exitImage;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip switchClip;
    public AudioClip pressClip;

    public float pressCD = 0.1f;

    public void Update()
    {
        switch (option)
        {
            case PauseOption.RESUME:
                ResumeHover();
                break;
            case PauseOption.RESTART:
                RestartHover();
                break;
            case PauseOption.EXIT:
                ExitHover();
                break;
            default:
                break;
        }
        /*
        if(chooseOptionAvailable && chooseOptionButtonTriggered)
        {
            chooseOptionAvailable = false;
            switch (option)
            {
                case PauseOption.RESUME:
                    ResumeChosen();
                    break;
                case PauseOption.RESTART:
                    RestartChosen();
                    break;
                case PauseOption.EXIT:
                    ExitChosen();
                    break;
                default:
                    break;
            }
            StartCoroutine(optionChosen());
        }

        if(Mathf.Abs(moveOptionY) > 0 && moveOptionAvailable)
        {
            int numOfOptions = System.Enum.GetNames(typeof(PauseOption)).Length;
            if(moveOptionY < 0)
            {
                if ((int) option < numOfOptions - 1) option++;
            }
            else
            {
                if ((int)option > 0) option--;
            }
        }*/
    }

    
    public void PauseTriggered(Component sender, object data)
    {
        if(!isPaused)
        {
            isPaused = true;
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(true);   
            }
            enabled = true;
            isPlayer1 = sender.GetComponent<PlayerController>().isPlayer1;
            option = PauseOption.RESUME;
        }
    }

    private void ResumeHover()
    {
        resumeImage.color = new Color(200, 200, 200);
    }

    private void ResumeChosen()
    {
        resumeImage.sprite = ResumeSpritePress;
        StartCoroutine(Wait(pressCD, () =>
        {
            Resume();
            UnpauseEvent.Raise(this, null);
        }));
    }

    public void Resume()
    {
        isPaused = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    
    private void RestartHover()
    {
        restartImage.color = new Color(200, 200, 200);
    }

    private void RestartChosen()
    {
        restartImage.sprite = RestartSpritePress;
        StartCoroutine(Wait(pressCD, () =>
        {
            SceneManager.LoadScene("Hacker Map");
        }));
    }

    private void ExitHover()
    {
        exitImage.color = new Color(200, 200, 200);
    }

    private void ExitChosen()
    {
        exitImage.sprite = ExitSpritePress;
        StartCoroutine(Wait(pressCD, () =>
        {
            SceneManager.LoadScene("Start Menu");
        }));
    }

    public void UnpauseTriggered(Component sender, object data)
    {
        if(sender is PauseControl && sender.GetComponent<PlayerController>().isPlayer1 == isPlayer1 && isPaused)
        {
            Resume();
        }
    }

    public void OptionChanged(Component sender, object data)
    {
        if(sender.GetComponent<PlayerController>().isPlayer1 == isPlayer1)
        {
            Debug.Log("test");
            int numOfOptions = System.Enum.GetNames(typeof(PauseOption)).Length;
            if ((float) data < 0)
            {
                if ((int)option < numOfOptions - 1)
                {
                    audioSource.PlayOneShot(switchClip);
                    option++;
                }
            }
            else if((float) data >0)
            {
                if ((int)option > 0)
                {
                    audioSource.PlayOneShot(switchClip);
                    option--;
                }
            }
        }
    }

    public void OptionChosen(Component sender, object data)
    {
        if(sender.GetComponent<PlayerController>().isPlayer1 == isPlayer1)
        {
            audioSource.PlayOneShot(pressClip);
            switch (option)
            {
                case PauseOption.RESUME:
                    ResumeChosen();
                    break;
                case PauseOption.RESTART:
                    RestartChosen();
                    break;
                case PauseOption.EXIT:
                    ExitChosen();
                    break;
                default:
                    break;
            }
        }
    }

    public IEnumerator Wait(float delay, System.Action operation)
    {
        yield return new WaitForSecondsRealtime(delay);
        operation();
    }
}

public enum PauseOption
{
    RESUME,
    RESTART,
    EXIT
}
