using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    #region Componenets
    private InputController InputController;
    private InputAction movement;
    private Transform cameraTransform;
    #endregion

    #region Camera movement values
    [SerializeField] private float maxSpeed = 10f;
    private float speed;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float damping = 15f;
    #endregion

    #region Camera Position Update
    private Vector3 targetPosition;
    #endregion



    #region Tracking without RigidBody
    private Vector3 horizontalVelocity;
    private Vector3 lastPosition;
    //private Transform lastPosition;
    #endregion

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        InputController = new InputController();
        cameraTransform = this.GetComponentInChildren<Camera>().transform;

    }
    private void Start()
    {
        //InputController = GetComponent<InputController>();
    }

    private void OnEnable()
    {
        lastPosition = this.transform.position;
        movement = InputController.Gameplay.Movement;
        InputController.Gameplay.Enable();
    }
    private void OnDisable()
    {
        InputController.Gameplay.Disable();

    }

    private void Update()
    {
        GetKeyboardMovement();
        UpdateVelocity();
        UpdateBasePosition();
    }
    private void UpdateVelocity()
    {
        horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
        horizontalVelocity.y = 0;
        lastPosition = this.transform.position;
    }
    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight()
            + movement.ReadValue<Vector2>().y * GetCameraForward();
        inputValue = inputValue.normalized;
        if(inputValue.sqrMagnitude > 0.1f)
        {
            targetPosition += inputValue;
        }
    }


    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0;
        return right;
    }

    private Vector3 GetCameraForward()
    {
        Vector3 up = cameraTransform.up;
        up.x = 0;
        return Vector3.up;
    }
    private void UpdateBasePosition()
    {
        if(targetPosition.sqrMagnitude > 0.1f)
        {
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * speed * Time.deltaTime;
        }
        else
        {
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }

        targetPosition = Vector3.zero;
    }    
}
