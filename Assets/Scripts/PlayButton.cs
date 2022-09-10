using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public Scene sceneName;

    public void ButtonClick()
    {
        //if (int.Parse(sceneName.ToString().Substring(5)) <= PlayerPrefs.GetInt("LastLevel"))
            Loader.Load(sceneName);
    }
}
