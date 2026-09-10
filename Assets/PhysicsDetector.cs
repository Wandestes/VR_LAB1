using UnityEngine;

public class PhysicsDetector : MonoBehaviour
{
    // 1. Стандартна колізія (для Rigidbody)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[КОЛІЗІЯ Rigidbody] Об'єкт '{gameObject.name}' зіткнувся з '{collision.gameObject.name}'");
    }

    // 2. Стандартний тригер (для Rigidbody / Collider)
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[ТРИГЕР] Об'єкт '{gameObject.name}' перетнув зону '{other.gameObject.name}'");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string objectName = hit.gameObject.name.ToLower();

        // Ігноруємо підлогу за назвою (якщо містить "floor", "plane" або "terrain")
        if (objectName.Contains("floor") || objectName.Contains("wall") || objectName.Contains("terrain"))
        {
            return;
        }

        // Логуємо лише зіткнення зі стінами, меблями та іншими предметами
        Debug.Log($"[КОЛІЗІЯ Персонажа] Камера зіткнулася з об'єктом '{hit.gameObject.name}'");
    }
}