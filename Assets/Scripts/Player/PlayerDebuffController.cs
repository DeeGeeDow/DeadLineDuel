using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebuffController : MonoBehaviour
{
    private void EnableAttack(bool isEnabled)
    {
        GetComponent<PlayerShootController>().enabled = isEnabled;
        //if (isEnabled) Debug.Log("mati");
        //else Debug.Log("nyala");
    }

    private void EnableMovement(bool isEnabled)
    {
        GetComponent<PlayerMovementController>().enabled = isEnabled;
    }

    public void HackerSkillCasted(Component sender, object data)
    {
        if (sender is HackerSkill && sender.GetComponent<PlayerController>().isPlayer1 != GetComponent<PlayerController>().isPlayer1)
        {
            //Debug.Log("Harusnya awl awl");
            EnableAttack(false);
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0.8f);
        }
    }

    public void HackerSkillFinished(Component sender, object data)
    {
        // data consists "is the entire skill is finished?"
        if (sender is HackerSkill && sender.GetComponent<PlayerController>().isPlayer1 != GetComponent<PlayerController>().isPlayer1)
        {
            if((bool)data)
            {
                EnableAttack(true);
                GetComponent<SpriteRenderer>().color = new Color(1,1,1);
                //Debug.Log("Harusnya akhir kahir");
            }
        }
    }
}
