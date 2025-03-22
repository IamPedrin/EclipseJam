using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class EclipseController : MonoBehaviour
{
    public static EclipseController Instance;
    public SpriteRenderer eclipseRenderer; // Para exibir o eclipse no fundo
    public Sprite[] eclipsePhases; // Imagens do eclipse
    public Image darknessOverlay; // Imagem para escurecer a tela

    private int currentPhase = 0;
    private float eclipseProgress = 0f;
    public float maxEclipseTime = 60f; // Tempo total até o eclipse completo
    public float delayPerKill = 3f; // Quanto tempo cada inimigo morto atrasa o eclipse
    public float advancePerHit = 3f; //Quanto tempo avança o eclipse em cada hit inimido

    private float remainingTime;

    public TextMeshProUGUI tempoSobrevivenciaTexto;
    private float tempoSobrevivencia = 0f;
    private bool contando = true;

    public SpriteRenderer sprite;

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

    public void OnEnemyHit()
    {
        remainingTime -= advancePerHit;
        remainingTime = Mathf.Min(remainingTime, maxEclipseTime);
        StartCoroutine(flashRed());
    }

    void UpdateEclipseVisual()
    {
        int phaseIndex = Mathf.FloorToInt(eclipseProgress * (eclipsePhases.Length - 1));
        if (phaseIndex != currentPhase)
        {
            currentPhase = phaseIndex;
            eclipseRenderer.sprite = eclipsePhases[currentPhase];
        }

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
        AudioManager.Instance.PlayMusic("Fim");
        SceneManager.LoadScene("Final");

    }

    private IEnumerator flashRed()
    {
        sprite.color = Color.red;
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(0.8f);
        sprite.color = Color.white;
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
}
