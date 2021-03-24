using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HomeLoading : MonoBehaviour
{
	private Touch touch;
	private GameObject localObject;
    
    void Start () {
        
    }

	void Update () {
        if (Input.touchCount == 1) {
            touch = Input.GetTouch (0);
            if (touch.tapCount == 2) {
                localObject = Helper.GetObjectOnTouchByTag(touch.position, ObjectTag.organTag);
                if (localObject != null)
                {
                    string nameOrgan = localObject.tag;
                    if (localObject.tag == ObjectTag.organTag) {
                        PlayerPrefs.SetString("nameOrgan", localObject.name);
                        PlayerPrefs.Save();
                        SceneManager.LoadScene(SceneConfig.organDetailScene);
                    }
                }
            }
        } else {
            return;
        }
    } 
}
