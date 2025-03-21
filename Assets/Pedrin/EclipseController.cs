using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EclipseController : MonoBehaviour
{
    public static EclipseController Instance;
    public SpriteRenderer eclipseRenderer; // Para exibir o eclipse no fundo
    public Sprite[] eclipsePhases; // Imagens do eclipse
    public Image darknessOverlay; // UI para escurecer a tela (deve estar em um Canvas)

    private int currentPhase = 0;
    private float eclipseProgress = 0f;
    public float maxEclipseTime = 60f; // Tempo total até o eclipse completo
    public float delayPerKill = 3f; // Quanto tempo cada inimigo morto atrasa o eclipse

    private float remainingTime;

    public TextMeshProUGUI tempoSobrevivenciaTexto;
    private float tempoSobrevivencia = 0f;
    private bool contando = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        remainingTime = maxEclipseTime;
        UpdateEclipseVisual();
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            eclipseProgress = 1 - (remainingTime / maxEclipseTime);
            UpdateEclipseVisual();
        }
        else
        {
            GameOver();
        }

        if (contando)
        {
            tempoSobrevivencia += Time.deltaTime;
            AtualizaTexto();
        }
    }

    public void OnEnemyKilled()
    {
        remainingTime += delayPerKill;
        remainingTime = Mathf.Min(remainingTime, maxEclipseTime);
    }

    void UpdateEclipseVisual()
    {
        int phaseIndex = Mathf.FloorToInt(eclipseProgress * (eclipsePhases.Length - 1));
        if (phaseIndex != currentPhase)
        {
            currentPhase = phaseIndex;
            eclipseRenderer.sprite = eclipsePhases[currentPhase];
        }

        // Atualiza a opacidade da tela escurecendo aos poucos
        darknessOverlay.color = new Color(0, 0, 0, eclipseProgress);
    }

    void AtualizaTexto()
    {
        int minutos = Mathf.FloorToInt(tempoSobrevivencia / 60);
        int segundos = Mathf.FloorToInt(tempoSobrevivencia % 60);
        tempoSobrevivenciaTexto.text = $"{minutos:00}:{segundos:00}";
    }

    void GameOver()
    {
        contando = false;
        PlayerPrefs.SetFloat("TempoSobrevivencia", tempoSobrevivencia);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Final");

    }
}
