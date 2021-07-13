using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpwardMovement : MonoBehaviour
{
   
    public static Vector3 force;
    [SerializeField] Rigidbody rb;
 
    [SerializeField] float power = 5;
    
    Camera cam;
    Touch touch;
    Vector3 dragStartPos;
   
    Vector3 dragEndPos;
    float camDistance = 0f;
    public static bool isControlEnable = false;

  

    [SerializeField] Transform parent;
    public static Vector3 vel;
    
    [Header("Trajectory Dot")]
    public GameObject trajectoryDot;
    public static GameObject[] trajectoryDots;
    public int dotNo = 12;
    [SerializeField] [Range(0.01f, 0.3f)] float dotMinScale;
    [SerializeField] [Range(0.3f, 1f)] float dotMaxScale;

    private void Awake()
    {
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        trajectoryDots = new GameObject[dotNo];
       
        
    }

  
    private void Update()
    {
       
        if (!isControlEnable) 
        {
            return;
        }

        Drag();
        
    }

    private void Drag()
    {
       
        if ((Input.touchCount > 0))
        {
            touch = Input.GetTouch(0);

            if  (touch.phase == TouchPhase.Began)
            {
                DragStart();
                force = Vector3.zero;

            }
            if  (touch.phase == TouchPhase.Moved) 
            {
                Dragging();
               
            }
            if ((touch.phase == TouchPhase.Ended) )
            {
                DragEnd();

            }
        }
    }

    void DragStart()
    {
        dragStartPos = cam.ScreenToWorldPoint( (touch.position));
        dragStartPos.z = camDistance;

        trajectoryDot.transform.localScale = Vector3.one * dotMaxScale;
      
      
     

    }
    void Dragging()
    {
        dragEndPos = cam.ScreenToWorldPoint(touch.position);
        dragEndPos.z = camDistance;
      
        force = (dragStartPos - dragEndPos);
        
        force.z = 0;

        float scale = dotMaxScale;
        float scaleFactor = scale / dotNo;
        for (int i = 0; i < dotNo; i++)
        {
            if (trajectoryDots[i] == null)
            {
                trajectoryDots[i] = (GameObject)Instantiate(trajectoryDot, transform.position, Quaternion.identity);
            }

            trajectoryDots[i].transform.localScale = Vector3.one * scale;
            if (scale > dotMinScale)
                scale -= scaleFactor;
        }


        for (int i = 0; i < dotNo; i++)
        {
                trajectoryDots[i].transform.position = (Vector3)currenPointPos(i * 0.1f);
        }
    }
    void DragEnd()
    {
       
        rb.velocity = new Vector3(force.x * power, force.y * power, force.z * power);
      
        if (rb.velocity.y >= 25)
        {
            rb.velocity = new Vector3(rb.velocity.x, 25f, rb.velocity.z); 
        }
       

        for ( int i =0; i < dotNo; i++)
        {
            Destroy(trajectoryDots[i]);
            Debug.Log("Destroyed");
        }

        float t;
        t = (-1f * rb.velocity.y) / Physics.gravity.y;
        t = 2f * t;
    }

    Vector3 currenPointPos(float t)
    {
        vel = transform.position + (force* power * t) + 0.5f * Physics.gravity * t * t;
        
        vel.z = 0;
        
        return vel;
    }

}
