using UnityEngine;
using System.Collections;

public class SSMBase 
{

    #region VARIABLES

    public enum SSMPlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping,
        Falling,

    }

    public delegate SSMPlayerState SSMFuncDelegate(SSMPlayerState state);

    public SSMFuncDelegate Exec;

   

    public SSMPlayerState currentState;

    #endregion

    public SSMPlayerState CurrentState
    {
        get { return currentState;}
        set
        {
            if (value == currentState) return;

            switch(value)
            {
                case SSMPlayerState.Idle:

                    Exec = Idling;
                    

                    break;
                case SSMPlayerState.Walking:

                    Exec = Walking;

                    break;
                case SSMPlayerState.Running:

                    break;
                case SSMPlayerState.Jumping:

                    break;
                case SSMPlayerState.Falling:

                    break;
            }

            currentState = value;
        }
    }

    #region BASE FUNCTIONS

    public void Init()
    {
        CurrentState = SSMPlayerState.Walking;
    }


    public void Execute()
    {
        CurrentState = Exec(CurrentState);
    }

    

    #endregion

    #region STATES

    public SSMPlayerState Walking(SSMPlayerState state)
    {

        Debug.Log("Walking");

        return ConditionsWalking(state);
    }

    public SSMPlayerState Idling(SSMPlayerState state)
    {

        Debug.Log("Idling");

        return ConditionsIdling(state);
    }

    #endregion

    #region CONDITIONS

    public SSMPlayerState ConditionsWalking(SSMPlayerState state)
    {
        SSMPlayerState returnState = state;

        Debug.Log("Cheching Conditions For Walking");

        return returnState;
    }

    public SSMPlayerState ConditionsIdling(SSMPlayerState state)
    {
        SSMPlayerState returnState = state;

        Debug.Log("Cheching Conditions For Idling");

        return returnState;
    }

    #endregion

}
