using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSettings : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectsSlider;
    public Toggle bloodParticlesCheckbox;

    public TextMeshProUGUI musicValue;
    public TextMeshProUGUI effectsValue;
    public TextMeshProUGUI bloodValue;

    public GameObject musicHandler;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVloume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVloume");
            effectsSlider.value = PlayerPrefs.GetFloat("EffectsVloume");
            bloodParticlesCheckbox.isOn = PlayerPrefs.GetInt("BloodParticles") > 0;
        }
        else
        {
            musicSlider.value = 0.5f;
            effectsSlider.value = 0.5f;
            bloodParticlesCheckbox.isOn = true;
        }


        OnUpdate();
    }

    public void OnUpdate()
    {
        //GM values
        GameManager.musicVolume = musicSlider.value;
        GameManager.effectsVolume = effectsSlider.value;
        GameManager.bloodParticle = bloodParticlesCheckbox.isOn;

        //Changing music volume in real time
        if (musicHandler == null)
            musicHandler = GameObject.FindGameObjectWithTag("MusicHandler");

        musicHandler.GetComponent<AudioSource>().volume = 0.1f * musicSlider.value;

        //Changing text labels
        musicValue.text = (int)Mathf.Floor(musicSlider.value * 100) + "%";
        effectsValue.text = (int)Mathf.Floor(effectsSlider.value * 100) + "%";

        if (bloodParticlesCheckbox.isOn)
            bloodValue.text = "ON";
        else
            bloodValue.text = "OFF";

    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVloume", musicSlider.value);
        PlayerPrefs.SetFloat("EffectsVloume", effectsSlider.value);

        if (bloodParticlesCheckbox.isOn)
            PlayerPrefs.SetInt("BloodParticles", 1);
        else
            PlayerPrefs.SetInt("BloodParticles", -1);
    }
}
