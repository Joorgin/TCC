using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

    public GameObject MainCamera;
    private float lenght, startPos;
    public float speedParalax;
   
   
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        MainCamera = GameObject.Find("MainCamera");
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (MainCamera.transform.position.x * (1f - speedParalax));
        float dist = (MainCamera.transform.position.x * speedParalax);
   
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
   
        if (temp > startPos + lenght)
        {
            startPos += lenght;
        }
        else if(temp < startPos - lenght)
        {
            startPos -= lenght;
        }
    }
}
