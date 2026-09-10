using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4.0f;
    public float gravity = -9.81f;

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 100.0f;

    private float xRotation = 0f;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Блокуємо курсор
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. Огляд мишею (Горизонтальний поворот тіла, вертикальний — очі)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f); // Обмеження погляду вгору/вниз

        transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y + mouseX, 0f);

        // 2. Перевірка приземлення (чи стоїмо на підлозі)
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            // Невелике від'ємне значення, щоб персонаж впевнено притискався до підлоги
            velocity.y = -2f;
        }

        // 3. Рух горизонтальною площиною (Без зльоту вгору при погляді в небо)
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        // Беремо напрямок погляду, але ігноруємо нахил по вертикалі (Y = 0)
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = right * x + forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 4. Прикладне застосування гравітації
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Натисни ESC для звільнення курсора
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}