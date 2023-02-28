using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public GameManager manager;
    Renderer _color;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _color = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bucket"))
        {
            TechnicalProcess();
            manager.BallEntered();

        }
        else if (other.CompareTag("Ground"))
        {
            TechnicalProcess();
            manager.DidntEnter();
        }
    }
    void TechnicalProcess()
    {
        manager.ParcEffect(gameObject.transform.position, _color.material.color);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}