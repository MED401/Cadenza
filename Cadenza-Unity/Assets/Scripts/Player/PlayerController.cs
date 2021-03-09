using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputMaster playerInput;
    [SerializeField] [Range(0.0f, 1.0f)] private float mouseSensitivity = 0.1f;
    [SerializeField] [Range(0.0f, 10.0f)] private float movementSpeed = 6.0f;
    [SerializeField] private float stickToGroundForce = 10;
    [SerializeField] [Range(0.0f, 20.0f)] private float jumpStrength = 10.0f;
    [SerializeField] [Range(0.0f, 5.0f)] private float gravityMultiplier = 2.0f;
    [SerializeField] [Range(0.0f, 5.0f)] private float interactDistance = 2.0f;
    [SerializeField] private bool lockCursor = true;

    private CharacterController controller;
    private new Camera camera;
    private Vector3 currentDirection = Vector3.zero;
    private Transform currentTarget;
    private string selectableTag = "Interactable";
    private bool isJumping = false;
    private float cameraPitch = 0.0f;

    private void Awake()
    {
        playerInput = new InputMaster();
        controller = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();

        playerInput.OnFoot.Interact.performed += _ => Interact();
        playerInput.OnFoot.Jump.performed += _ => Jump();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        UpdateMouseLook();
        UpdateTarget();
        controller.Move(UpdateMovement() * Time.deltaTime);
    }

    private void UpdateMouseLook()
    {
        Vector2 mouseDelta = playerInput.OnFoot.CameraMovement.ReadValue<Vector2>();

        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        camera.transform.localEulerAngles = Vector3.right * cameraPitch;
        this.transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
    }

    private void UpdateTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane)), camera.transform.forward, out hit, interactDistance))
        {
            Transform newTarget = hit.transform;

            if (currentTarget != null && currentTarget != newTarget)
            {
                GameEvents.current.RemoveTarget(currentTarget.GetComponent<Interactable>().id);
            }

            if (newTarget.CompareTag(selectableTag))
            {
                currentTarget = newTarget;
                GameEvents.current.TakeTarget(currentTarget.GetComponent<Interactable>().id);
            }
            else
            {
                currentTarget = null;
            }
        }
        else
        {
            if (currentTarget != null)
            {
                GameEvents.current.RemoveTarget(currentTarget.GetComponent<Interactable>().id);
            }
        }
    }

    private Vector3 UpdateMovement()
    {
        float verticalMovement = playerInput.OnFoot.VerticalMovement.ReadValue<float>();
        float horizontalMovement = playerInput.OnFoot.HorizontalMovement.ReadValue<float>();

        Vector3 targetDirection = new Vector3(horizontalMovement, verticalMovement, 0.0f);
        targetDirection.Normalize();

        // always move along the camera forward as it is the direction that it being aimed at
        Vector3 desiredMove = transform.forward * targetDirection.y + transform.right * targetDirection.x;

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, this.controller.radius, Vector3.down, out hitInfo, this.controller.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        currentDirection.x = desiredMove.x * movementSpeed;
        currentDirection.z = desiredMove.z * movementSpeed;

        if (this.controller.isGrounded)
        {
            currentDirection.y = -stickToGroundForce;

            if (isJumping)
            {
                currentDirection.y = jumpStrength;
            }
            isJumping = false;
        }
        else
        {
            currentDirection += Physics.gravity * gravityMultiplier * Time.deltaTime;
        }

        return currentDirection;
    }

    private void Jump()
    {
        isJumping = true;
    }
    private void Interact()
    {
        if (currentTarget != null)
        {
            GameEvents.current.Interact(currentTarget.GetComponent<Interactable>().id);
        }
    }

}
