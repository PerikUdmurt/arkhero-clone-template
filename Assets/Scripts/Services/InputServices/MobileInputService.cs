using ArkheroClone.Infrastructure.Timer;
using UnityEngine;

public class MobileInputService : InputService
{
    private Joystick _joystick;

    public override Vector2 GetDirection() => _joystick.Direction;
    
    public void ProvideJoystick(Joystick joystick)
        => _joystick = joystick;
}
