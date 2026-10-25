
using UnityEngine;
using TMPro;

public class WasteBin : MonoBehaviour
{
    public string acceptedWasteType = "Recyclable";
    public int pointsPerItem = 10;

    public TMP_Text resultText;

    private AudioSource pickupAudio;
    private AudioSource okAudio;
    private AudioSource wrongAudio;

    private void Start()
    {
        AudioSource[] sounds = FindFirstObjectByType<PlayerMovement>()
            .GetComponents<AudioSource>();

        if (sounds.Length > 0)
            pickupAudio = sounds[0];

        if (sounds.Length > 1)
            okAudio = sounds[1];

        if (sounds.Length > 2)
            wrongAudio = sounds[2];
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        WasteItem waste = player.GetCarriedWaste();

        if (waste == null)
            return;

        if (waste.wasteType == acceptedWasteType)
        {
            GameManager manager = FindFirstObjectByType<GameManager>();

            if (manager != null)
                manager.AddPoints(pointsPerItem);

            if (resultText != null)
            {
                resultText.color = Color.green;
                resultText.text = "¡Muy bien! Residuo clasificado correctamente.";
            }

            if (okAudio != null)
                okAudio.Play();

            Destroy(waste.gameObject);
            player.ClearCarriedWaste();
        }
        else
        {
            if (resultText != null)
            {
                resultText.color = Color.red;
                resultText.text = "Contenedor incorrecto. Intenta con otro.";
            }

            if (wrongAudio != null)
                wrongAudio.Play();
        }
    }
}
