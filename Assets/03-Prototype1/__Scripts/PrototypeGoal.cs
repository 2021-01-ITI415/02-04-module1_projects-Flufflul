﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeGoal : MonoBehaviour
{
	public static bool goalMet = false;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			PrototypeGoal.goalMet = true;

			Material mat = GetComponent<Renderer>().material;
			Color c = mat.color;
			c.a = 1;
			mat.color = c;
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
