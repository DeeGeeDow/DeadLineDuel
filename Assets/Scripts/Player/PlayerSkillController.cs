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
    public Animator animator;
    private void Update()
    {
        SkillProgress();
        if (skillButtonTriggered && isSkillReady) castSkill();
    }

    public void onSkillCast(InputAction.CallbackContext context)
    {
        skillButtonTriggered = context.action.triggered;
    }

    private void castSkill()
    {
        skill.onCast();
        animator.SetTrigger("Ult");
        this.isSkillReady = false;
        this.skillProgress = 0;
        SkillCasted.Raise(this, GetComponent<PlayerController>().isPlayer1);
        GetComponent<PlayerMovementController>().setHalfSpeed();
    }

    public void SkillFinished(Component sender, object data)
    {
        // data consists "is the entire skill is finished?"

        // removed: obvious check (always true)??
        //if (sender.GetComponent<PlayerController>().isPlayer1 == GetComponent<PlayerController>().isPlayer1)
        //{
        if ((bool)data && sender.GetComponentInChildren<PlayerController>().isPlayer1 == GetComponent<PlayerController>().isPlayer1)
        {
            GetComponent<PlayerMovementController>().setDoubleSpeed();
        }
        animator.SetTrigger("UltFinished");
        //}
    }
    private void SkillProgress()
    {
        if (skillProgress < skillCooldown)
        {
            skillProgress += Time.deltaTime;
            //SkillProgressUpdated.Raise(this, skillProgress);
        }
        else
        {
            isSkillReady = true;
        }
    }

    [ContextMenu("Cast Skill")]
    public void CastSkillTest()
    {
        castSkill();
    }
}
