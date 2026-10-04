using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    Player player;

    public GameObject spawnObjectContainer;
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    private void Start()
    {
        spawnObjectContainer = new GameObject("SpawnedObjectsContainer");
        player = FindFirstObjectByType<Player>();
    }

    private void Update()
    {
        gameOverPanel.SetActive(value: GameState.Instance.currentState == GameState.State.Lose);
        pausePanel.SetActive(value: GameState.Instance.currentState == GameState.State.Paused);
    }

    public void RestartGame()
    {
        foreach(Transform child in spawnObjectContainer.transform)
        {
            Destroy(child.gameObject);
        }

        player.transform.position = new Vector3(0, -3.4f, player.defaultPosition.position.z);

        player.GetComponent<Health>().health = player.startHealth; // Reset health
        player.GetComponent<Health>().UpdateHealth(); // Update health UI

        GameState.Instance.KillCount = 0;
        killCountText.text = GameState.Instance.KillCount.ToString();

        GameState.Instance.Dead = false;
        GameState.Instance.currentState = GameState.State.Playing;
    }

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
