using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameEvent onPlayerBulletChanged;
    public Animator animator;
    public AudioClip shootSFX;
    public AudioClip ReloadSFX;
    [SerializeField] private float shootCooldown = 1;
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private int bulletCount = 5;
    [SerializeField] private int maxBullet = 5;
    private AudioSource audioSource;
    private bool isPlayer1;
    private bool shootButtonTriggered;
    private bool reloadButtonTriggered;
    private bool isReloading = false;
    private bool shootAvailable = true;
    private float delayAttack = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        this.isPlayer1 = GetComponent<PlayerController>().isPlayer1;
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadButtonTriggered) reload();
        Shoot();
    }

    private void OnEnable()
    {
        shootAvailable = false;
        StartCoroutine(StartCooldown(shootCooldown, () => { shootAvailable = true; }));
    }

    public void onShoot(InputAction.CallbackContext context)
    {
        shootButtonTriggered = context.action.triggered;
    }

    public void onReload(InputAction.CallbackContext context)
    {
        reloadButtonTriggered = context.action.triggered;
    }

    private void Shoot()
    {
        if (!shootButtonTriggered || !shootAvailable || bulletCount <= 0 || isReloading)
        {
            // notify SoundController nyetel sfx peluru abis
            return;
        }
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(shootSFX);
        shootAvailable = false;
        bulletCount--;
        onPlayerBulletChanged.Raise(this, bulletCount);
        StartCoroutine(StartCooldown(delayAttack, ShootAfterAnim));
        StartCoroutine(StartCooldown(shootCooldown, () => { shootAvailable = true; }));
    }

    public void ShootAfterAnim()
    {
        var newPos = transform.position + new Vector3(1.1f, 0, 0) * ((this.isPlayer1) ? 1 : -1);
        ShootAt(newPos);
    }

    public void ShootAt(Vector3 position)
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<BulletController>()
            .setIsPlayer1(this.isPlayer1)
            .setPosition(position);
    }

    public void reload()
    {
        audioSource.PlayOneShot(ReloadSFX);
        if (bulletCount == maxBullet || isReloading)
        {
            return;
        }
        isReloading = true;
        StartCoroutine(StartCooldown(reloadTime, onFinishReload));
    }
    public IEnumerator StartCooldown(float duration, System.Action operation)
    {
        yield return new WaitForSeconds(duration);
        operation();
    }
    public void onFinishReload()
    {
        bulletCount = maxBullet;
        isReloading = false;
        onPlayerBulletChanged.Raise(this, bulletCount);
        Debug.Log("Reload finished, bullet count : " + bulletCount);
    }
}
