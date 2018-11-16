using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMachineBase
{

    // Le state actuel
    [HideInInspector]
    public State currentState;

    State newState;

    Data data;



    // Drag n Drop container
    // Les states utilisés par cette machine
    public State[] MachineStates;

    Dictionary<string, State> MachineStatesDic = new Dictionary<string, State>();
    
    
    
    public void ChangeState(State newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();

        newState = null;
    }


    public void Update()
    {
        if (currentState != null)
        {
            
            if (newState != currentState)
                ChangeState(newState);

            currentState.Execute();
        }
    }


    public void InitStates()
    {
        
       for(int i = 0; i < MachineStates.Length; i++)
       {
            MachineStatesDic.Add(MachineStates[i].Name, GameObject.Instantiate(MachineStates[i]));

       }

        foreach(var sta in MachineStatesDic)
        {
            sta.Value.setStatesForConditions(MachineStatesDic);

        }



    }
}
