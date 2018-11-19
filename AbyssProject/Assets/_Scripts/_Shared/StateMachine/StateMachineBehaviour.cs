using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachineBehaviour : MonoBehaviour
{
    
   
    public PlayerStateMachine machine;

    
    // Use this for initialization
    void Start()
    {
        


        
        machine.Init();

       

    }

    // Update is called once per frame
    void Update()
    {
        machine.Update();
    }


    private void FixedUpdate()
    {
        
    }
}
