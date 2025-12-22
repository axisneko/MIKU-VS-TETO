using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class playerMovement : MonoBehaviour
{
    public InputActionAsset InputActions;
    public GameObject CameraHolder;

    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_jumpAction;
    private InputAction m_pauseActionPlayer;
    private InputAction m_pauseActionUI;
    public TextMeshProUGUI VelocityField;
    public Slider MouseSensitivitySlider;

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;
    private Animator m_animator;
    private CharacterController m_characterController;

    public float WalkSpeed = 5;
    public float MouseSensitivity = 0.3f;
    public float JumpSpeed = 5;
    public float AirMultiplier = 0f;
    public float GroundDrag = 5;
    public float gravityForce = -30f;
    public float verticalVelocity = 0;

    public bool grounded;
    public LayerMask groundLayer;

    public GameObject PauseDisplay;
    private float xRotation = 0f;
    private float yRotation = 0f;
    void Start()
    { 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");
        m_jumpAction = InputSystem.actions.FindAction("Jump");

        m_pauseActionPlayer = InputSystem.actions.FindAction("Player/Pause");
        m_pauseActionUI = InputSystem.actions.FindAction("UI/Pause");

        m_animator = GetComponent<Animator>();
        m_characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_lookAmt = m_lookAction.ReadValue<Vector2>();

        grounded = Physics.CheckSphere(transform.position + new Vector3(0, 0.26f, 0), 0.34f, groundLayer);
        //grounded = m_characterController.isGrounded;

        if (m_jumpAction.WasPressedThisFrame())
        {
            Jump();
        }

        MouseSensitivity = MouseSensitivitySlider.value;
        //SpeedControl();
        DoGravity();
        Rotating();
        DisplayPause();
    }

    private void FixedUpdate()
    {
        Walking();
    }

    private void DisplayPause()
    {
        if (m_pauseActionPlayer.WasPressedThisFrame())
        {
            ChangePauseMenuState(true);
        }
        else if (m_pauseActionUI.WasPressedThisFrame())
        {
            ChangePauseMenuState(false);
        }
    }

    public void ChangePauseMenuState(bool isOpening)
    {
        if (isOpening)
        {
            PauseDisplay.SetActive(true);
            InputActions.FindActionMap("Player").Disable();
            InputActions.FindActionMap("UI").Enable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!isOpening)
        {
            PauseDisplay.SetActive(false);
            InputActions.FindActionMap("Player").Enable();
            InputActions.FindActionMap("UI").Disable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Jump()
    {
        if (grounded) {
            verticalVelocity = JumpSpeed;
            //m_animator.SetTrigger("Jump");
        }
    }

    private void Walking()
    {
        //m_animator.SetFloat("Speed", m_moveAmt.y);
        var moveDirection = transform.forward * m_moveAmt.y + transform.right * m_moveAmt.x;
        moveDirection = moveDirection * WalkSpeed * Time.deltaTime;
        m_characterController.Move(moveDirection);
    }

    //private void SpeedControl()
    //{
    //    Vector3 flatVel = new Vector3(m_rigidbody.linearVelocity.x, 0f, m_rigidbody.linearVelocity.z);

    //    if (flatVel.magnitude > WalkSpeed)
    //    {
    //        Vector3 limitedVel = flatVel.normalized * WalkSpeed;
    //        m_rigidbody.linearVelocity = new Vector3(limitedVel.x, m_rigidbody.linearVelocity.y, limitedVel.z);
    //    }
    //    VelocityField.text = m_rigidbody.linearVelocity.magnitude.ToString();
    //}

    private void Rotating()
    {
        yRotation += m_lookAmt.x * MouseSensitivity * Time.deltaTime * 50;
        xRotation -= m_lookAmt.y * MouseSensitivity * Time.deltaTime * 50;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        CameraHolder.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void DoGravity()
    {
        verticalVelocity += gravityForce * Time.deltaTime;
        m_characterController.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
    }
}