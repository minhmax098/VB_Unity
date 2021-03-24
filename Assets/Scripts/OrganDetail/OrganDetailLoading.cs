using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems; 

public class OrganDetailLoading : MonoBehaviour
{
    string currentOrganName; 
    GameObject currentOrganPreference; 
    Organ dataOrgan; 
    public static List<Organ> listSubOrgans = new List<Organ>();

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("nameOrgan"))
        {
            string nameOrgan = PlayerPrefs.GetString("nameOrgan"); 
            GameObject currentOrgan = Resources.Load(nameOrgan) as GameObject; 
            if (currentOrgan != null)
            {
                currentOrganPreference = Instantiate(currentOrgan); 
                Organ fakeDataOrgan = new Organ(10, "Skeletal", 1, "Skeletal Paragraph are the building blocks", 0, 0); 
                OrganManager.InitOrgan(nameOrgan, currentOrganPreference, fakeDataOrgan, false, false); 
                
            }
        }
        
        else 
        {
            Debug.Log("error");
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
