using UnityEngine;
using System.Collections;
using AbyssObj;
using UnityEngine.UI;

public class ButtonUpdater : MonoBehaviour
{

    public LevelStore storage;


    public Text tex;

    // Use this for initialization
    void Start()
    {

        
        tex.text = storage.elements[0].active.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        

    }


    public void ChangeValue()
    {

        storage.ChangeValue(0, true);


    }
}
