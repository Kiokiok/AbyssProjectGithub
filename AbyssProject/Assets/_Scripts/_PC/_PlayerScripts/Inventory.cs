using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public int tabSize;
    public GameObject[] tabInventory;
    public GameObject[] tabCells;
    public GameObject invCell;
    public GameObject inventoryHUD;
    public Sprite spriteOther;
    public playerCC Playercc;


    int compteurObjets;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;





    // Use this for initialization
    void Start ()
    {
        CastInventoryUI();

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();


    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateInventory();
        OnUIDisplay();


        //TEST//

        
        /*
        // Check if the mouse was clicked over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    Debug.Log("Hit " + result.gameObject.name);
                }
            }

        
            //Debug.Log("Clicked on the UI");
        }*/


        

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

    void OnUIDisplay()
    {
        if (Playercc.isPause == 1)
        {
            inventoryHUD.SetActive(false);
        }
        else
            inventoryHUD.SetActive(true);
    }


}
