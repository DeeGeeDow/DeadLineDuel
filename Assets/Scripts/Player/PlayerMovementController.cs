using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Vector2 movementInput = Vector2.zero;
    [SerializeField] private float movementSpeed = 5.0f;
    private List<float> movementBound;
    private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        movementBound = gameObject.GetComponent<PlayerController>().getBoundaries();
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        parent.position += new Vector3(movementInput.x, 0, movementInput.y).normalized*movementSpeed*Time.deltaTime;
        float z_pos = (transform.position.z > movementBound[0]) ? movementBound[0] : (transform.position.z < movementBound[1]) ? movementBound[1] : transform.position.z;
        float x_pos = (transform.position.x > movementBound[2]) ? movementBound[2] : (transform.position.x < movementBound[3]) ? movementBound[3] : transform.position.x;
        parent.position = new Vector3(x_pos, 0, z_pos);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void setHalfSpeed()
    {
        movementSpeed /= 2;
    }

    public void setDoubleSpeed()
    {
        movementSpeed *= 2;
    }
}
