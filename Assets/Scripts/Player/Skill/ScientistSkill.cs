using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistSkill : MonoBehaviour, Skill
{
    public float skillDuration = 0.5f;
    public GameEvent skillFinished;
    public GameObject skillPrefab;
    private GameObject skill;
    public void onCast()
    {
        Cooldown cooldown = new Cooldown();
        cooldown.setDuration(skillDuration);
        cooldown.setOperation(afterSkill);
        Debug.Log("Scientist Skill casted");
        skill = Instantiate(skillPrefab, transform);
        bool isPlayer1 = GetComponent<PlayerController>().isPlayer1;
        skill.transform.position = (isPlayer1) ? new Vector3(1,0,0) : new Vector3(-1,0,0);
        StartCoroutine(cooldown.start());
    }

    public void afterSkill()
    {
        skillFinished.Raise(this, null);
        Destroy(skill);
        Debug.Log("after skill");
    }
}
