using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{

    private static Record instance;
//public PlayerMovement playermovement;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
       
        
        if (PlayerPrefs.HasKey("RecordTime"))
        {
           
            
           
        }

       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static Record Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<Record>();
                singletonObject.name = "singletonRecord";
                DontDestroyOnLoad(singletonObject);
            }

            return instance;
        }
    }
    public void RecordPrefs(float writeNum)
    {
        if (PlayerPrefs.HasKey("RecordTime"))
        {
            

        }
        
     
        if ((writeNum < PlayerPrefs.GetFloat("RecordTime", 0f)))
        {
            
            PlayerPrefs.SetFloat("RecordTime", writeNum);
           
            
        }
        
    }


   

}
