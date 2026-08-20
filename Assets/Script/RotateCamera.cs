using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private InputSystem_Actions conTrols;

    public float rotationSpeed = 150f;

    private void Awake()
    {
        conTrols = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        conTrols.Player.Enable();
        Debug.Log(conTrols.Player.Move);
    }

    void Update()
    {
        // Read move action (Which is a Vector 2)
        Vector2 moveInput = conTrols.Player.Move.ReadValue<Vector2>();
        float horizontalInput = moveInput.x; // Right/Left (A/D or arrow keys)
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
