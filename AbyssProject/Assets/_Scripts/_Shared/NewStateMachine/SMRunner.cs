using UnityEngine;
using System.Collections;
using SM;


public class SMRunner : MonoBehaviour
{


    public SMPlayer machine = new SMPlayer();

    // Use this for initialization
    void Start()
    {
        machine.Init();

    }

    // Update is called once per frame
    void Update()
    {
        machine.Execute();
    }
}
