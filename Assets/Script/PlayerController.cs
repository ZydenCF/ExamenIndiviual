using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(horizontal, 0, vertical) * velocidad;
        rb.linearVelocity = new Vector3(movimiento.x, rb.linearVelocity.y, movimiento.z);
    }
}

