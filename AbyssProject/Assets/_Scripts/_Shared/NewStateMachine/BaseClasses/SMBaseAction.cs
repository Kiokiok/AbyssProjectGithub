using UnityEngine;
using System.Collections;

namespace SM
{
    public class SMBaseAction<T> where T : DataStorage
    {

        public virtual void Execute(T data)
        {



        }
        
    }



}

