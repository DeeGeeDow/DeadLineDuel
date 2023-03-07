using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private bool isPlayer1;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.eulerAngles = mainCamera.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime * ((isPlayer1) ? 1 : -1);
        if (Mathf.Abs(transform.position.x) > 25) Destroy(gameObject);
    }

    public BulletController setIsPlayer1(bool isPlayer1)
    {
        this.isPlayer1 = isPlayer1;
        return this;
    }

    public bool getIsPlayer1()
    {
        return this.isPlayer1;
    }

    public BulletController setPosition(Vector3 newPos)
    {
        this.transform.position = newPos;
        return this;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.gameObject.GetComponent<PlayerController>().isPlayer1 != this.isPlayer1)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
