using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    Menu,
    Loading,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    Level7,
    Level8,
    Level9,
    Level10,
    Level11,
    Level12,
    Level13,
    Level14,
    Level15,
    Level16,
    Level17,
    Level18,
    Level19,
    Level20
}

public static class Loader
{
    private static Action onLoaderCallback;
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());

        //onLoaderCallback = () =>
        //{
        //    SceneManager.LoadScene(scene.ToString());
        //};

        //SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
