using UnityEngine;
using System.Collections;
using AbyssObj;
using UnityEngine.UI;


//Classe de test
// Permet de debug le network
public class ButtonUpdater : MonoBehaviour
{

    public LevelStore storage;

    

    bool val = false;

    public Text tex;

    // Use this for initialization
    void Start()
    {

        
      
    }

    // Update is called once per frame
    void Update()
    {

        tex.text = storage.elements[0].active.ToString();

    }


    public void ChangeValue()
    {
        val = !val;
        storage.ChangeValue(0, val);


    }
}
