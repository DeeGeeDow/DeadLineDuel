using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistSkill : MonoBehaviour, Skill
{
    public float skillDuration = 0.5f;
    public float skillDelay = 0.1f;
    public GameEvent skillFinished;
    public GameObject skillPrefab;
    private GameObject skill;
    public void onCast()
    {
        Cooldown cooldown = new Cooldown();
        Cooldown delay = new Cooldown();

        cooldown.setDuration(skillDuration);
        cooldown.setOperation(afterSkill);
        delay.setDuration(skillDelay);
        delay.setOperation(beginSkill);

        Debug.Log("Scientist Skill casted");
        StartCoroutine(delay.start());
        StartCoroutine(cooldown.start());
    }

    public void beginSkill()
    {
        skill = Instantiate(skillPrefab, transform.parent);
        bool isPlayer1 = GetComponent<PlayerController>().isPlayer1;
        skill.transform.localPosition = (isPlayer1) ? Vector3.right : Vector3.left;
        if (!isPlayer1)
        {
            skill.GetComponentInChildren<SpriteRenderer>().flipX = true;
            Vector3 collider_center = skill.GetComponentInChildren<BoxCollider>().center;
            skill.GetComponentInChildren<BoxCollider>().center = new Vector3(collider_center.x * (-1), collider_center.y, collider_center.z);
        }

    }
    public void afterSkill()
    {
        skillFinished.Raise(this, true);
        Destroy(skill);
        Debug.Log("after skill");
    }
}
