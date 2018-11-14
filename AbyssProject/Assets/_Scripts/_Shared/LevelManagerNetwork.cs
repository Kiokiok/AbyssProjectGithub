using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using AbyssObj;

public class LevelManagerNetwork : NetworkBehaviour {


    //------CLIENT VARIABLES-------
    public LevelStore storage;

    public platform platform;


    //------SERVER AND CLIENT VARIABLES-------
  

    public List<networkElement> ObjectsPC = new List<networkElement>();

    public List<networkElement> ObjectsAR = new List<networkElement>();

    //------SERVER VARIABLES--------

    public LevelManagerNetwork otherLevNet;

    //private List<NetworkInstanceId> AllIdsOnserver = new List<NetworkInstanceId>();

    
    //---------END-----------

    private void Start()
    {

        
        //Debug.Log("PRE RETURN");

        if (!hasAuthority)
            return;
        if (isClient)
            CmdSERVERDEBUGLOG(netId);

       

        //Debug.Log("INIT LEVEL MANAGER");

    }

    
    void FindStorageObject()
    {
        Debug.Log("Finding Level Storage Object");

        storage = GameObject.Find("LevelStorage").GetComponent<LevelStore>();

        storage.lNet = this;

        platform = storage.platform;

        if(platform == platform.PC)
        {
            CmdSetPlaformForServer("PC");
        }
        else if (platform == platform.AR)
        {
            CmdSetPlaformForServer("AR");
        }
        foreach (GameObject go in storage.levelElements)
        {
            //go.GetComponent<LevelComponent>().lNet = this;

        }

    }

    
    [Command]
    void CmdSERVERDEBUGLOG(NetworkInstanceId netID)
    {
        Debug.Log(netId.ToString() + "Connected");
    }

    [Command]
    void CmdSetPlaformForServer(string plat)
    {
        
        if(plat.Equals("PC"))
        {
            platform = platform.PC;
        }
        else if(plat.Equals("AR"))
        {
            platform = platform.AR;
        }

    }

    [ClientRpc]
    void RpcClientDEBUGLOG(string message)
    {
       // Debug.Log(message + " -- " + netId);


    }


    [Command]
    public void CmdUpdateValuesOnServerAR(networkElement elem)
    {
        //Debug.Log("UPDATE DES VALUES AR SUR SERVEUR");
        ObjectsAR.Add(elem);

        //platform = platform.AR;
    }

    [Command]
    public void CmdUpdateValuesOnServerPC(networkElement elem)
    {
        //Debug.Log("UPDATE DES VALUES PC SUR SERVEUR");

        ObjectsPC.Add(elem);

        //platform = platform.PC;
    }

    



    private void Update()
    {

        // Server functions
        if(isServer && hasAuthority && storage)
        {
            

        }

        // Cherche l'autre storage
        if (isServer && otherLevNet == null)
            FindOtherLevNet();

        // Echange les valeurs entre serveur et client
        if (isServer && otherLevNet)
            ExchangeValues();


        // Si on est el serveur et on a trouvé l'autre storage
        //
        if(isServer && otherLevNet)
        {
           // Debug.Log("Objects PC Amount = " + ObjectsPC.Count);
            // Debug.Log("Objects AR Amount = " + ObjectsAR.Count);

            if(platform == platform.AR)
            {
                foreach (networkElement netEl in ObjectsPC)
                {
                    RpcMoveElementOnAR(netEl);
                }

                //if(otherLevNet.ObjectsPC.Count > 0)
                    otherLevNet.ObjectsPC.Clear();
            }
            else if (platform == platform.PC)
            {
                foreach (networkElement netEl in ObjectsAR)
                {
                    RpcMoveElementOnPC(netEl);
                }
               //if (otherLevNet.ObjectsAR.Count > 0)
                    otherLevNet.ObjectsAR.Clear();
            }


            //RpcMoveElements(ObjectsAR, ObjectsPC);
            //ObjectsAR.Clear();
           // ObjectsPC.Clear();
        }

        if (!hasAuthority || !isClient)
            return;
                  

        ////////////////////////
        // Client functions
        ///////////////////////
        ///

        if (storage == null && isClient)
            FindStorageObject();

    }

    [ClientRpc]
    public void RpcMoveElementOnAR(networkElement nl)
    {

        if (!storage) return;
        //Debug.Log("____________CALLED AR ELEMENT ON ENTITY = " + netId);
        //storage.levelElements[nl.index].GetComponent<LevelComponent>().Syncronizer(nl);
        storage.UpdateFromNetValue(nl);
    }

    [ClientRpc]
    public void RpcMoveElementOnPC(networkElement nl)
    {

        if (!storage) return;
        //Debug.Log("____________CALLED PC ELEMENT ON ENTITY = " + netId);
        //storage.levelElements[nl.index].GetComponent<LevelComponent>().Syncronizer(nl);
        storage.UpdateFromNetValue(nl);
    }

    // Sert a rien ????
    //
    //

    // Appelé sur le client
    // Effectue le changement de valeur


    /*
[ClientRpc]
public void RpcMoveElements(SyncListObjects objAR, SyncListObjects objPC)
{

    // Si on est sur l'AR, on envoie les changements effectués sur PC
    // Et inversement
    if (platform == platform.AR)
    {
        Debug.Log("PLATFORM AR UPDATE = " + objPC.Count);

        foreach (networkElement nl in objPC)
        {
            Debug.Log("MOVED");
            storage.levelElements[nl.index].transform.position = nl.pos;

        }


        //ObjectsPC.Clear();
    }
    else if (platform == platform.PC)
    {

        Debug.Log("PLATFORM PC UPDATE = " + objPC.Count);
        foreach (networkElement nl in objAR)
        {
            Debug.Log("MOVED");
            storage.levelElements[nl.index].transform.position = nl.pos;

        }

        //ObjectsAR.Clear();
    }

}*/

    // Trouve l'autre netWorkManager
    void FindOtherLevNet()
    {

        LevelManagerNetwork temp = null;
        foreach(NetworkInstanceId id in NetworkServer.objects.Keys)
        {
            temp = NetworkServer.FindLocalObject(id).GetComponent<LevelManagerNetwork>();
            if ( temp != this && temp != null)
            {
                otherLevNet = temp;
                RpcClientDEBUGLOG("Found other levelManNet : " + otherLevNet.name);
            }
        }
    }
    


    // Effectue le tranfert des valeurs sur le serveur
    // Les valeurs changées depuis l'AR passent sur le PC et inversement
    void ExchangeValues()
    {
        if(platform == platform.AR)
        { 
            otherLevNet.ObjectsAR = ObjectsAR;
        }
        else if (platform == platform.PC)
        {
            otherLevNet.ObjectsPC = ObjectsPC; 
        }



    }


}
