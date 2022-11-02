using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CollisionControl : MonoBehaviour
{
    public GameObject mainobje,newobje,createdobje,cam; // Objects
    public float mainobjeleftedge,newobjeleftedge,edgedifference; // Objects left edge
    public int Score = 1; // Score
    public Text Scoretx; // Score Text
    public List<GameObject> cubs =new List<GameObject>(); // List for Rigidbody


    void Update()
    {
        Scoretx.text = $" Level = {Score - 1}"; // Score Text
        mainobjeleftedge  =  mainobje.transform.position.z - mainobje.transform.localScale.z / 2; // mainobjeleftedge
        newobjeleftedge =  newobje.transform.position.z + newobje.transform.localScale.z / 2; // newobjeleftedge
        edgedifference = newobjeleftedge-mainobjeleftedge; // newobjeleftedge - mainobjeleftedge
        if (Input.GetMouseButtonDown(0))
        {
            if (mainobjeleftedge <= newobjeleftedge) // Collision control
            {
                GetComponent<movement>().speed += Random.RandomRange(0,1f); // add Speed
                mainobje = Instantiate(newobje); // new object 
                mainobje.transform.localScale = new Vector3(5,0.5f,edgedifference); // change mainblock 
                mainobje.transform.position =new Vector3(0,Score * 0.5f,  mainobjeleftedge +edgedifference/2);
                GameObject af = Instantiate(newobje); // Drop Obje
                cubs.Remove(newobje);
                Destroy(newobje); 
                af.transform.localScale = new Vector3(5,0.5f,edgedifference); // Drop Obje
                af.transform.position =new Vector3(0,0.5f, mainobjeleftedge -edgedifference/2); // Drop Obje
                af.AddComponent<Rigidbody>(); // Drop Obje
                newobje =Instantiate(createdobje,new Vector3(0,mainobje.transform.position.y +0.5f,10), Quaternion.identity ); // New Block start position
                newobje.transform.localScale = mainobje.transform.localScale; // New Block scale = mainobje scale
                newobje.SetActive(true);
                Score++; // score add 
                GetComponent<movement>().ChangeObject();
                cubs.Add(mainobje);
                cam.transform.position +=new Vector3(0,0.5f,0);
            }
            else
            {
                GetComponent<movement>().enabled = false;
                newobje.AddComponent<Rigidbody>();
                StartCoroutine(Endgame());
                foreach(var cubs in cubs)
                {
                    cubs.AddComponent<Rigidbody>(); // ADD Rıgıdbody for end game
                }
            }
        }
    }

    IEnumerator Endgame()
    {
        yield return new WaitForSeconds (3f);
        Application.LoadLevel(0);
    }
}
