using UnityEngine;

public class PhysicsDetector : MonoBehaviour
{
    // Фізичне зіткнення двох твердих об'єктів
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[КОЛІЗІЯ Rigidbody] Об'єкт '{gameObject.name}' зіткнувся з '{collision.gameObject.name}'");
    }

    // Зіткнення гравця (CharacterController) із перешкодами
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[ТРИГЕР] Об'єкт '{gameObject.name}' перетнув зону '{other.gameObject.name}'");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string objectName = hit.gameObject.name.ToLower();

        // Ігноруємо підлогу та стіни за назвою
        if (objectName.Contains("floor") || objectName.Contains("wall") || objectName.Contains("terrain"))
        {
            return;
        }

        // Логуємо лише зіткнення зі стінами, меблями та іншими предметами
        Debug.Log($"[КОЛІЗІЯ Персонажа] Камера зіткнулася з об'єктом '{hit.gameObject.name}'");
    }
}