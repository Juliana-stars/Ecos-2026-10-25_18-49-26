
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public TMP_Text resultText;

    public float timeLeft = 60f;
    public int score = 0;
    public int targetScore = 30;

    private bool gameEnded = false;

    private AudioSource winAudio;
    private AudioSource failAudio;

    void Start()
    {
        // Busca los sonidos del jugador.
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();

        if (player != null)
        {
            AudioSource[] sounds = player.GetComponents<AudioSource>();

            if (sounds.Length > 3)
                winAudio = sounds[3];

            if (sounds.Length > 4)
                failAudio = sounds[4];
        }

        if (resultText != null)
        {
            resultText.text = "";
            resultText.color = Color.white;
        }

        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }

        if (timerText != null)
        {
            timerText.text = "Tiempo: " + Mathf.CeilToInt(timeLeft);
        }
    }

    void Update()
    {
        if (gameEnded)
            return;

        timeLeft -= Time.deltaTime;
        timeLeft = Mathf.Max(0f, timeLeft);

        if (timerText != null)
        {
            timerText.text = "Tiempo: " + Mathf.CeilToInt(timeLeft);
        }

        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }

        if (timeLeft <= 0f)
        {
            EndGame();
        }
    }

    public void AddPoints(int points)
    {
        if (gameEnded)
            return;

        score += points;

        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }
    }

    void EndGame()
    {
        gameEnded = true;

        if (timerText != null)
        {
            timerText.text = "¡Tiempo terminado!";
        }

        if (resultText != null)
        {
            if (score >= targetScore)
            {
                resultText.color = new Color(0f, 0.7f, 0f, 1f);
                resultText.text = "¡GANASTE! Barrio limpio.";

                if (winAudio != null)
                    winAudio.Play();
            }
            else
            {
                resultText.color = new Color(1f, 0f, 0f, 1f);
                resultText.text = "¡FIN DEL JUEGO! Sigue intentando.";

                if (failAudio != null)
                    failAudio.Play();
            }
        }

        Debug.Log("Partida terminada. Puntuación final: " + score);
    }
}
