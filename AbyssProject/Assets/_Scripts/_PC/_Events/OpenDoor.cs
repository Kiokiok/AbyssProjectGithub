using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : BaseEvent {

    public float openTime = 2F;
    [Range(-180, 180)]
    public float maxOpenDegree = -90F;


    public override void StartEvent()
    {
        Debug.Log("LAUNCH OpenDoor");

        StartCoroutine(Open());
    }

    public void LaunchEvent()
    {
      
    }

    IEnumerator Open()
    {
        Vector3 initRot = transform.eulerAngles;

        transform.eulerAngles = new Vector3(initRot.x, initRot.y + maxOpenDegree, initRot.z);
        yield return null;
    }
}
