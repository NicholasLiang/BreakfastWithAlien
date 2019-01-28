using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    public AudioSource audio;
    public Transform lookAt;
    public Transform controllerPos;
    public int frameToGet;
    public float tol;

    private Vector3[] controllerPosArray;
    private int counter;

    public Animator animator;


    // Use this for initialization
    void Start()
    {
        controllerPosArray = new Vector3[frameToGet];
        counter = 0;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        controllerPosArray[counter] = controllerPos.position;

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
        if (controllerPosArray.Length > 2)
        {
            for (int i = 0; i < frameToGet - 1; i++)
            {
                totalDistances = totalDistances + Vector3.Distance(controllerPosArray[i], controllerPosArray[i + 1]);
            }
        }

        float lookingPos = lookAt.rotation.eulerAngles.y;
        if (lookingPos > 250 && lookingPos < 310)
        {
            if (totalDistances > frameToGet * tol)
            {
                animator.SetBool("Waving", true);
            }
            else
            {
                animator.SetBool("Waving", false);
            }
        }
    }
}
