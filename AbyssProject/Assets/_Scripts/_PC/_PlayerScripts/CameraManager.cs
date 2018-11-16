using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {


    [Header("Objets à Drag'n'Drop")]
    [Space(5f)]
	// La camera est enfant de ce GameObject, qui est lui meme enfant du player ( au niveau de sa tete )
    public Transform cameraOrigin;
	
	//Permet de translate le player en fonction de la rotation de la camera
    public Transform playerDir;

	
	// La camera
    public Camera mainCam;

	[Space(10f)]
    [Header("Variables d'options")]

    public float rotationSpeedHorizontal = 2.0f;
    public float rotationSpeedVertical = 2.0f;

    public bool isCameraSmoothed = false;
    public float cameraSmooth = 4f;

    [HideInInspector]
    public bool canMove = false;




    // The minimum distance of the camera from its origin
    [Range(0f, 20f)]
    public float minDistance = 2.75f;
    //The maximum distance of the camera from its origin
    [Range(0f, 20f)]
    public float maxDistance = 13f;

    private float yaw = 0f;
    private float pitch = 0f;

    private float currentDistance = 0f;

    private Vector3 wallOffset;

    private RaycastHit[] sphereHits;

    private float oldpitch;

    [Range(0f,1f)]
    public float wallOffsetAmount = 0.5f;




	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        cameraOrigin.position = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up * 0.8f;


        changeRotation();
		checkForObstacle();
	}


    // perform the camera movement last after everything has moved and been calculated
    private void LateUpdate()
    {
        setCameraBack();
    }

    //First we change the camera rotation according to mouse input
    private void changeRotation()
    {
        if (canMove)
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeedHorizontal;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeedVertical;
        }

        if (pitch > 83 || pitch < -65) pitch = oldpitch;
       

        cameraOrigin.eulerAngles = new Vector3(pitch, yaw, 0f);

        //playerDir.eulerAngles = new Vector3(0, yaw, 0);

        oldpitch = pitch;
    }

    // We then check if there is an obstale in the way of the camera
    private void checkForObstacle()
    {
        RaycastHit hit;

        currentDistance = maxDistance;

        wallOffset = Vector3.zero;

        Ray mainRay = new Ray(cameraOrigin.position, -cameraOrigin.forward);

        if (Physics.Raycast(mainRay, out hit, maxDistance))
        {
            if (hit.transform.tag != "Player")
            {
                currentDistance = hit.distance;

                wallOffset = hit.normal * 0.5f;
            }
        }
    }


    private void setCameraBack()
    {
        Vector3 oldcampos = mainCam.transform.position;
        Quaternion oldcamrot = mainCam.transform.rotation;

        mainCam.transform.position = cameraOrigin.position;
        mainCam.transform.rotation = Quaternion.Lerp(oldcamrot, cameraOrigin.rotation, cameraSmooth * Time.deltaTime);
        

        mainCam.transform.Translate(Vector3.back * currentDistance, Space.Self);

        float dist = Vector3.Distance(oldcampos, mainCam.transform.position + wallOffset);

        mainCam.transform.position += wallOffset; 

        if (isCameraSmoothed)
            mainCam.transform.position = Vector3.Lerp(oldcampos, mainCam.transform.position + wallOffset, cameraSmooth * dist * Time.deltaTime);

        
        wallOffset = Vector3.zero;
    }


}
