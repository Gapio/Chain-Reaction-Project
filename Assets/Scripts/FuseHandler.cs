using UnityEngine;

public class FuseHandler : MonoBehaviour
{
    public GameObject disable;
    public GameObject enable;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Domino"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            BoxCollider bc = GetComponent<BoxCollider>();

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            bc.enabled = false;
            disable.SetActive(false);
            enable.SetActive(true);
        }
    }
}
