using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    public Transform lookAt;
    public Transform controllerPos;
    public int frameToGet;
    public float tol;

    public Transform[] controllerPosArray;
    public int counter;
    public bool isActivated;


    // Use this for initialization
    void Start()
    {
        controllerPosArray = new Transform[frameToGet];
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        controllerPosArray[counter] = controllerPos;

        calculateTheTotalDistance();

        counter = counter + 1;
        if (counter >= frameToGet)
        {
            counter = 0;
        }
    }

    void calculateTheTotalDistance()
    {
        float totalDistances = 0;
        for (int i = 0; i < frameToGet - 1; i++)
        {
            totalDistances = totalDistances + Vector3.Distance(controllerPosArray[i].position, controllerPosArray[i + 1].position);
        }

        if (isActivated = false)
        {
            if (totalDistances > frameToGet * tol)
                isActivated = true;
        }
    }
}
