using UnityEngine;

public class DesktopInputService : InputService
{
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";

    public DesktopInputService()
    {

    }

    public override Vector2 GetDirection()
        => new(
            x: Input.GetAxis(_horizontal),
            y: Input.GetAxis(_vertical));
}
