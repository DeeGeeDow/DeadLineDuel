using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerSkill : MonoBehaviour, Skill
{
    public float skillDuration = 16f;
    public float animationCharaDuration = 3.5f;
    public float animationSkillDuration = 3f;
    public float skillDelay = 3f;
    public GameEvent skillStarted;
    public GameEvent skillFinished;
    public GameObject skillPrefab;
    private GameObject skill;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if(!(skill is null))
        {
            skill.transform.eulerAngles = mainCamera.transform.eulerAngles;
        }
    }
    public void onCast()
    {
        Cooldown cooldown = new Cooldown(skillDuration, afterSkill);
        Cooldown delay = new Cooldown(skillDelay, beginSkill);
        Cooldown animationCD = new Cooldown(animationCharaDuration, afterAnimationFinsihed);

        //beginSkill();

        Debug.Log("Hacker Skill casted");
        StartCoroutine(delay.start());
        StartCoroutine(cooldown.start());
        StartCoroutine(animationCD.start());
    }

    public void beginSkill()
    {
        skill = Instantiate(skillPrefab, transform.parent);
        bool isPlayer1 = GetComponent<PlayerController>().isPlayer1;
        skill.transform.localPosition = ((isPlayer1) ? Vector3.right : Vector3.left) + Vector3.up;
        if (!isPlayer1)
        {
            skill.GetComponentInChildren<SpriteRenderer>().flipX = true;
            // collider_center = skill.GetComponentInChildren<BoxCollider>().center;
            // skill.GetComponentInChildren<BoxCollider>().center = new Vector3(collider_center.x * (-1), collider_center.y, collider_center.z);
        }

        Cooldown skillAnimationCD = new Cooldown(animationSkillDuration, () =>
        {
            Destroy(skill);
            skill = null;
            skillStarted.Raise(this, null);
        });
        StartCoroutine(skillAnimationCD.start());

    }
    public void afterSkill()
    {
        skillFinished.Raise(this, true);
        Debug.Log("after skill");
    }
    public void afterAnimationFinsihed()
    {
        Debug.Log("aodskamfamof");
        skillFinished.Raise(this, false);
    }
}
