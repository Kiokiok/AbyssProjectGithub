using UnityEngine;
using System.Collections;
using AbyssObj;


public class LevelStore : MonoBehaviour
{
    public platform platform;

    public GameObject[] levelElements;

    public LevelManagerNetwork lNet;

    // Liste principale des éléments networkés
    public networkElement[] elements;

    private void Start()
    {
        elements = new networkElement[2];
    }


    //
    // Partie Network
    //

    public void UpdateFromNetValue(networkElement nl)
    {
        elements[nl.index] = nl;
    }

    //
    // Partie local
    //

    public void ChangeValue(int index, bool value)
    {
        elements[index].Active = value;

            if (platform == platform.AR)
                {
                    lNet.CmdUpdateValuesOnServerAR(elements[index]);
                }
            else if (platform == platform.PC)
                {
                    lNet.CmdUpdateValuesOnServerPC(elements[index]);
                }

    }

    public void ChangeValue(int index, Vector3 value)
    {
        elements[index].pos = value;
    }

    public void ChangeValue(int index, Quaternion value)
    {
        elements[index].rot = value;
    }


}
