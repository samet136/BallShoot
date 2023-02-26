using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public GameManager manager;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bucket"))
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            manager.BallEntered();
            //partýcle efect oalcak
            // sayýlar guncellenecek
            //Slider
        }
        else if (other.CompareTag("Ground"))
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            manager.DidntEnter();
        }
    }
}
