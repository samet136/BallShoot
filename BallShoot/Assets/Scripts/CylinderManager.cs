using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CylinderManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject Cylinder;

    bool ButtonPressed;
    [SerializeField] private float RotateForce;
    [SerializeField] private string Direction;


    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonPressed = false;

    }



    // Update is called once per frame
    void Update()
    {
        if (ButtonPressed)
        {
            if (Direction == "Left")
            {
                Cylinder.transform.Rotate(0, RotateForce * Time.deltaTime, 0, Space.Self);
            }
            else
            {
                Cylinder.transform.Rotate(0, -RotateForce * Time.deltaTime, 0, Space.Self);
            }

        }
        else
        {

        }
    }
}
