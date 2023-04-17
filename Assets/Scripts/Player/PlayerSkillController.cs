using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillController : MonoBehaviour
{
    private bool skillButtonTriggered;
    [SerializeField]
    [RequireInterface(typeof(Skill))]
    private Object _skill;
    private Skill skill => _skill as Skill;
    private float skillCooldown = 30f;
    private float skillProgress = 0f;
    private bool isSkillReady = false;
    public GameEvent SkillCasted;
    private void Update()
    {
        SkillProgress();
        if(skillButtonTriggered && isSkillReady) castSkill();
    }

    public void onSkillCast(InputAction.CallbackContext context)
    {
        skillButtonTriggered = context.action.triggered;
    }

    private void castSkill()
    {
        skill.onCast();
        this.isSkillReady = false;
        SkillCasted.Raise(this, GetComponent<PlayerController>().isPlayer1);
    }

    private void SkillProgress()
    {
        if(skillProgress < skillCooldown)
        {
            skillProgress += Time.deltaTime;
            //SkillProgressUpdated.Raise(this, skillProgress);
            Debug.Log(skillProgress);
        }
        else
        {
            isSkillReady = true;
        }
    }
}
