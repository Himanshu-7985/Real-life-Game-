using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public Joystick joystick; // any joystick asset you add
    public ThirdPersonController player;

    void Update()
    {
        if (joystick != null)
        {
            float h = joystick.Horizontal;
            float v = joystick.Vertical;
            // map to Unity axes for compatibility
            // This approach uses Input simulation; another better approach is to modify controller to read joystick directly
            // For simplicity:
            // move values into player's input via direct method (if you modified controller to accept external input)
        }
    }
}
