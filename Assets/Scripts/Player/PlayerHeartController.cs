using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartController : MonoBehaviour
{
    [SerializeField] private int heart = 3;
    public GameEvent onPlayerHealthChanged;
    public AudioSource audioSource;
    public AudioClip hitSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Bullet")
        {
            if (collider.gameObject.GetComponent<BulletController>().getIsPlayer1() != gameObject.GetComponent<PlayerController>().isPlayer1)
            {
                audioSource.PlayOneShot(hitSFX, 1);
                heart--;
                onPlayerHealthChanged.Raise(this, heart);
            }
        }
        if (heart <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
