using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Controls;

public class StartTextController : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxScale = 1.2f;
    private bool grow = true;
    private bool inputAllowed = false;
    private bool inputPressed = false;
    void Start()
    {
        Time.timeScale = 1;
        GetComponent<Image>().enabled = false;
        StartCoroutine(DelayShowStart());
        Debug.Log("Mulai");
    }

    private void OnEnable()
    {
        grow = true;
        inputAllowed = false;
        inputPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        float currScale = transform.localScale.x;
        if ((grow && currScale < maxScale) || (!grow && currScale < 1))
        {
            grow = true;
            transform.localScale = new Vector3(currScale + Time.deltaTime * 0.25f, currScale + Time.deltaTime * 0.25f, currScale + Time.deltaTime * 0.25f);
        }
        else if ((!grow && currScale > 1) || (grow && currScale > maxScale))
        {
            grow = false;
            transform.localScale = new Vector3(currScale - Time.deltaTime * 0.25f, currScale - Time.deltaTime * 0.25f, currScale - Time.deltaTime * 0.25f);
        }

        if (inputAllowed && !inputPressed)
        {
            InputSystem.onAnyButtonPress.CallOnce(e => ButtonPressed());
            Debug.Log("Test");
        } else if (inputAllowed && inputPressed)
        {
            inputAllowed = false;
            inputPressed = false;
            SceneManager.LoadScene("Player Selection", LoadSceneMode.Single);
        }
    }

    private IEnumerator DelayShowStart()
    {
        Debug.Log("Mulai menunggu");
        yield return new WaitForSecondsRealtime(2.5f);
        Debug.Log("sudah selesai menunggu");
        GetComponent<Image>().enabled = true;
        inputAllowed = true;
    }

    private void ButtonPressed()
    {
        inputPressed = true;
    }
}
