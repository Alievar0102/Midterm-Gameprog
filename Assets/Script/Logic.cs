using UnityEngine;

public class Logic : MonoBehaviour
{
    public void DestroyWhenTriggerDeadZone(GameObject target, Collider2D trigger)
    {
        // Check if isTrigger with the DeadZone
        if (trigger.gameObject.tag == "DeadZone")
        {
            Destroy(target);
        }
    }

    public void DestroyWhenCollisDeadZone(GameObject target, Collision2D collision)
    {
        // Check if collision with the DeadZone
        if (collision.gameObject.tag == "DeadZone")
        {
            Destroy(target);
        }
    }
}
