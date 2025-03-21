using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;


    void Start()
    {
        AudioManager.Instance.Func("Inicial", "Principal");

        float savedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        float savedSfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.75f);
        musicVolumeSlider.value = savedMusicVolume;
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.value = savedSfxVolume;
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.MusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SFXVolume(volume);
    }
}
