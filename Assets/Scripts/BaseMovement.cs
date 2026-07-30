using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rigidbody2D = UnityEngine.Rigidbody2D;

public class BaseMovement : MonoBehaviour
{
    public float moveX = 0f;
    public float moveY = 0f;
    private float moveSpeed = 6.25f;
    void Update()
    {
        Move();
        
    }
    public void Move()
    {
        
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, moveY, 0f).normalized;
        GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
    }
}
