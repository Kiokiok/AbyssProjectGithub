using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_One : EventBase {




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public override void EventStart()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }
}
