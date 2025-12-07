# Real-life-Game-
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3.5f;
    public float sprintSpeed = 6f;
    public float rotationSmooth = 10f;
    public float jumpForce = 6f;

    [Header("References")]
    public Rigidbody rb;
    public Transform cameraTransform;
    public Animator animator;
    public Transform aimPivot; // where weapon aims from (chest/shoulder)
    public WeaponController weaponController;
    public SkillSystem skillSystem;

    Vector3 inputDir;
    bool isGrounded = true;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (cameraTransform == null && Camera.main != null) cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        ReadInput();
        HandleAnimation();
        HandleAiming();
        if (Input.GetButtonDown("Fire1"))
        {
            weaponController?.TryFire();
        }

        if (Input.GetKeyDown(KeyCode.R))
            weaponController?.Reload();
        // Mobile buttons should call weaponController.TryFire() via UI button
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void ReadInput()
    {
        // Unity axes mapped to mobile virtual joystick if present
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        inputDir = new Vector3(h, 0f, v);
    }

    void MoveCharacter()
    {
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * inputDir.z + camRight * inputDir.x).normalized;
        bool sprint = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed = sprint ? sprintSpeed : walkSpeed;
        Vector3 velocity = move * targetSpeed;
        Vector3 vel = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        rb.velocity = vel;

        if (move.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSmooth * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (!isGrounded) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.contacts.Length > 0)
        {
            // crude ground check
            if (Vector3.Dot(col.contacts[0].normal, Vector3.up) > 0.5f)
                isGrounded = true;
        }
    }

    void HandleAnimation()
    {
        if (animator == null) return;
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        animator.SetFloat("Speed", speed);
    }

    void HandleAiming()
    {
        if (aimPivot == null) return;
        // If mouse present, aim with mouse; else use camera forward
        Vector3 aimPoint;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 forward = Camera.main.transform.forward;
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            aimPoint = hit.point;
        else
            aimPoint = transform.position + forward * 30f;

        Vector3 dir = (aimPoint - aimPivot.position).normalized;
        aimPivot.forward = Vector3.Lerp(aimPivot.forward, dir, 20f * Time.deltaTime);
    }
}
