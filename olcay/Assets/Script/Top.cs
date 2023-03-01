using UnityEngine;

public class Top : MonoBehaviour
{
    public GameManager _GameManager;
    Rigidbody rb;
    Renderer renk;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        renk = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kova"))
        {
            Teknikislem();
            _GameManager.TopGirdi();
        }
        else if (other.CompareTag("AltObje"))
        {
            Teknikislem();
            _GameManager.TopGirmedi();
        }
    }
    void Teknikislem()
    {
        _GameManager.ParcEfekt(gameObject.transform.position, renk.material.color);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
