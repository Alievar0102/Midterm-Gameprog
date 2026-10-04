using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    #region KillCount
    public TMP_Text killCountText;

    public void UpdateKillCount()
    {
        // Update kill count game state dan UI
        GameState.Instance.KillCount++;
        killCountText.text = GameState.Instance.KillCount.ToString();
    }   
    #endregion
}
