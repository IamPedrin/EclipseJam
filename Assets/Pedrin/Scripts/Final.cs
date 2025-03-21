using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    public TextMeshProUGUI tempoFinal;

    void Start()
    {
        float tempoSobrevivencia = PlayerPrefs.GetFloat("TempoSobrevivencia", 0);
        int minutos = Mathf.FloorToInt(tempoSobrevivencia / 60);
        int segundos = Mathf.FloorToInt(tempoSobrevivencia % 60);
        tempoFinal.text = $"Você sobreviveu por {minutos:00}:{segundos:00}";
    }

    public static void ReturnMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
