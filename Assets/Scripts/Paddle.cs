using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    //refrences
    public Rigidbody2D rb;
    private PlayerInput controls;


    //paddle settings
    public bool right;
    public float rotateSpeed;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void use()
    {
        rb.MoveRotation(right ? -25f : 25f);
    }
}
