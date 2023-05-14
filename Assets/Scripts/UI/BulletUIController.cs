using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberOfBullet;
    public bool isPlayer1;
    public GameObject BulletPrefab;
    private Stack<GameObject> Bullets;
    void Start()
    {
        clearBullet();
        for(int i=0; i<numberOfBullet; i++)
        {
            createBullet();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void clearBullet()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        Bullets = new Stack<GameObject>();
    }
    void createBullet()
    {
        GameObject Bullet = Instantiate(BulletPrefab);
        Bullet.transform.SetParent(transform);
        Vector3 BulletScale = Bullet.transform.localScale;
        Bullet.transform.localScale = new Vector3(Mathf.Abs(BulletScale.x), Mathf.Abs(BulletScale.y), Mathf.Abs(BulletScale.z));
        Bullets.Push(Bullet);
    }

    public void updateBullet(Component sender, object data)
    {
        if (sender is PlayerShootController && sender.gameObject.GetComponent<PlayerController>().isPlayer1 == isPlayer1)
        {
            if(data is int)
            {
                int BulletAfterChanged = (int) data;

                if(numberOfBullet > BulletAfterChanged)
                {
                    for(int i=numberOfBullet; i>BulletAfterChanged; i--){
                        GameObject BulletToRemove = Bullets.Pop();
                        Destroy(BulletToRemove);
                    }
                }
                else if (numberOfBullet < BulletAfterChanged)
                {
                    for(int i=numberOfBullet; i<BulletAfterChanged; i++)
                    {
                        createBullet();
                    }
                }

                numberOfBullet = BulletAfterChanged;
                //temp
                if (isPlayer1)
                {
                    Debug.Log("Player 1's Bullet : " + numberOfBullet);
                }
                else
                {
                    Debug.Log("Player 2's Bullet : " + numberOfBullet);
                }
            }
        }
    }
}