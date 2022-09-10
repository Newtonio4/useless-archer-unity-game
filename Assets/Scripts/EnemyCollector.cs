using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCollector : MonoBehaviour
{
    public GameObject canvas;
    public Player player;
    public TextMeshProUGUI arrowCounter;
    public TextMeshProUGUI rewardCounter;
    public int levelNumber;

    public void CheckChildren()
    {
        StartCoroutine("WaitForDeath");
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(0.3f);
        if (gameObject.transform.childCount == 0)
        {
            Cursor.visible = true;
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            arrowCounter.text = "You used " + player.arrows + " arrows!";
            
            if (player.arrows <= GameManager.arrowIntervals[levelNumber][0])
                rewardCounter.text = "MIND BLOWING";
            else if (player.arrows <= GameManager.arrowIntervals[levelNumber][1])
                rewardCounter.text = "VERY GOOD";
            else
                rewardCounter.text = "NICE";

            canvas.SetActive(true);
            GameManager.EndGame(levelNumber, player.arrows);
        }
    }
}
