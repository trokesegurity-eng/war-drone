using UnityEngine;

// MobileHybridController: combines touch joystick + buttons with autonomous steering for a hybrid mobile control.
// Attach to the drone GameObject with a Rigidbody and an AutonomousSteering component.
[RequireComponent(typeof(Rigidbody))]
public class MobileHybridController : MonoBehaviour
{
    public Rigidbody rb;
    public AutonomousSteering autonomous;
    [Range(0f,1f)] public float autonomousWeight = 1f; // 0 = player, 1 = AI
    public float maxSpeed = 6f;
    public float acceleration = 20f;
    public float manualInputTimeout = 1.0f; // seconds to auto-resume AI after manual input stops
    private float manualTimer = 0f;

    // Inputs (set by UI)
    Vector2 joystick = Vector2.zero; // x = strafe, y = forward
    bool ascend = false;
    bool descend = false;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (autonomous == null) autonomous = GetComponent<AutonomousSteering>();
    }

    void Update()
    {
        bool hasManualInput = joystick.sqrMagnitude > 0.0001f || ascend || descend;

        if (hasManualInput)
        {
            manualTimer = manualInputTimeout;
            autonomousWeight = Mathf.Lerp(autonomousWeight, 0f, 8f * Time.deltaTime);
        }
        else
        {
            if (manualTimer > 0f) manualTimer -= Time.deltaTime;
            else autonomousWeight = Mathf.Lerp(autonomousWeight, 1f, 1.5f * Time.deltaTime);
        }

        Vector3 playerDir = ConvertJoystickToWorld();
        Vector3 aiDir = autonomous != null ? autonomous.GetSteeringDirection() : transform.forward;
        Vector3 finalDir = Vector3.Slerp(playerDir, aiDir.normalized, autonomousWeight);

        ApplyMovement(finalDir);
    }

    Vector3 ConvertJoystickToWorld()
    {
        // joystick: y = forward/back, x = strafe, relative to transform
        Vector3 dir = transform.forward * joystick.y + transform.right * joystick.x;
        if (ascend) dir += transform.up;
        if (descend) dir -= transform.up;
        return Vector3.ClampMagnitude(dir, 1f);
    }

    void ApplyMovement(Vector3 dir)
    {
        Vector3 targetVel = dir * maxSpeed;
        Vector3 velocity = rb.velocity;
        Vector3 dv = targetVel - velocity;
        Vector3 accel = Vector3.ClampMagnitude(dv * acceleration, acceleration);
        rb.AddForce(accel, ForceMode.Acceleration);
    }

    // --- Methods to be called by UI ---
    public void SetJoystick(Vector2 v) => joystick = Vector2.ClampMagnitude(v, 1f);
    public void SetAscend(bool v) => ascend = v;
    public void SetDescend(bool v) => descend = v;
    public void ToggleManual() => autonomousWeight = autonomousWeight > 0.5f ? 0f : 1f;
}