using UnityEngine;

public class Logic : MonoBehaviour
{
    public void DestroyWhenTriggerDeadZone(GameObject target, Collider2D trigger)
    {
        // Check apakah isTrigger dengan DeadZone
        if (trigger.gameObject.tag == "DeadZone")
        {
            Destroy(target);
        }
    }

    public void DestroyWhenCollisDeadZone(GameObject target, Collision2D collision)
    {
        // Check apakah collision dengan DeadZone
        if (collision.gameObject.tag == "DeadZone")
        {
            Destroy(target);
        }
    }
}
