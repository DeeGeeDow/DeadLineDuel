using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistSkill : MonoBehaviour, Skill
{
    public void onCast()
    {
        Debug.Log("Scientist Skill casted");
    }
}
