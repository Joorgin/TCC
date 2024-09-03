using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

    public GameObject MainCamera;
    public Transform cam;
    private float lenght, startPos;
    public float speedParalax;
   
   
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        MainCamera = GameObject.Find("MainCamera");

        cam = MainCamera.transform;
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        float rePOs = cam.transform.position.x * (1 - speedParalax);
        float dist = (cam.transform.position.x * speedParalax);
        transform.position = new Vector3(startPos + dist ,transform.position.y, transform.position.z);

        if (rePOs > startPos + lenght) startPos += lenght;
        else if(rePOs < startPos - lenght) startPos -= lenght;
    }
}
