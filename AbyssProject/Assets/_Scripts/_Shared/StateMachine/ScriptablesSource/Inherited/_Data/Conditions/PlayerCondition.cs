using UnityEngine;
using System.Collections;


[CreateAssetMenu(menuName = "StateMachine/Player/Conditions/Base")]
public class PlayerCondition : Condition
{

    public string debug;

    public override bool Check<F>(F data) 
    {
        Debug.Log(debug);



        return false;
    }

}
