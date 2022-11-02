using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class movement : MonoBehaviour
{
    public Color[] color;
    public Material mat;
    public GameObject MovementObject,Aimobject;
    public bool sag;

    public float speed;
    public void ChangeObject()
    {
        MovementObject = GetComponent<CollisionControl>().newobje;
        Aimobject = GetComponent<CollisionControl>().mainobje;
        mat.color = color[Random.RandomRange(0,6)];
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Aimobject.transform.position.z - MovementObject.transform.position.z) <= 0.1f )
        {   
            sag = false;
        }
        if (Mathf.Abs(Aimobject.transform.position.z - MovementObject.transform.position.z) >= 6 )
        {
           sag = true;
        }
        if (sag)
        {
            MovementObject.transform.position = Vector3.MoveTowards(MovementObject.transform.position, new Vector3(MovementObject.transform.position.x ,MovementObject.transform.position.y ,Aimobject.transform.position.z), speed *Time.deltaTime);
        }
        else
        {
            MovementObject.transform.position = Vector3.MoveTowards(MovementObject.transform.position, new Vector3(MovementObject.transform.position.x ,MovementObject.transform.position.y ,MovementObject.transform.position.z-10), speed *Time.deltaTime);
        }
    }
}
