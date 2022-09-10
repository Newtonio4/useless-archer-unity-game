using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            Debug.Log("LoaderCallback");
            isFirstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
