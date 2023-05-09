using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer1;
    private Camera mainCamera;
    private List<float> boundary = new List<float>(4);

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
        transform.parent.position += newPos;
        return this;
    }

    public PlayerController setIsPlayer1(bool isPlayer1)
    {
        this.isPlayer1 = isPlayer1;
        return this;
    }

    public PlayerController setBoundaries(float up, float down, float right, float left)
    {
        boundary[0] = up;
        boundary[1] = down;
        boundary[2] = right;
        boundary[3] = left;
        return this;
    }

    public PlayerController setBoundaries(List<float> bound)
    {
        boundary = bound;
        return this;
    }

    public List<float> getBoundaries()
    {
        return this.boundary;
    }
}
