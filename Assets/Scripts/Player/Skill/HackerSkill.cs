using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerSkill : MonoBehaviour, Skill
{
    public float skillDuration = 2.0f;
    public float animationSkillDuration = 0.5f;
    //public float skillDelay = 0.1f;
    public GameEvent skillStarted;
    public GameEvent skillFinished;
    public GameObject skillPrefab;
    private GameObject skill;
    public void onCast()
    {
        Cooldown cooldown = new Cooldown();
        Cooldown animationCD = new Cooldown();

        cooldown.setDuration(skillDuration);
        cooldown.setOperation(afterSkill);
        //delay.setDuration(skillDelay);
        //delay.setOperation(beginSkill);

        animationCD.setDuration(animationSkillDuration);
        animationCD.setOperation(afterAnimationFinsihed);

        beginSkill();

        Debug.Log("Hacker Skill casted");
        //StartCoroutine(delay.start());
        StartCoroutine(cooldown.start());
        StartCoroutine(animationCD.start());
    }

    public void beginSkill()
    {
        skill = Instantiate(skillPrefab, transform.parent);
        bool isPlayer1 = GetComponent<PlayerController>().isPlayer1;
        skill.transform.localPosition = (isPlayer1) ? Vector3.right : Vector3.left;
        if (!isPlayer1)
        {
            skill.GetComponentInChildren<SpriteRenderer>().flipX = true;
            // collider_center = skill.GetComponentInChildren<BoxCollider>().center;
            // skill.GetComponentInChildren<BoxCollider>().center = new Vector3(collider_center.x * (-1), collider_center.y, collider_center.z);
        }
        skillStarted.Raise(this, null);
    }
    public void afterSkill()
    {
        skillFinished.Raise(this, true);
        Debug.Log("after skill");
    }
    public void afterAnimationFinsihed()
    {
        Debug.Log("aodskamfamof");
        Destroy(skill);
        skillFinished.Raise(this, false);
    }
}
