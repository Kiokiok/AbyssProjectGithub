using UnityEngine;
using System.Collections;
using AbyssObj;

// La classe pricipale du niveau
// C'est elle qui stocke les divers éléments interactifs du niveau
// Et gère les échanges en networking entre les instances du jeu 
public class LevelStore : MonoBehaviour
{

    // SUPER IMPORTANT
    // => Détermine qui envoie et reçoit quelles valeurs en network
    // Il faut juste le set, mais il faut bien faire attention à ce qu'il qoit bien set
    //
    public platform platform;


    // UNUSED
    [HideInInspector]
    public GameObject[] levelElements;

    // Les valeurs que l'on veut networker
    public BaseNetworkElement[] AllNetworkElements;

    [Space(15f)]


    // Accès au script qui gère le networking
    // Il se set automatiquement
    [HideInInspector]
    public LevelManagerNetwork lNet;

    // Liste principale des éléments networkés
    //public networkElement[] elements;

    private void Start()
    {


    }


    //
    // Partie Network
    //

        // Reçoit un changement de valeur depuis le serveur
    public void UpdateFromNetValue(networkElement nl)
    {
        //elements[nl.index] = nl;

        AllNetworkElements[nl.index].ElemValue = nl.active;
    }

    //
    // Partie local
    //

        // Appelé depuis un script exterieur
        // Envoie un changement de valeur sur un bool, à l'index précisé
    public void ChangeValue(int index, bool value)
    {
        AllNetworkElements[index].ChangeVal(value);

            if (platform == platform.AR)
                {
                    lNet.CmdUpdateValuesOnServerAR(createNetElement(index, value));
                }
            else if (platform == platform.PC)
                {
                    lNet.CmdUpdateValuesOnServerPC(createNetElement(index, value));
                }

    }


    
    private networkElement createNetElement(int index, bool value)
    {
        return new networkElement(Vector3.zero, index, Quaternion.identity, value); ;
    }



    /*
    public void ChangeValue(int index, Vector3 value)
    {
        elements[index].pos = value;
    }

    public void ChangeValue(int index, Quaternion value)
    {
        elements[index].rot = value;
    }

    */
}
