using UnityEngine;
using System.Collections;

public class SSMRunner : MonoBehaviour
{
    public SSMBase Base = new SSMBase();

    // Use this for initialization
    void Start()
    {
        Base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        Base.Execute();
    }
}
