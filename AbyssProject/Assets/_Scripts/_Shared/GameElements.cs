using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.Networking;

namespace AbyssObj
{
    [System.Serializable]
    public struct levelBlock
    {
        public GameObject currentDisplay;

        public string modelName;

        public int Index;

        public Vector3 position;

        public Quaternion rota;

        public blockType type;



    }

    public struct NetChangeObserver
    {
        


    }


    //
    //  ==> Element envoyé à travers  le network pour transmettre des valeurs
    //
    [System.Serializable]
    public struct networkElement
    {
        public Vector3 pos;

        public int index;

        public Quaternion rot;

        public bool active;

        

        public networkElement(Vector3 vec, int i, Quaternion quat, bool act)
        {
            pos = vec;
            index = i;
            rot = quat;
            active = act;  
        }

    }


    public enum blockType
    {
        Static =0,
        Movable=1,
        Rotatable=2,
        Booleanble=3,
    }

    public enum NetworkedType
    {
        Sender =0,
        Receiver =1,
        TwoWay = 2,

    }


    public enum platform
    {
       PC,
       AR,
       

    }

    public enum CustomEventType
    {
        CalledOnTrue,
        CalledOnFalse,
        CalledOnChange


    }

    public enum Axis
    {
        X,Y,Z

    }

    public enum BoolType
    {
        Unique,
        Multiple,
        Timed
    }

    public enum MovementType
    {
        Once,
        Multiple
    }

    public enum ActivatedType
    {
        StopsWhenDeactivated,
        ChangeDirections,


    }

    public enum RotationType
    {
        Servo,
        Loop


    }

    public enum EnnemyState
    {
        Idle,
        Charging,
        Rushing,
        Moving

    }

    public static class DataPlayer
    {

        public static Vector3[] testFloorPos = new Vector3[5]
        {   new Vector3(0,0.15f,0.5f),
            new Vector3(0.5f,0.15f,0),
            new Vector3(-0.5f,0.15f,0),
            new Vector3(0,0.15f,-0.5f),
            new Vector3(0,0.15f,0)
        };

    }






}