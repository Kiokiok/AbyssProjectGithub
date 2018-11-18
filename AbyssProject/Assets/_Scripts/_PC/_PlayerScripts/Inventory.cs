using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int tabSize;
    public GameObject[] tabInventory;
    public GameObject[] tabCells;
    public GameObject invCell;
    public GameObject inventoryHUD;
    public Sprite spriteOther;
    int compteurObjets;
    // Use this for initialization
    void Start ()
    {
        CastInventoryUI();


    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateInventory();

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactable_Objet" && Input.GetKeyDown(KeyCode.E))
        {
            
            PickupObjects(other.gameObject);
            other.gameObject.SetActive(false);

        }
        
        if (other.gameObject.name == "Interrupteur" && Input.GetKeyDown(KeyCode.E))
        {

            EmptyInvObjects("Object_Inventaire_Cube");
            
        }
    }

    //je pick mon objet
    void PickupObjects(GameObject Object)
    {

        //Debug.Log("Objet Ramassé");

        //je parcours mon inventaire
        for (int i = 0; i < tabInventory.Length; i++)
        {
            //je remplis mon inventaire
            if (tabInventory[i] == null)
            {
                tabInventory[i] = Object;
                tabCells[i].transform.GetChild(0).GetComponent<Image>().sprite = spriteOther;
                return;
            }
        }
    }

    //je vide mon inventaire
    void EmptyInvObjects(string Object_Name)
    {
        //je parcours mon inventaire
        for (int i = 0; i < tabInventory.Length; i++)
        {
            if (tabInventory[i].name != null)
            {
                if (tabInventory[i].name == Object_Name)
                {
                    tabInventory[i] = null;
                    tabCells[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                    return;
                }
            }
            else
                return;
            
            
        }
    }


    //je crée l'UI de l'inventaire
    void CastInventoryUI()
    {
        tabInventory = new GameObject[tabSize];
        tabCells = new GameObject[tabSize];

        for (int i = 0; i < tabSize; i++)
        {


            
            tabCells[i] = Instantiate(invCell);
            tabCells[i].transform.parent = inventoryHUD.transform;
            
        }

        //tabCells[0].transform.GetChild(0).GetComponent<Image>().sprite = spriteOther;
    }

    void UpdateInventory()
    {
        for (int j = 0; j < tabSize; j++)
        {
            if (tabInventory[j] != null)
                compteurObjets++;
        }

        if (compteurObjets != 0)
        {
            for (int i = 0; i < tabSize; i++)
            {
                if (tabInventory[i] == null && i < tabSize - 1)
                {
                    tabInventory[i] = tabInventory[i + 1];
                    tabCells[i].transform.GetChild(0).GetComponent<Image>().sprite = tabCells[i + 1].transform.GetChild(0).GetComponent<Image>().sprite;
                    tabInventory[i + 1] = null;
                    tabCells[i + 1].transform.GetChild(0).GetComponent<Image>().sprite = null;
                }
            }
            compteurObjets = 0;
        }
        
    }


}
