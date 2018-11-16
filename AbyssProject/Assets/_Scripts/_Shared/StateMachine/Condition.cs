using UnityEngine;
using System.Collections;

[System.Serializable]
public class Condition 
{
    public State targetState;

    Data objectData;

    StateMachineBase parent;

    public virtual bool Check()
    {


        return false;
    }


    //public static Condition[] AllConditions;

}
