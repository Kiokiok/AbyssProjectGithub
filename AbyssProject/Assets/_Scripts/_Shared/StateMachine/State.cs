using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public abstract class State : ScriptableObject
{
    public string Name;

    public Condition[] stateConditions;

    public Actions[] stateActions;

    
    [HideInInspector]
   

    

    public virtual void Enter<F>(F data) where F : Data
    {

    }

    public virtual void Execute<F>(F data) where F : Data
    {
        foreach(Actions act in stateActions)
        {
            act.Execute(data);
        }

    }
    public virtual void FixedExecute<F>(F data) where F : Data
    {

    }
    public virtual void Exit<F>(F data) where F : Data
    {

    }

    public virtual State CheckConditions<F>(F data) where F : Data
    {
        foreach(Condition con in stateConditions)
        {
            if (con.Check(data))
            {
                return con.targetState;
            }
        }
         return this;
    }

    

}
