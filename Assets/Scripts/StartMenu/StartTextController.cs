using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class StartTextController : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxScale = 1.2f;
    private bool grow = true;
    private bool inputAllowed = false;
    void Start()
    {
        GetComponent<Image>().enabled = false;
        StartCoroutine(DelayShowStart());
    }

    // Update is called once per frame
    void Update()
    {
        float currScale = transform.localScale.x;
        if((grow && currScale < maxScale) || (!grow && currScale < 1))
        {
            grow = true;
            transform.localScale = new Vector3(currScale + Time.deltaTime*0.25f, currScale + Time.deltaTime*0.25f, currScale + Time.deltaTime*0.25f);
        }
        else if((!grow && currScale > 1) || (grow && currScale > maxScale))
        {
            grow = false;
            transform.localScale = new Vector3(currScale - Time.deltaTime*0.25f, currScale - Time.deltaTime*0.25f, currScale - Time.deltaTime*0.25f);
        }

        if (inputAllowed)
        {
            InputSystem.onAnyButtonPress.CallOnce(e => { SceneManager.LoadScene("Player Selection"); Debug.Log("Dari Dalem"); });
            Debug.Log("Test");

        }
    }

    private IEnumerator DelayShowStart()
    {
        yield return new WaitForSeconds(2.5f);
        GetComponent<Image>().enabled = true;
        inputAllowed = true;
    }
}
