using Event_System;
using Interactions;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] [Range(0.0f, 1.0f)] private float mouseSensitivity = 0.1f;
        [SerializeField] [Range(0.0f, 10.0f)] private float movementSpeed = 6.0f;
        [SerializeField] private float stickToGroundForce = 10;
        [SerializeField] [Range(0.0f, 20.0f)] private float jumpStrength = 10.0f;
        [SerializeField] [Range(0.0f, 5.0f)] private float gravityMultiplier = 2.0f;
        [SerializeField] [Range(0.0f, 5.0f)] private float interactDistance = 2.0f;
        [SerializeField] private bool lockCursor = true;
        private new Camera camera;
        private float cameraPitch;
        
        private CharacterController controller;
        private Vector3 currentDirection = Vector3.zero;
        private int currentTargetId;
        private bool isJumping;
        private InputMaster playerInput;

        private void Awake()
        {
            playerInput = new InputMaster();
            controller = GetComponent<CharacterController>();
            camera = GetComponentInChildren<Camera>();

            playerInput.OnFoot.Interact.performed += _ => Interact();
            playerInput.OnFoot.Jump.performed += _ => Jump();
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

        private void OnEnable()
        {
            playerInput.Enable();
        }

        private void OnDisable()
        {
            playerInput.Disable();
        }

        private void UpdateMouseLook()
        {
            var mouseDelta = playerInput.OnFoot.CameraMovement.ReadValue<Vector2>();

            cameraPitch -= mouseDelta.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

            camera.transform.localEulerAngles = Vector3.right * cameraPitch;
            transform.Rotate(Vector3.up * (mouseDelta.x * mouseSensitivity));
        }

        private void UpdateTarget()
        {
            var newTargetId = 0;
            if (Physics.Raycast(
                camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane)),
                camera.transform.forward, out var hit, interactDistance))
            {
                if (!hit.transform.GetComponent<Interactable>()) return;

                newTargetId = hit.transform.GetComponent<Interactable>().GetId();

                if (currentTargetId != 0 && currentTargetId != newTargetId)
                    GameEvents.Current.RemoveTarget(currentTargetId);

                currentTargetId = newTargetId;
                GameEvents.Current.TakeTarget(currentTargetId);
            }
            else
            {
                if (currentTargetId != 0) GameEvents.Current.RemoveTarget(currentTargetId);
                currentTargetId = 0;
            }
        }

        private Vector3 UpdateMovement()
        {
            var verticalMovement = playerInput.OnFoot.VerticalMovement.ReadValue<float>();
            var horizontalMovement = playerInput.OnFoot.HorizontalMovement.ReadValue<float>();

            var targetDirection = new Vector3(horizontalMovement, verticalMovement, 0.0f);
            targetDirection.Normalize();

            // always move along the camera forward as it is the direction that it being aimed at
            var transform1 = transform;
            var desiredMove = transform1.forward * targetDirection.y + transform1.right * targetDirection.x;

            // get a normal for the surface that is being touched to move along it
            Physics.SphereCast(transform1.position, controller.radius, Vector3.down, out var hitInfo,
                controller.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            currentDirection.x = desiredMove.x * movementSpeed;
            currentDirection.z = desiredMove.z * movementSpeed;

            if (controller.isGrounded)
            {
                currentDirection.y = -stickToGroundForce;

                if (isJumping) currentDirection.y = jumpStrength;
                isJumping = false;
            }
            else
            {
                currentDirection += Physics.gravity * (gravityMultiplier * Time.deltaTime);
            }

            return currentDirection;
        }

        private void Jump()
        {
            isJumping = true;
        }

        private void Interact()
        {
            if (camera.GetComponentInChildren<Pickup>())
            {
                GameEvents.Current.Drop(camera.GetComponentInChildren<Pickup>().GetId());
            }

            else if (currentTargetId != 0)
            {
                GameEvents.Current.Interact(currentTargetId);
            }
        }
    }
}