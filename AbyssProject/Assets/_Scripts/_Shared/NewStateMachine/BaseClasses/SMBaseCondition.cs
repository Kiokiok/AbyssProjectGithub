using UnityEngine;
using System.Collections;

namespace SM
{

    public class SMBaseCondition<T> where T : DataStorage 
    {
        public SMBaseState<T> TargetState;

        public virtual bool Check(T data)
        {


            return false;
        }

    }


}
