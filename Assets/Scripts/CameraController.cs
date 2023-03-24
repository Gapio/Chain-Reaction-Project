using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float moveSpeed = 10.0f;
    public float moveSpeedMultiplier = 2.0f;
    public float interactionDistance = 5.0f;
    public float crosshairSize = 3f;
    public Color crosshairColor = Color.white;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // Clamp rotation Y

        transform.localRotation = Quaternion.Euler(-rotationY, rotationX, 0.0f); // Rotate the camera based on mouse input

        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed *= moveSpeedMultiplier;

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.Space))
            movement.y += 1f;
        if (Input.GetKey(KeyCode.LeftShift))
            movement.y -= 1f;

        movement = transform.TransformDirection(movement) * speed * Time.deltaTime;
        transform.position += movement;

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
            {
                if (hit.transform.CompareTag("Domino"))
                {
                    hit.transform.Rotate(Vector3.right, 10f);
                }
            }
        }
    }

    void OnGUI()
    {
        // Draw crosshair in center of screen
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;

        GUI.color = crosshairColor;
        GUI.DrawTexture(new Rect(centerX - (crosshairSize / 2), centerY - (crosshairSize / 2), crosshairSize, crosshairSize), Texture2D.whiteTexture);
    }
}