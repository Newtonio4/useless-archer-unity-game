using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioClip[] musicClips;

    private AudioSource audioSource;
    private int index = 0;

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("MusicHandler").Length > 1)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();

            PlayMusic();
        }
    }

    private void PlayMusic()
    {
        audioSource.PlayOneShot(musicClips[index]);
        StartCoroutine(WaitForLength(musicClips[index].length));

        if (index < musicClips.Length - 1)
            index++;
        else
            index = 0;
    }

    IEnumerator WaitForLength(float length)
    {
        yield return new WaitForSeconds(length);
        PlayMusic();
    }
}
