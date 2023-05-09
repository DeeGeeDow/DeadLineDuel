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
    public Animator animator;
    private bool immune = false;

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
        }else if(collider.gameObject.tag == "Laser")
        {
            if(collider.transform.parent.parent.GetComponentInChildren<PlayerController>().isPlayer1 != GetComponent<PlayerController>().isPlayer1) hit();
        }
        if (heart <= 0)
        {
            Die();
            //Destroy(this.gameObject);
        }
    }

    private void Die()
    {
        gameObject.GetComponent<PlayerShootController>().enabled = false;
        gameObject.GetComponent<PlayerMovementController>().enabled = false;
        //gameObject.GetComponent<PlayerSkillController>().enabled = false;
        animator.SetTrigger("Die");
    }
    private void hit()
    {
        heart--;
        audioSource.PlayOneShot(HitSFX);
        onPlayerHealthChanged.Raise(this, heart);
        StartCoroutine(Immune());
        StartCoroutine(TinungTinung());
    }

    private IEnumerator TinungTinung()
    {
        //GetComponent<Collider>().enabled = false;
        float timer = 0f;
        Debug.Log("Tinung Tinung");
        while(immune || timer<0.01f)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            timer += Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
            //Debug.Log(timer);
        }
        //GetComponent<SpriteRenderer>().enabled = true;
        //GetComponent<Collider>().enabled = true;
    }

    private IEnumerator Immune()
    {
        this.immune = true;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(this.tinungTinungDuration);
        this.immune = false;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
