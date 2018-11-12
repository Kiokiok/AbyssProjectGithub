using UnityEngine;
using AbyssObj;

public class LevelComponent : MonoBehaviour {

    public int index;

    public blockType Type;

    public platform platform;

    public InteractType InteractType;

    public string modelUsed;

    
    public LevelManagerNetwork lNet;

    private Vector3 lastPos;

    private Quaternion lastRot;

    public bool IsActivated;

    private bool lastActiveValue;

    private networkElement netEl;

    

    [Space(15f)]

    public bool LockXAxis;
    public bool LockYAxis;
    public bool LockZAxis;

    

    public void Start()
    {
        netEl.index = index;

        netEl.pos = transform.position;
        netEl.rot = transform.rotation;

        lastRot = transform.rotation;
        lastPos = transform.position;
        lastActiveValue = IsActivated;
    }

    

    public void Update()
    {
        if (!lNet) return;

        if(Type == blockType.Movable)
        {
            if(InteractType == InteractType.Sender)
            {
                SendValues();
                lastPos = transform.position;
            }             
        }
        else if(Type == blockType.Rotatable)
        {
            if (InteractType == InteractType.Sender)
            {
                SendValues();
                lastRot = transform.rotation;
            } 
        }
        else if (Type == blockType.Booleanble)
        {
            if (InteractType == InteractType.Sender)
            {
                SendValues();
                lastActiveValue = IsActivated;
            }
        }
    }


       

    public bool testChangePos()
    {
        if (lastPos != transform.position)
            return true;
        else
            return false;

    }

    public bool testChangeRot()
    {
        if (lastRot != transform.rotation)
            return true;
        else
            return false;

    }

    public bool testChangeBool()
    {
        if (lastActiveValue != IsActivated)
            return true;
        else
            return false;

    }


    public void Syncronizer(networkElement nl)
    {
        if(Type == blockType.Movable)
        {
            Vector3 oldPos = transform.position;
            if(!LockXAxis)
                oldPos.x = nl.pos.x;
            if (!LockYAxis)
                oldPos.y = nl.pos.y;
            if (!LockZAxis)
                oldPos.z = nl.pos.z;

            transform.position = oldPos;

        }
        else if(Type == blockType.Rotatable)
        {
            transform.rotation = nl.rot;

        }
        else if (Type == blockType.Booleanble)
        {
            IsActivated = nl.Active;

        }



    }


    
    private void SendValues()
    {
        if (testChangePos())
        {
            netEl.pos = transform.position;

            if (platform == platform.AR)
            {

                lNet.CmdUpdateValuesOnServerAR(netEl);

            }
            else if (platform == platform.PC)
            {
                lNet.CmdUpdateValuesOnServerPC(netEl);
            }

        }
        else if (testChangeRot())
        {
            netEl.rot = transform.rotation;

            if (platform == platform.AR)
            {

                lNet.CmdUpdateValuesOnServerAR(netEl);

            }
            else if (platform == platform.PC)
            {
                lNet.CmdUpdateValuesOnServerPC(netEl);
            }

        }
        else if (testChangeBool())
        {
            netEl.Active = IsActivated;

            if (platform == platform.AR)
            {

                lNet.CmdUpdateValuesOnServerAR(netEl);

            }
            else if (platform == platform.PC)
            {
                lNet.CmdUpdateValuesOnServerPC(netEl);
            }

        }
    }

}
