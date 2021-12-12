using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    PlayerController playerControls;
    Rigidbody shipRb; 
    [SerializeField] float speed;
    [SerializeField] float angle_x;
    [SerializeField] float angle_y;
    [SerializeField] float rotationSpeed;
    float boostInput;
    Vector2 move;

    void Awake()
    {
        playerControls = new PlayerController();
        playerControls.Gameplay.Boost.performed += onBoost;
        playerControls.Gameplay.Boost.canceled += onBoost;
        playerControls.Gameplay.Move.performed += ctx => move = (ctx.ReadValue<Vector2>());
        playerControls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    void onBoost(InputAction.CallbackContext value) 
    {
        boostInput = value.ReadValue<float>();
    
    }
    private void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable(); 
    }


    void Start()
    {
        shipRb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shipRb.velocity = transform.forward * speed * (Mathf.Max(boostInput, .2f));
        turnShip();
    }
    void turnShip() 
    {
        transform.Rotate(Vector3.up, move.x * rotationSpeed * Time.fixedDeltaTime);
        turnShipVisual();
    }
    void turnShipVisual() 
    {
        transform.localEulerAngles = new Vector3(move.y * angle_y, transform.localEulerAngles.y, -move.x * angle_x);
    }

}
