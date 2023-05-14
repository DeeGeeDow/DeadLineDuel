using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebuffController : MonoBehaviour
{
    private void EnableAttack(bool isEnabled)
    {
        GetComponent<PlayerShootController>().enabled = isEnabled;
    }

    private void EnableMovement(bool isEnabled)
    {
        GetComponent<PlayerMovementController>().enabled = isEnabled;
    }

    public void HackerSkillCasted(Component sender, object data)
    {
        if (sender is HackerSkill && sender.GetComponent<PlayerController>().isPlayer1 != sender.GetComponent<PlayerController>().isPlayer1)
        {
            EnableAttack(false);
        }
    }

    public void HackerSkillFinished(Component sender, object data)
    {
        // data consists "is the entire skill is finished?"
        if (sender is HackerSkill && sender.GetComponent<PlayerController>().isPlayer1 != sender.GetComponent<PlayerController>().isPlayer1)
        {
            if((bool)data)
            {
                EnableAttack(true);
            }
        }
    }
}
