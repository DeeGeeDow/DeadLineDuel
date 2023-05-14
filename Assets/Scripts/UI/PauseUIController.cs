using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseUIController : MonoBehaviour
{
    public bool isPlayer1;
    public bool isPaused = false;
    public PauseOption option;


    [Header("Sprites")]
    public Sprite ResumeSpriteIdle;
    public Sprite ResumeSpriteHover;
    public Sprite RestartSpriteIdle;
    public Sprite RestartSpriteHover;
    public Sprite ExitSpriteIdle;
    public Sprite ExitSpriteHover;

    public Image resumeImage;
    public Image restartImage;
    public Image exitImage;

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
        resumeImage.sprite = ResumeSpriteHover;
        restartImage.sprite = RestartSpriteIdle;
        exitImage.sprite = ExitSpriteIdle;
    }

    private void ResumeChosen()
    {
        isPaused = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    
    private void RestartHover()
    {
        resumeImage.sprite = ResumeSpriteIdle;
        restartImage.sprite = RestartSpriteHover;
        exitImage.sprite = ExitSpriteIdle;
    }

    private void RestartChosen()
    {

    }

    private void ExitHover()
    {
        resumeImage.sprite = ResumeSpriteIdle;
        restartImage.sprite = RestartSpriteIdle;
        exitImage.sprite = ExitSpriteHover;
    }

    private void ExitChosen()
    {

    }

    public void UnpauseTriggered(Component sender, object data)
    {
        if(sender.GetComponent<PlayerController>().isPlayer1 == isPlayer1 && isPaused)
        {
            ResumeChosen();
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
                if ((int)option < numOfOptions - 1) option++;
            }
            else if((float) data >0)
            {
                if ((int)option > 0) option--;
            }
        }
    }

    public void OptionChosen(Component sender, object data)
    {
        if(sender.GetComponent<PlayerController>().isPlayer1 == isPlayer1)
        {
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
}

public enum PauseOption
{
    RESUME,
    RESTART,
    EXIT
}
