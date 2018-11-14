using UnityEngine;
using System.Collections;
using AbyssObj;


// Un Base Network Element est un élément commun aux deux instances du jeu
// Ils permettent d'envoyer et syncroniser des valeurs entre les deux instances
// Quand une valeur change, un event est envoyé sur tout les EventBase Listés pour cette valeur


[System.Serializable]
public class BaseNetworkElement
{
    public BaseNetworkElement()
    {


    }

    // Nom de l'élément
    public string name;

    public CustomEventType TypeEvent = CustomEventType.CalledOnTrue;

    public EventBase[] ElementEvents;


    private bool elemValue;

    public bool ElemValue
    {
        get { return elemValue; }
        set
        {
            elemValue = value;

            if(TypeEvent == CustomEventType.CalledOnTrue && value)
            {
                callEvents();
            }
            else if (TypeEvent == CustomEventType.CalledOnFalse && !value)
            {
                callEvents();
            }
            else if(TypeEvent == CustomEventType.CalledOnChange)
            {
                callEvents();
            }

        }


    }

    private void callEvents()
    {
        foreach(EventBase ev in ElementEvents)
        {
            ev.EventStart();
        }

    }
   
}
