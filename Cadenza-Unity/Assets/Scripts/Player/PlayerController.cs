using System.Collections;
using Interactions;
using LevelComponents.SolutionElements;
using Menus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float mouseSensitivity;

        private static Transform _playerPickupContainer;

        [SerializeField] private GameObject playMenu;
        [SerializeField] [Range(0.0f, 10.0f)] private float movementSpeed = 6.0f;
        [SerializeField] private float stickToGroundForce = 10;
        [SerializeField] [Range(0.0f, 20.0f)] private float jumpStrength = 10.0f;
        [SerializeField] [Range(0.0f, 5.0f)] private float gravityMultiplier = 2.0f;
        [SerializeField] [Range(0.0f, 5.0f)] private float interactDistance = 2.0f;
        [SerializeField] private bool lockCursor = true;
        [SerializeField] private CanvasRenderer textRenderer;

        private Camera _camera;
        private float _cameraPitch;
        private CharacterController _characterController;
        private Vector3 _currentDirection = Vector3.zero;
        private bool _inMenu, _isJumping;
        private InputMaster _playerInput;
        private Interactable _targetInteractable;
        private Text _targetUseHintText;
        private Image _textBackground;

        public bool IsDead { get; set; }
        private Vector3 SpawnPoint { get; set; }

        private void Awake()
        {
            mouseSensitivity = CurrentAudioSettings.MouseSensitivity;
            _characterController = GetComponent<CharacterController>();
            _camera = GetComponentInChildren<Camera>();
            _playerPickupContainer = _camera.transform.GetChild(1);
            _targetUseHintText = _camera.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
            _playerInput = new InputMaster();
            _textBackground = textRenderer.GetComponent<Image>();

            _playerInput.OnFoot.Interact.performed += _ => Interact();
            _playerInput.OnFoot.Jump.performed += _ => Jump();
            _playerInput.OnFoot.OpenMenu.performed += _ => ToggleMenu();

            SpawnPoint = transform.position;
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
            if (IsDead)
            {
                StartCoroutine(Respawn());
            }
            else if (!_inMenu)
            {
                UpdateMouseLook();
                UpdateTarget();
                _characterController.Move(UpdateMovement() * Time.deltaTime);
            }
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public void ToggleMenu()
        {
            _inMenu = !_inMenu;
            playMenu.SetActive(_inMenu);

            Cursor.lockState = _inMenu ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = _inMenu;
        }

        private void SkipScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private IEnumerator Respawn()
        {
            transform.position = SpawnPoint;

            var i = 0;

            while (i < 3)
            {
                i++;
                yield return new WaitForEndOfFrame();
            }

            IsDead = false;
        }

        private void UpdateMouseLook()
        {
            var mouseDelta = _playerInput.OnFoot.CameraMovement.ReadValue<Vector2>();

            _cameraPitch -= mouseDelta.y * mouseSensitivity;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -90.0f, 90.0f);

            _camera.transform.localEulerAngles = Vector3.right * _cameraPitch;
            transform.Rotate(Vector3.up * (mouseDelta.x * mouseSensitivity));
        }

        private void UpdateTarget()
        {
            if (Physics.Raycast(
                _camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, _camera.nearClipPlane)),
                _camera.transform.forward, out var hit, interactDistance))
            {
                if (hit.transform.GetComponent<Interactable>())
                {
                    var newTarget = hit.transform.GetComponent<Interactable>();
                    if (_targetInteractable != null && _targetInteractable != newTarget)
                        _targetInteractable.RemoveTarget();
                    _targetInteractable = newTarget;
                    _targetInteractable.Target();
                }
                else
                {
                    if (_targetInteractable != null) _targetInteractable.RemoveTarget();
                    _targetInteractable = null;
                }
            }
            else
            {
                if (_targetInteractable != null) _targetInteractable.RemoveTarget();
                _targetInteractable = null;
            }

            UpdateInfoText();
        }

        private void UpdateInfoText()
        {
            textRenderer.gameObject.SetActive(false);
            if (_targetInteractable is SoundObjectPlatform && _playerPickupContainer.childCount > 0)
            {
                textRenderer.gameObject.SetActive(true);
                _targetUseHintText.text = "Place";
                _textBackground.rectTransform.sizeDelta = new Vector2(72, 30);
            }
            else if (_targetInteractable)
            {
                textRenderer.gameObject.SetActive(true);
                _targetUseHintText.text = _targetInteractable.UseInfo;
                _textBackground.rectTransform.sizeDelta = new Vector2(_targetUseHintText.text.Length * 13, 30);
            }
            else
            {
                _targetUseHintText.text = "";
            }
        }

        private Vector3 UpdateMovement()
        {
            var verticalMovement = _playerInput.OnFoot.VerticalMovement.ReadValue<float>();
            var horizontalMovement = _playerInput.OnFoot.HorizontalMovement.ReadValue<float>();

            var targetDirection = new Vector3(horizontalMovement, verticalMovement, 0.0f);
            targetDirection.Normalize();

            // Always move along the camera forward as it is the direction that it being aimed at
            var transform1 = transform;
            var desiredMove = transform1.forward * targetDirection.y + transform1.right * targetDirection.x;

            // Get a normal for the surface that is being touched to move along it
            Physics.SphereCast(transform1.position, _characterController.radius, Vector3.down, out var hitInfo,
                _characterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            _currentDirection.x = desiredMove.x * movementSpeed;
            _currentDirection.z = desiredMove.z * movementSpeed;

            if (_characterController.isGrounded)
            {
                _currentDirection.y = -stickToGroundForce;

                if (_isJumping) _currentDirection.y = jumpStrength;
                _isJumping = false;
            }
            else
            {
                _currentDirection += Physics.gravity * (gravityMultiplier * Time.deltaTime);
            }

            return _currentDirection;
        }

        private void Jump()
        {
            _isJumping = true;
        }

        private void Interact()
        {
            if (_camera.GetComponentInChildren<Pickup>() & _targetInteractable is SoundObjectPlatform)
                _playerPickupContainer.GetComponentInChildren<Pickup>()
                    .Place(_targetInteractable as SoundObjectPlatform);
            else if (_playerPickupContainer.childCount > 0)
                _playerPickupContainer.GetComponentInChildren<Pickup>().Drop();

            else if (_targetInteractable != null) _targetInteractable.Interact();
        }
    }
}