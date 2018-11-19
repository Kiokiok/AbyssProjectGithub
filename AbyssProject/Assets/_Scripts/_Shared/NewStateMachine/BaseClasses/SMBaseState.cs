using UnityEngine;
using System.Collections;


namespace SM
{
    public class SMBaseState<T> where T : DataStorage 
    {

        public SMBaseCondition<T>[] Conditions;

        public SMBaseAction<T>[] OnEnterActions;
        public SMBaseAction<T>[] Actions;
        public SMBaseAction<T>[] FixedActions;
        public SMBaseAction<T>[] OnExitActions;

        public virtual SMBaseState<T> CheckConditions(T data)
        {
            foreach(SMBaseCondition<T> cond in Conditions)
            {
                if(cond.Check(data))
                {
                    return cond.TargetState;
                }

            }

            return this;

        }


        public virtual void Execute(T data)
        {
            foreach(SMBaseAction<T> ac in Actions)
            {
                ac.Execute(data);

            }


        }

        public virtual void Enter(T data)
        {
            foreach (SMBaseAction<T> ac in OnEnterActions)
            {
                ac.Execute(data);

            }


        }

        public virtual void Exit(T data)
        {
            foreach (SMBaseAction<T> ac in OnExitActions)
            {
                ac.Execute(data);

            }


        }

        public virtual void FixedExecute(T data)
        {
            foreach (SMBaseAction<T> ac in FixedActions)
            {
                ac.Execute(data);

            }


        }

    }



}

