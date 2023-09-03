using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown
{
    private float duration;
    private System.Action operation; 

    public Cooldown()
    {
        this.duration = 0f;
        this.operation = () => { };
    }

    public Cooldown(float duration, System.Action operation)
    {
        this.duration = duration;
        this.operation = operation;
    }

    public void setDuration(float duration)
    {
        this.duration = duration;
    }

    public void setOperation(System.Action operation)
    {
        this.operation = operation;
    }

    public IEnumerator start()
    {
        yield return new WaitForSeconds(duration);
        operation();
    }
}
    
