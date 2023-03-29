using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillController : MonoBehaviour
{
    private bool skillButtonTriggered;
    private Skill skill;
    private void Update()
    {
        if(skillButtonTriggered) castSkill();
    }

    public void onSkillCast(InputAction.CallbackContext context)
    {
        skillButtonTriggered = context.action.triggered;
    }

    private void castSkill()
    {
        skill.onCast();
    }
}
