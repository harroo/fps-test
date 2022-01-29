
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonPlayerController : MonoBehaviour {

    [Header("Control")]
    public bool returnTogglesControl = false;

    [Header("Movemenmt")]
    public float walkSpeed = 4.0f;
    public float runSpeed = 6.0f;
    public float jumpHeight = 1.4f;
    public float gravity = 32.0f;
    public bool runInAllDirections = false;

    private Vector3 moveDirection, moveCache;
    private CharacterController controller;

    [Header("Looking")]
    public float sensitivity = 6.0f;
    public Transform head;

    private float pitch, yaw;

    [Header("Camera Bobbing")]
    public bool useBobbing = false;
    public float bobSpeed = 14.0f;
    public float intensity = 0.05f;

    private float defaultPosY = 0;
    private float bobTimer = 0;

    [Header("Camera FOV")]
    public bool useZoom = false;
    public bool useLeaning = false;
    public float leanAmount = 8;

    private float defaultFOV, viewFieldCache;
    private Camera headCam;

    private void Start () {

        controller = GetComponent<CharacterController>();

        defaultPosY = head.localPosition.y;

        headCam = head.GetComponent<Camera>();
        defaultFOV = headCam.fieldOfView;
    }

    private void Update () {

        UpdateControl();

        UpdateMovement();
        UpdateLooking();

        UpdateCameraBobbing();

        UpdateCameraFOV();

        UpdateDoulbeTapRun();
    }

    private void UpdateControl () {

        if (!returnTogglesControl) return;

        if (Input.GetKeyDown(KeyCode.Return)) {

            Cursor.visible = !Cursor.visible;

            if (!Cursor.visible) {

                Cursor.lockState = CursorLockMode.Locked;
                
            } else {

                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void UpdateMovement () {

        controller.stepOffset = controller.isGrounded ? 0.5f : 0.3f;

        if (!Cursor.visible) {

            if (controller.isGrounded) {

                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= moveSpeed;

                if (Input.GetKey(KeyCode.Space))
                    moveDirection.y = Mathf.Sqrt(jumpHeight * 2.0f * gravity);

            } else {

                moveCache = new Vector3(moveSpeed, moveDirection.y, moveSpeed);
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, 0.25f * Time.deltaTime);
                moveDirection.y = moveCache.y;

                moveDirection = transform.InverseTransformDirection(moveDirection);

                moveDirection += new Vector3(
                    Input.GetAxis("Horizontal") * 20 * Time.deltaTime, 0,
                    Input.GetAxis("Vertical") * 20 * Time.deltaTime
                );

                moveDirection.x = Mathf.Clamp(moveDirection.x, -moveSpeed, moveSpeed);
                moveDirection.z = Mathf.Clamp(moveDirection.z, -moveSpeed, moveSpeed);

                moveDirection = transform.TransformDirection(moveDirection);

                if (Physics.Raycast(transform.position, Vector3.up, 0.82f))
                    moveDirection.y = -0.1f;
            }

        } else {

            moveDirection.x = 0;
            moveDirection.z = 0;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void UpdateLooking () {

        if (Cursor.visible) return;

        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch -= sensitivity * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);

        head.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0);
        transform.localEulerAngles = new Vector3(0, yaw, 0);
    }

    private void UpdateCameraBobbing () {

        if (useBobbing && isMoving) {

            bobTimer += Time.deltaTime * bobSpeed;

            head.localPosition = new Vector3(
                head.localPosition.x,
                defaultPosY + Mathf.Sin(bobTimer) * intensity,
                head.localPosition.z
            );

        } else {

            bobTimer = 0;
            head.localPosition = new Vector3(head.localPosition.x, Mathf.Lerp(
                head.localPosition.y, defaultPosY, Time.deltaTime * bobSpeed
            ), head.localPosition.z);
        }
    }

    private void UpdateCameraFOV () {

        if (useZoom && isZooming) {

            headCam.fieldOfView = 16.0f;

        } else if (useLeaning && running) {

            viewFieldCache += Time.deltaTime * 64;
            viewFieldCache = viewFieldCache > leanAmount ? leanAmount : viewFieldCache;
            headCam.fieldOfView = defaultFOV + viewFieldCache;

        } else if (useLeaning) {

            viewFieldCache -= Time.deltaTime * 82;
            viewFieldCache = viewFieldCache < 0 ? 0 : viewFieldCache;
            headCam.fieldOfView = defaultFOV + viewFieldCache;

        } else {

            headCam.fieldOfView = defaultFOV;
        }
    }

    private float doubleTapTimer;
    private bool doubleTapRun;

    private void UpdateDoulbeTapRun () {

        if (Input.GetKeyDown(KeyCode.W)) {

            if (doubleTapTimer > 0) doubleTapRun = true;
            else doubleTapTimer = 0.64f;
        }

        if (Input.GetKeyUp(KeyCode.W)) doubleTapRun = false;

        doubleTapTimer -= Time.deltaTime;
    }

    private float moveSpeed => running ? runSpeed : movingDiagonal ? walkSpeed / 1.32f : walkSpeed;
    private bool running => runCommanded;
    private bool isMoving => Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f;
    private bool isZooming => Input.GetMouseButton(2) || Input.GetKey(KeyCode.C);

    private bool runCommanded
        => runInAllDirections
            ? //if run in all directions then just check if any running flags are on
                Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.R) || doubleTapRun && Input.GetKey(KeyCode.W)
            : //else check running flags + not going diagonal at all
                ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.R)) && Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) ||
                (doubleTapRun && Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)));

    private bool movingDiagonal
        => Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D);
}
