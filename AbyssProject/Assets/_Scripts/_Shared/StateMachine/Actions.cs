using UnityEngine;
using System.Collections;

[System.Serializable]
public class Actions : ScriptableObject
{

    

    public string Name;

    public virtual void Execute<F>(F data) where F : Data
    {



    }


}
