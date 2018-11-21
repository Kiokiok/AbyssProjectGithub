using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCC : MonoBehaviour
{

    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float stretchSpeed;
    public float slideSpeed;
    public float jumpHeight;
    public float gravity;
    public float mouseSensitivity;
    public Text pauseGameText;



    float dt;
    float movementSpeed;
    float maxViewX = 60;
    float minViewX = -60f;
    float rotationX;
    float moveH;
    float moveV;
    float normalFloat;
    int isPause = 1; //1 for yes, -1 for no
    bool isCrouch = false;
    bool isStretch = false;
    bool crouchKeyPressed = false;
    bool stretchKeyPressed = false;
    bool canJump = true;
    bool onJump = true;

    GameObject go_camInitPos;
    Vector3 v3_input;
    Vector3 normalGround;
    CharacterController cc;



    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start()
    {
        go_camInitPos = transform.GetChild(0).gameObject;

        movementSpeed = walkSpeed;

        mouseSensitivity *= 50;

        LaunchGame();
    }

    public void LaunchGame()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Camera.main.GetComponent<CameraManager>().canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        if (cc.isGrounded)
        {
            if (onJump)
                onJump = false;

            // S'ACCROUPIR
            if (Input.GetButtonDown("Crouch"))
            {
                crouchKeyPressed = true;
                ChangeCrouch(1);
            }
            else if (Input.GetButtonUp("Crouch"))
                crouchKeyPressed = false;

            // SE COUCHER
             if (Input.GetKeyDown(KeyCode.X))
            {
                stretchKeyPressed = true;
                ChangeCrouch(-1);
            }
            else if (Input.GetKeyUp(KeyCode.X))
                stretchKeyPressed = false;

            if (!stretchKeyPressed && crouchKeyPressed && isStretch) ChangeCrouch(1);
            else if (!stretchKeyPressed & !crouchKeyPressed && (isCrouch || isStretch)) ChangeCrouch(0);
            else if (stretchKeyPressed & !crouchKeyPressed && isCrouch) ChangeCrouch(-1);

            // VITESSE DU MOUVEMENT
            if (Input.GetAxis("Sprint") > 0.1f & !isCrouch & !isStretch && movementSpeed != runSpeed && normalFloat < 20f)
                movementSpeed = runSpeed;
            else if (Input.GetAxis("Sprint") < 0.1f & !isCrouch & !isStretch && movementSpeed == runSpeed)
                movementSpeed = walkSpeed;

            // DIRECTION DU MOUVEMENT
            if (Input.GetAxis("Horizontal") > 0.3f || Input.GetAxis("Horizontal") < -0.3f)
                moveH = Input.GetAxis("Horizontal");
            else moveH = 0f;
            if (Input.GetAxis("Vertical") > 0.3f || Input.GetAxis("Vertical") < -0.3f)
                moveV = Input.GetAxis("Vertical");
            else moveV = 0f;

            /*if (moveH != 0 && Time.timeScale != 0 || moveV != 0 && Time.timeScale != 0)
                transform.localEulerAngles = new Vector3(0f, Camera.main.transform.eulerAngles.y + Mathf.Atan2(moveH, moveV) * 180 / Mathf.PI, 0f);*/

            v3_input = new Vector3(moveH, 0f, moveV);
            v3_input = transform.TransformDirection(v3_input);
            v3_input *= movementSpeed;

            // JUMP
            if (Input.GetAxisRaw("Jump") != 0 &! isStretch & !isCrouch && canJump && isOnGround())
            {
                v3_input.y = jumpHeight;
                onJump = true;
            }
        }

        if(!onJump && isOnGround())
            v3_input.y -= gravity * dt;
        else
            v3_input.y -= gravity / 10 * dt;

        // PAUSE
        if (Input.GetButtonDown("Cancel"))
            PauseGame();

        // MOUVEMENT DE SOURIS
        if (Input.GetAxis("Mouse X") != 0) transform.Rotate(0f, Input.GetAxis("Mouse X") * dt * mouseSensitivity, 0f);
        if (Input.GetAxis("Mouse Y") != 0) CameraView();

        // DETECTION DE DEGRES DE PENTE SOUS NOS PIEDS
        //Debug.Log(normalFloat);
        if (normalFloat >= cc.slopeLimit) movementSpeed = walkSpeed;
        else if (normalFloat <= cc.slopeLimit && normalFloat >= 20f) movementSpeed = walkSpeed;
        else if (Input.GetAxis("Sprint") > 0.1f || Input.GetAxis("Sprint") < -0.1f)
            if (movementSpeed != runSpeed & !isCrouch & !isStretch)
                movementSpeed = runSpeed;
    }




    void FixedUpdate()
    {
        // FAIT GLISSER LE JOUEUR S'IL EST SUR UNE PENTE TROP RAIDE
        if (!canJump)
        {
            if (normalFloat < 85)
            {
                v3_input.x = (1f - normalGround.y) * normalGround.x * slideSpeed;
                v3_input.z = (1f - normalGround.y) * normalGround.z * slideSpeed;
            }
        }

        // DEPLACE LE JOUEUR
        cc.Move(v3_input * dt);
        
        // S'IL EST SUR UNE PENTE TROP RAIDE
        canJump = (normalFloat <= cc.slopeLimit || normalFloat >= 180f - cc.slopeLimit);
    }

    // Fonction de test pour se relever
    bool CanGetUp(float dist)
    {
        float distanceToPoints = 2 / 2 - cc.radius;

        Vector3 point1 = transform.position + cc.center + Vector3.up * distanceToPoints + Vector3.up * (1f - transform.localScale.y);
        Vector3 point2 = transform.position + cc.center - Vector3.up * distanceToPoints + Vector3.up * (1f - transform.localScale.y);

        float radius = cc.radius * 0.95f;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, Vector3.up, dist);

        foreach (RaycastHit obj in hits)
            if (obj.transform.GetComponent<Collider>() && obj.transform.tag != "Player")
                if (obj.transform.GetComponent<Collider>().enabled & !obj.transform.GetComponent<Collider>().isTrigger)
                    return false;

        return true;
    }

    bool isOnGround()
    {
        float distanceToPoints = 2 / 2 - cc.radius;

        Vector3 point1 = transform.position + cc.center + Vector3.up * distanceToPoints;
        Vector3 point2 = transform.position + cc.center - Vector3.up * distanceToPoints;

        float radius = cc.radius * 0.95f;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, Vector3.down, 0.25f);

        foreach (RaycastHit obj in hits)
            if (obj.transform != transform)
                if (obj.transform.GetComponent<Collider>())
                    if (obj.transform.GetComponent<Collider>().enabled &! obj.transform.GetComponent<Collider>().isTrigger)
                        return true;

        return false;
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject)
        {
            normalGround = col.contacts[0].normal;
            normalFloat = Vector3.Angle(Vector3.up, normalGround);

            if (normalFloat < 95 && normalFloat > 85 ||normalFloat == 180)
                normalFloat = 0;
        }
    }

    // Fonction pour s'accroupir / s'allonger
    void ChangeCrouch(float a)
    {
        if (!isStretch && a == 1 || isStretch && a == 1 && CanGetUp(0.2f))
        {
            // S'ACCROUPIR
            transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y - 1f), transform.position.z);
            transform.localScale = new Vector3(1f, 0.70f, 1f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
            movementSpeed = crouchSpeed;
            isCrouch = true;
            isStretch = false;
        }
        else if (a == -1)
        {
            // S'ALLONGER
            transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y - 1f), transform.position.z);
            transform.localScale = new Vector3(1f, 0.5f, 1f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            movementSpeed = stretchSpeed;
            isStretch = true;
            isCrouch = false;
        }
        else if (a == 0)
        {
            // SE RELEVER
            float dist = 0f;
            if (isCrouch) dist = 0.3f;
            if (isStretch) dist = 0.5f;
            
            if (CanGetUp(dist))
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                if (movementSpeed == crouchSpeed) transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                if (movementSpeed == stretchSpeed) transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

                isCrouch = false;
                isStretch = false;

                movementSpeed = walkSpeed;
            }
        }
    }

    // Fonction qui s'occupe des mouvements de camera (1st et 3rd person)
    void CameraView()
    {
        rotationX += Input.GetAxis("Mouse Y") * dt * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, minViewX, maxViewX);
        Camera.main.transform.localEulerAngles = new Vector3(-rotationX, 0f, 0f);
    }

    // Fonction de mise en pause et reprise du jeu
    void PauseGame()
    {
        if (isPause == 1) // MISE DU JEU EN PAUSE
        {
            Cursor.lockState = CursorLockMode.None;
            pauseGameText.transform.localScale = new Vector3(1f, 1f, 1f);
            Time.timeScale = 0;
        }
        else // REPRISE DU JEU
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseGameText.transform.localScale = new Vector3(0f, 0f, 0f);
            Time.timeScale = 1;
        }

        isPause *= -1;
    }
}
