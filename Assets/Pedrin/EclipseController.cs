using UnityEngine;
using UnityEngine.UI;

public class EclipseController : MonoBehaviour
{
    public SpriteRenderer eclipseRenderer; // Para exibir o eclipse no fundo
    public Sprite[] eclipsePhases; // Imagens do eclipse
    public Image darknessOverlay; // UI para escurecer a tela (deve estar em um Canvas)
    
    private int currentPhase = 0;
    private float eclipseProgress = 0f;
    public float maxEclipseTime = 60f; // Tempo total até o eclipse completo
    public float delayPerKill = 3f; // Quanto tempo cada inimigo morto atrasa o eclipse

    private float remainingTime;

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

    void GameOver()
    {
        // Eclipse total aconteceu - aqui você pode encerrar o jogo
        Debug.Log("O Eclipse chegou! Fim do jogo.");
    }
}
