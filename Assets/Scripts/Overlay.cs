using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    public Scene scene;

    public Image redImage;
    public Image greenImage;
    public Image blueImage;

    private Player player;
    private float startTime;

    void Start()
    {
        Potion.OnCollect += ChangeLabel;
        Player.OnShot += ChangeLabel;

        player = GameObject.Find("Player").GetComponent<Player>();

        startTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Time.time - startTime > 0.5f)
        {
            ResetLevel();
        }
    }

    private void OnDisable()
    {
        Potion.OnCollect -= ChangeLabel;
        Player.OnShot -= ChangeLabel;
    }

    private void ChangeLabel()
    {
        redImage.enabled = false;
        greenImage.enabled = false;
        blueImage.enabled = false;

        if (player.arrowType == ArrowType.Default)
            return;

        else if (player.arrowType == ArrowType.Tripple)
        {
            redImage.enabled = true;
        }
        else if (player.arrowType == ArrowType.Invulnerable)
        {
            greenImage.enabled = true;
        }
        else if (player.arrowType == ArrowType.Long)
        {
            blueImage.enabled = true;
        }
    }

    public void ResetLevel()
    {
        Loader.Load(scene);
    }
}
