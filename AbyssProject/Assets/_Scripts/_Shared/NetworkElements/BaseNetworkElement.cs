using UnityEngine;
using System.Collections;
using AbyssObj;


// Un Base Network Element est un élément commun aux deux instances du jeu
// Ils permettent d'envoyer et syncroniser des valeurs entre les deux instances
// Quand une valeur change, un event est envoyé sur tout les EventBase Listés pour cette valeur


[System.Serializable]
public class BaseNetworkElement
{

    // Constructeur
    public BaseNetworkElement()
    {


    }

    // Nom de l'élément
    public string name;

    // Quand est ce que l'event est appelé ? 
    public CustomEventType TypeEvent = CustomEventType.CalledOnTrue;

    // Est ce que ce script est sender ou receiver ?
    // ==> Si receiver les events sont activés, sinon non
    public NetworkedType TypeNetwork = NetworkedType.Receiver;


    // La liste d'events appelé quand la valeur change
    // En général on en apelle qu'un seul, mais il est possible d'en appeler plusieurs avec
    // un meme changement de valeur
    public EventBase[] ElementEvents;

    // Le valeur de cet element
    private bool elemValue;

    // La property qui change la valeur, en plus d'apeller les events
    //
    public bool ElemValue
    {
        get { return elemValue; }
        set
        {
            // Change la valeur de l'élément
            elemValue = value;

            // Apelle les events
            if(TypeNetwork == NetworkedType.Receiver || TypeNetwork == NetworkedType.TwoWay)
            {
                if (TypeEvent == CustomEventType.CalledOnTrue && value)
                {
                    callEvents();
                }
                else if (TypeEvent == CustomEventType.CalledOnFalse && !value)
                {
                    callEvents();
                }
                else if (TypeEvent == CustomEventType.CalledOnChange)
                {
                    callEvents();
                }
            }
        }
    }

    // La fonction qui apelle les events
    private void callEvents()
    {
        Debug.Log(name);

        foreach(EventBase ev in ElementEvents)
        {
            ev.EventStart();
        }

    }

    public void ChangeVal(bool value)
    {
        elemValue = value;
    }
   
}
