using Interactions;
using LevelComponents.SolutionElements;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static Transform PlayerPickupContainer;

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
        private bool isJumping;
        private InputMaster playerInput;

        private Interactable target;
        private Text useInfo;

        private void Awake()
        {
            playerInput = new InputMaster();
            controller = GetComponent<CharacterController>();
            camera = GetComponentInChildren<Camera>();
            PlayerPickupContainer = camera.transform.GetChild(1);
            useInfo = camera.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();

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
            if (Physics.Raycast(
                camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane)),
                camera.transform.forward, out var hit, interactDistance))
            {
                if (hit.transform.GetComponent<Interactable>())
                {
                    var newTarget = hit.transform.GetComponent<Interactable>();
                    if (target != null && target != newTarget) target.RemoveTarget();
                    target = newTarget;
                    target.Target();
                }
                else
                {
                    if (target != null) target.RemoveTarget();
                    target = null;
                }
            }
            else
            {
                if (target != null) target.RemoveTarget();
                target = null;
            }
            
            UpdateInfoText();
        }

        private void UpdateInfoText()
        {
            if (target is SoundObjectPlatform && PlayerPickupContainer.childCount > 0)
            {
                useInfo.text = "Place";
            }
            else if (target)
            {
                useInfo.text = target.UseInfo;
            }
            else
            {
                useInfo.text = "";
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
                if (camera.GetComponentInChildren<Pickup>() & target is SoundObjectPlatform)
                    PlayerPickupContainer.GetComponentInChildren<Pickup>().Place(target as SoundObjectPlatform);
                else if (PlayerPickupContainer.childCount > 0)
                    PlayerPickupContainer.GetComponentInChildren<Pickup>().Drop();

                else if (target != null) target.Interact();
            }
        }
    }