
using UnityEngine;

public class WasteItem : MonoBehaviour
{
    public string wasteType = "Recyclable";
    public int points = 10;

    private void Start()
    {
        Debug.Log("Script WasteItem activo en: " + gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("El residuo detectó contacto con: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Residuo recogido: " + wasteType);
        }
    }
}
