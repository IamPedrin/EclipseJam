using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject creditsPanel;

    void Start()
    {
        PlayMusicSequence();
        //StartCoroutine(PlayMusicSequence());
    }
    void PlayMusicSequence()
    {
        // Obtém os Áudios
        AudioSource musicInicial = AudioManager.Instance.GetMusicAudioSource("Inicial");
        AudioSource musicaPrincipal = AudioManager.Instance.GetMusicAudioSource("Principal");

        // Configuração das músicas
        musicInicial.loop = false;
        musicaPrincipal.loop = true; // O loop precisa estar ativo ANTES de tocar

        // Inicia a música inicial imediatamente
        musicInicial.Play();

        // Calcula o tempo exato para começar a música principal
        double startTime = AudioSettings.dspTime + musicInicial.clip.length;
        
        // Agenda a música principal
        musicaPrincipal.PlayScheduled(startTime);

        // Ajusta o ponto de início correto do loop
        musicaPrincipal.time = 0;
    }

    // IEnumerator PlayMusicSequence()
    // {
    //     AudioSource musicInicial = AudioManager.Instance.GetMusicAudioSource("Inicial");
    //     musicInicial.loop = false;
    //     musicInicial.Play();
    //     yield return new WaitForSeconds(musicInicial.clip.length);
    //     AudioSource musicaPrincipal = AudioManager.Instance.GetMusicAudioSource("Principal");
    //     musicaPrincipal.loop = true;
    //     musicaPrincipal.Play();
    // }

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
}
