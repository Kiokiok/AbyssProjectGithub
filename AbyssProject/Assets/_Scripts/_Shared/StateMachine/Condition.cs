using UnityEngine;
using System.Collections;

[System.Serializable]
public class Condition : ScriptableObject
{

    public string Name;


    public State targetState;

    public virtual bool Check<F>(F data) 
    {
        
        
        return false;
    }

    


}


