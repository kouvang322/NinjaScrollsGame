using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Dreamteck.WelcomeWindow.WindowPanel;

public class EnemyKillUI : MonoBehaviour
{
    private TextMeshProUGUI enemyKillText;

    public PlayerInventory inventory;
    public ShrineBehavior shrine;

    // Start is called before the first frame update
    void Start()
    {
        enemyKillText = GetComponent<TextMeshProUGUI>();

        StartCoroutine(WaitToUpdateText());

    }

    // Update is called once per frame
    public void UpdateEnemyKillCountText(PlayerKills playerKills)
    {
        //Debug.Log("enemy killed");
        //enemyKillText.text = $"{playerKills.NumberOfEnemyKills} / 1";
    }

    IEnumerator WaitToUpdateText()
    {
        yield return new WaitForSeconds(.05f);
        enemyKillText.text = $"{inventory.NumberScrollsCollected} / {shrine.TotalEnemyCount}";

        //Debug.Log(enemyKillText.text);
    }
}
