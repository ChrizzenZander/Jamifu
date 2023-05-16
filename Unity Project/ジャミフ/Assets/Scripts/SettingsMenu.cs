using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Text displayText;

    public TMP_InputField iField;
    public TMP_Text placeHolderText;

    public Slider slider;
    public float GetMasterLevel()
    {
        float value;
        bool result = audioMixer.GetFloat("volume", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }

    private void Start()
	{
        slider.value = GetMasterLevel();
	}

	public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        displayText.text = "Volume: " + Mathf.Round(80+volume).ToString();
    }

    public void OpenAboutUs()
    {
        Application.OpenURL("https://youtu.be/dQw4w9WgXcQ");
    }

    public void CheckReset()
    {
        if (iField.text == "Reset")
        {
            Debug.Log("Reset Points");
            iField.text = "";
            placeHolderText.text = "Points were reset";
        }
        else
        {
            iField.text = "";
            placeHolderText.text = "Incorrect";
        }
    }
}
