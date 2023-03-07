using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameEvent onPlayerBulletChanged;
    [SerializeField] private float shootCooldown = 1;
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private int bulletCount = 5;
    [SerializeField] private int maxBullet = 5;
    private bool isPlayer1;
    private bool shootButtonTriggered;
    private bool reloadButtonTriggered;
    private bool isReloading = false;
    private bool shootAvailable = true;

    // Start is called before the first frame update
    void Start()
    {
        this.isPlayer1 = gameObject.GetComponent<PlayerController>().isPlayer1;
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadButtonTriggered) reload();
        shoot();
    }

    public void onShoot(InputAction.CallbackContext context)
    {
        shootButtonTriggered = context.action.triggered;
    }

    public void onReload(InputAction.CallbackContext context)
    {
        reloadButtonTriggered = context.action.triggered;
    }

    public void shoot()
    {
        if(!shootButtonTriggered || !shootAvailable || bulletCount<=0 || isReloading)
        {
            // notify SoundController nyetel sfx peluru abis
            return;
        }
        var newPos = transform.position + new Vector3(1.1f, 0, 0) * ((this.isPlayer1) ? 1 : -1);
        var bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<BulletController>()
            .setIsPlayer1(this.isPlayer1)
            .setPosition(newPos);
                
        shootAvailable = false;
        bulletCount--;
        onPlayerBulletChanged.Raise(this, bulletCount);
        StartCoroutine(StartCooldown(shootCooldown, () => { shootAvailable = true; }));
    }
    public void reload()
    {
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
