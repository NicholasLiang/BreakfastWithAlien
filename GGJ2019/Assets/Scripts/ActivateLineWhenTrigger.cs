using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLineWhenTrigger : MonoBehaviour {
    private LineRenderer line;

    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
	}
}
