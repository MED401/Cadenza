using Event_System;
using Interactions;
using LevelSystem;
using SoundMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static Transform _playerPickupContainer;

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
            _playerPickupContainer = camera.transform.GetChild(1);
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
            Interactable newTarget = null;
            if (Physics.Raycast(
                camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane)),
                camera.transform.forward, out var hit, interactDistance))
            {
                if (!hit.transform.GetComponent<Interactable>()) return;

                newTarget = hit.transform.GetComponent<Interactable>();

                if (target != null && target != newTarget)
                {
                    GameEvents.Current.RemoveTarget(target.GetInstanceID());
                }

                target = newTarget;
                GameEvents.Current.TakeTarget(target.GetInstanceID());
                UpdateInfoText(target);
            }
            else
            {
                if (target != null) GameEvents.Current.RemoveTarget(target.GetInstanceID());
                target = null;
                UpdateInfoText(target);
            }
        }

        private void UpdateInfoText(Interactable target)
        {
            useInfo.text = "";
            if (target is Pickup) useInfo.text = "Pick Up";
            if (target is IButton) useInfo.text = "Activate";
            if (target is SoundObjectPlatform) useInfo.text = "Activate";
            if (target is SoundObjectPlatform & _playerPickupContainer.childCount > 0) useInfo.text = "Place";
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
                GameEvents.Current.Place(camera.GetComponentInChildren<Pickup>().GetInstanceID(),
                    target as SoundObjectPlatform);
            else if (camera.GetComponentInChildren<Pickup>())
                GameEvents.Current.Drop(camera.GetComponentInChildren<Pickup>().GetInstanceID());

            else if (target != null) GameEvents.Current.Interact(target.GetInstanceID());
        }
    }
}