using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown
{
    private float duration;
    private System.Action operation; 

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
    
