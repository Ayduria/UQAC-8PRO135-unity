using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*public class ShipController : MonoBehaviour
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
        transform.Rotate(Vector3.up, move.x * rotationSpeed * Time.deltaTime);
        turnShipVisual();
    }
    void turnShipVisual() 
    {
        transform.localEulerAngles = new Vector3(move.y * angle_y, transform.localEulerAngles.y, -move.x * angle_x);
    } 

}*/


public class ShipController : MonoBehaviour 
{
    public float forwardSpeed =25f, strafeSpeed=7f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    private float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f; 

    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxis("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime , Space.Self);

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);
       

    }


}