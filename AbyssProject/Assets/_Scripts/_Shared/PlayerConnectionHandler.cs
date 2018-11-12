using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionHandler : NetworkBehaviour {


    public GameObject levelManager;

	// Use this for initialization
	void Start () {

        if (!isLocalPlayer)
            return;

        CmdSpawnLevelNetManager();
		
	}
	

    ////////////////////// COMMANDS
    ///

    [Command]
    void CmdSpawnLevelNetManager()
    {
        // Execute this function server side only
        GameObject go = Instantiate(levelManager);

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

    }
}
