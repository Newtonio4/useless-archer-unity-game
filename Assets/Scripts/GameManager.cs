using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameActive = false;

    //Settings
    public static float musicVolume = 0.5f;
    public static float effectsVolume = 0.5f;
    public static bool bloodParticle = true;

    public static Dictionary<int, int[]> arrowIntervals = new Dictionary<int, int[]>
    {
        {1, new int[] {1, 2 } },
        {2, new int[] {2, 3 } },
        {3, new int[] {4, 6 } },
        {4, new int[] {7, 9 } },
        {5, new int[] {2, 4 } },
        {6, new int[] {5, 7 } },
        {7, new int[] {6, 9 } },
        {8, new int[] {11, 15 } },
        {9, new int[] {15, 17 } },
        {10, new int[] {2, 6 } },
        {11, new int[] {3, 4 } },
        {12, new int[] {4, 5 } },
        {13, new int[] {4, 6 } },
        {14, new int[] {2, 3 } },
        {15, new int[] {2, 3 } },
        {16, new int[] {6, 8 } },
        {17, new int[] {2, 3 } },
        {18, new int[] {2, 4 } },
        {19, new int[] {7, 9 } },
        {20, new int[] {8, 12 } },
    };

    public static void EndGame(int level, int arrows)
    {
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            if (PlayerPrefs.GetInt("LastLevel") < level)
                PlayerPrefs.SetInt("LastLevel", level);
        }
        else
            PlayerPrefs.SetInt("LastLevel", level);

        if (PlayerPrefs.HasKey("Arrows" + level))
        {
            if (PlayerPrefs.GetInt("Arrows" + level) > arrows)
                PlayerPrefs.SetInt("Arrows" + level, arrows);
        }
        else
            PlayerPrefs.SetInt("Arrows" + level, arrows);

        isGameActive = false;
    }
}
