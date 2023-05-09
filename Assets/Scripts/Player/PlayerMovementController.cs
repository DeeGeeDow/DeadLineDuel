using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Vector2 movementInput = Vector2.zero;
    [SerializeField] private float movementSpeed = 5.0f;
    private List<float> movementBound;
    // Start is called before the first frame update
    void Start()
    {
        movementBound = gameObject.GetComponent<PlayerController>().getBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(movementInput.x, 0, movementInput.y).normalized*movementSpeed*Time.deltaTime;
        float z_pos = (transform.position.z > movementBound[0]) ? movementBound[0] : (transform.position.z < movementBound[1]) ? movementBound[1] : transform.position.z;
        float x_pos = (transform.position.x > movementBound[2]) ? movementBound[2] : (transform.position.x < movementBound[3]) ? movementBound[3] : transform.position.x;
        transform.position = new Vector3(x_pos, 0, z_pos);
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
