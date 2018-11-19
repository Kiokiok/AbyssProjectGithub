using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StateMachineBase<T> where T :Data
{
    

    public T data;

    // Le state actuel
   
    public State currentState;

    State newState;

    // Drag n Drop container
    // Les states utilisés par cette machine
    public State[] MachineStates;

    [HideInInspector]
    public List<string> conditionsNames;
    
    public Dictionary<string, State> MachineStatesDic = new Dictionary<string, State>();
    public Dictionary<string, Condition> Conditions = new Dictionary<string, Condition>();
    public Dictionary<string, Actions> AllActions = new Dictionary<string, Actions>();


    

    public void ChangeState(State newState)
    {
        

        if (currentState != null)
            currentState.Exit(data);

        currentState = newState;
        currentState.Enter(data);

        newState = null;
    }


    public void Update()
    {
        if (currentState != null)
        {
            newState = currentState.CheckConditions(data);

            if (newState != currentState)
                ChangeState(newState);

            currentState.Execute(data);

            
        }
    }

    public void FixedExecute()
    {

        if (currentState != null)
            currentState.FixedExecute(data);

    }

    public void Init()
    {      

        // Créé des Scriptables objects indépendants
        // Prend les scriptables objects drag n droppés et en fait une copie
        // Cela permet que tous les éléments ( States et Conditions ) se réfèrent toute au meme script*
        //
        // D'abord pour les states
        //
       for(int i = 0; i < MachineStates.Length; i++)
       {
            MachineStatesDic.Add(MachineStates[i].Name, GameObject.Instantiate(MachineStates[i]));


       }

        //Puis pour les conditions
        //

        getConditionsFromStates();
      
       
       // Ensuite les copies sont re-liées 
       //
       // D'abord les targetStates des conditions
       //
       foreach(var Cond in Conditions)
       {
            MachineStatesDic.TryGetValue(Cond.Value.targetState.Name, out Cond.Value.targetState);
            //Cond.Value.ObjectData = data;
            //Cond.Value.parent = this;

       }

       //Puis les Conditions
       //
        foreach (var Sta in MachineStatesDic)
        {
           //Sta.Value.parent = this;
           

            for(int i = 0; i < Sta.Value.stateConditions.Length; i++)
            {
                Conditions.TryGetValue(Sta.Value.stateConditions[i].Name, out Sta.Value.stateConditions[i]);
            }
        }


        SetData();

        

    }

    public virtual void SetData()
    {
        foreach(var sta in MachineStatesDic)
        {
           

        }

    }

    private void getConditionsFromStates()
    {
        conditionsNames = new List<string>();

        foreach(State sta in MachineStates)
        {
            foreach(Condition cond in sta.stateConditions)
            {
                if(!Conditions.ContainsKey(cond.Name))
                {
                    Conditions.Add(cond.Name, GameObject.Instantiate(cond));
                    conditionsNames.Add(cond.Name);
                }

            }

        }


    }


    
}
