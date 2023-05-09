using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIController : MonoBehaviour
{
    public float skillTime = 0f;
    public float timeToRecharge = 30f;
    public bool skillCasted = false;
    public bool isPlayer1 = true;
    private void Update()
    {
        UpdateBar();
        if (skillCasted) ResetBar();
    }

    public void onSkillCasted(Component sender, object data)
    {
        if(isPlayer1 == (bool)data)
        {
            this.skillTime = 0;
        }
    }

    public void UpdateBar()
    {
        skillTime += Time.deltaTime;
        if(skillTime < timeToRecharge)
        {
            DrawBar();
        }
        else
        {
            skillTime = timeToRecharge;
        }
    }

    public void ResetBar()
    {
        skillTime = 0f;
        DrawBar();
    }

    public void DrawBar()
    {
        transform.GetChild(0).GetComponent<Image>().fillAmount = Mathf.Min(skillTime / timeToRecharge, 1);
    }
}
