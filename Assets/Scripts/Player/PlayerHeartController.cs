using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartController : MonoBehaviour
{
    [SerializeField] private int heart = 3;
    public GameEvent onPlayerHealthChanged;
    public AudioClip HitSFX;
    public float tinungTinungDuration = 1f;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Bullet")
        {
            if (collider.gameObject.GetComponent<BulletController>().getIsPlayer1() != gameObject.GetComponent<PlayerController>().isPlayer1)
            {
                hit();
            }
        }
        if (heart <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void hit()
    {
        heart--;
        audioSource.PlayOneShot(HitSFX);
        onPlayerHealthChanged.Raise(this, heart);
        StartCoroutine(TinungTinung());
    }

    private IEnumerator TinungTinung()
    {
        GetComponent<Collider>().enabled = false;
        float timer = 0f;
        Debug.Log("Tinung Tinung");
        while(timer < tinungTinungDuration)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            timer += Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
