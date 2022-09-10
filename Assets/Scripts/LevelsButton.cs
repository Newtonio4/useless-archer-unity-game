using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LevelsButton : MonoBehaviour
{
    public int unlockedLevels;
    public GameObject[] panels;
    public TextMeshProUGUI totalScore;

    public void ButtonClick()
    {
        int scoreCounter = 0;

        if (PlayerPrefs.HasKey("LastLevel"))
            unlockedLevels = PlayerPrefs.GetInt("LastLevel") + 1;
        else
            unlockedLevels = 1;

        for (int i = 0; i < panels.Length; i++)
        {
            if (i < unlockedLevels)
            {
                panels[i].SetActive(true);

                if (PlayerPrefs.GetInt("Arrows" + (i + 1)) == 0)
                {
                    panels[i].transform.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: 0";
                }
                else if (PlayerPrefs.GetInt("Arrows" + (i + 1)) <= GameManager.arrowIntervals[i + 1][0])
                {
                    panels[i].transform.Find("Image").gameObject.SetActive(true);
                    panels[i].transform.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: 3";
                    scoreCounter += 3;
                }
                else if (PlayerPrefs.GetInt("Arrows" + (i + 1)) <= GameManager.arrowIntervals[i + 1][1])
                {
                    panels[i].transform.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: 2";
                    scoreCounter += 2;
                }
                else
                {
                    panels[i].transform.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: 1";
                    scoreCounter += 1;
                }
            }
            else
                panels[i].SetActive(false);

            totalScore.text = "Total Score: " + scoreCounter;
        }
    }
}
