using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer1;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.eulerAngles = mainCamera.transform.eulerAngles;
    }

    public PlayerController setPosition(Vector3 newPos)
    {
        this.transform.position += newPos;
        return this;
    }

    public PlayerController setIsPlayer1(bool isPlayer1)
    {
        this.isPlayer1 = isPlayer1;
        return this;
    }
}
