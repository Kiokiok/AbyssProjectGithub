using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public class State : ScriptableObject 
{
    public string Name;


    public string[] conditionsTargetStates;


    public virtual void Enter()
    {

    }

    public virtual State CheckConditions()
    {
        return null;
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

    public void setStatesForConditions(Dictionary<string,State> sta)
    {


    }

    

}
