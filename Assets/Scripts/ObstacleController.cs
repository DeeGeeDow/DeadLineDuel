using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public int hp = 3;
#nullable enable
    public Item? itemInside = null;
    public GameEvent PlayerGetItem;
    public GameEvent ObstacleDestroyed;

    // Update is called once per frame
    void Update()
    {
        // write code here
    }

    void decreaseHP()
    {
        hp--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            decreaseHP();
        }
        if (hp <= 0)
        {
            ObstacleDestroyed.Raise(this, null);
            if (itemInside != null)
            {
                PlayerGetItem.Raise(this, (other.gameObject.GetComponent<PlayerController>().isPlayer1, itemInside));
            }
            Destroy(gameObject);
        }
    }

    public ObstacleController setItem(Item item)
    {
        itemInside = item;
        return this;
    }
}