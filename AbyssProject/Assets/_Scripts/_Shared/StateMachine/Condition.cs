using UnityEngine;
using System.Collections;

[System.Serializable]
public class Condition : ScriptableObject
{

    public string Name;

    public State targetState;

   

    public StateMachineBase parent;

   

    public virtual bool Check()
    {
        
        
        return false;
    }

    



    

}
