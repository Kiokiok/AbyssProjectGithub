using System.Collections;
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

    public BoxCollider col;

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector3(transform.position.x + (col.center.x * transform.localScale.x), transform.position.y + (col.center.y * transform.localScale.y), transform.position.z + (col.center.z * transform.localScale.z)), Vector3.one * 0.5F);
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
