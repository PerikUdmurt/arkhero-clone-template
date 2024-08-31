using UnityEngine;

public class HUDRoot : MonoBehaviour
{
    [SerializeField]
    private Joystick _joystick;

    public Joystick GetJoystick() => _joystick;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
