﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {
    
    [System.Serializable]
    public struct obj
    {
        public GameObject inteObj;
        public BaseEvent[] eventPlay;
    }
    public obj[] interactions;

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + Vector3.up * transform.GetComponent<BoxCollider>().center.y, Vector3.one * 0.5F);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            foreach (obj o in interactions)
                foreach (BaseEvent s in o.eventPlay)
                    s.StartEvent();
        }
    }
}
