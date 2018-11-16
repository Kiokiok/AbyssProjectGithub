using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachineBehaviour : MonoBehaviour
{
    
    [SerializeField]
    StateMachineBase machine = new StateMachineBase();

    
   
    // Use this for initialization
    void Start()
    {
        //machine.ChangeState(new IState());

        machine.Init();
    }

    // Update is called once per frame
    void Update()
    {
        machine.Update();
    }
}
