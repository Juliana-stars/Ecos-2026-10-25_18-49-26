
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public Transform carryPosition;
    public float rotationSpeed = 10f;

    private CharacterController controller;
    private WasteItem carriedWaste;
    private float verticalVelocity;
    private AudioSource audioSource;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // Gira el personaje hacia la direccion en la que camina.
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Aplica gravedad y mantiene al personaje en el suelo.
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        movement.y = verticalVelocity;

        controller.Move(movement * moveSpeed * Time.deltaTime);

        // Mantiene el residuo recogido en el punto indicado.
        if (carriedWaste != null && carryPosition != null)
        {
            carriedWaste.transform.position = carryPosition.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (carriedWaste != null)
        {
            return;
        }

        WasteItem waste = other.GetComponent<WasteItem>();

        if (waste == null)
        {
            waste = other.GetComponentInParent<WasteItem>();
        }

        if (waste != null)
        {
            carriedWaste = waste;

            // Reproduce el sonido al recoger un residuo.
            if (audioSource != null)
            {
                audioSource.Play();
            }

            Debug.Log("Has recogido: " + waste.wasteType);
        }
    }

    public WasteItem GetCarriedWaste()
    {
        return carriedWaste;
    }

    public void ClearCarriedWaste()
    {
        carriedWaste = null;
    }
}
