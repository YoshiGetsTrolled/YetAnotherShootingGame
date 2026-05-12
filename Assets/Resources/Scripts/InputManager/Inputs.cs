using UnityEngine;
using UnityEngine.InputSystem;
public class Inputs : MonoBehaviour
{
    [Header("入力")]
    public static Vector2 moveInput;
    public static bool buttonA;
    public static bool buttonADown;
    [SerializeField] private bool prevButtonA;

    public static bool buttonB;
    public static bool buttonBDown;
    [SerializeField] private bool prevButtonB;

    [Header("コンポーネント")]
    [SerializeField]
    private PlayerInput input;
    [SerializeField] InputAction iButtonA;
    [SerializeField] InputAction iButtonB;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        iButtonA = input.actions["ButtonA"];
        iButtonB = input.actions["ButtonB"];
    }
    private void Update()
    {
        buttonA = iButtonA.IsPressed();
        buttonADown = buttonA && !prevButtonA;
        prevButtonA = buttonA;

        buttonB = iButtonB.IsPressed();
        buttonBDown = buttonB && !prevButtonB;
        prevButtonB = buttonB;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
