using UnityEngine;
using UnityEditor;

namespace SM
{

    [System.Serializable]
    public class SMPlayer : SMBase<PlayerStorageData> 
    {

        

        public SMBaseState<PlayerStorageData> CurrentState;

        public SMBaseState<PlayerStorageData>[] States;

        public void Execute()
        {

            CurrentState = CurrentState.CheckConditions(storedData);

            CurrentState.Execute(storedData);
            

        }

        public void Init()
        {
            storedData = GameObject.Instantiate<PlayerStorageData>(storedData);
        }



    }


}


