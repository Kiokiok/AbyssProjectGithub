using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbWall : BaseEvent {


    public override void StartEvent()
    {
        Debug.Log("LAUNCH ClimbWall");

        StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<playerCC>().climbAWall(this.gameObject));
    }
}
