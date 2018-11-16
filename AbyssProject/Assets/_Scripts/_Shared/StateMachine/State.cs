using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public abstract class State : ScriptableObject 
{
    public string Name;

    public Condition[] stateConditions;


    
    [HideInInspector]
    public StateMachineBase parent;

    

    public virtual void Enter()
    {

    }

    public virtual void Execute()
    {

    }
    public virtual void FixedExecute()
    {

    }
    public virtual void Exit()
    {

    }

    public virtual State CheckConditions()
    {
        foreach(Condition con in stateConditions)
        {
            if (con.Check())
            {
                return con.targetState;
            }
        }
         return this;
    }

    

}
