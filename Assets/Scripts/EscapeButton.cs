using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeButton : MonoBehaviour
{
    private GameObject mainCanvas;

    private void Start()
    {
        mainCanvas = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainCanvas.activeInHierarchy)
                Hide();
            else
                Show();
        }
    }

    public void Show()
    {
        Cursor.visible = true;
        mainCanvas.SetActive(true);
        GameManager.isGameActive = false;
    }

    public void Hide()
    {
        Cursor.visible = false;
        mainCanvas.SetActive(false);
        GameManager.isGameActive = true;
    }
}
