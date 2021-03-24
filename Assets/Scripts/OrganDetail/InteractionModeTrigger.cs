using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InteractionModeTrigger : MonoBehaviour
{
    private GameObject listSubOrgan;
    private GameObject btnHome;
    private GameObject btnMove;
    private GameObject btnRotate;
    private GameObject btnInfo;
    private GameObject btnSound;
    private GameObject btnTag;
    private GameObject btnExit;
    private GameObject btnAR;
    private GameObject btnQuiz;
    private GameObject btnExitInfo;
    private GameObject btnMenu;
    private GameObject btnDraw;
    private GameObject btnGuide;
    private GameObject btnSetting;

    private GameObject panelInfo;
    private GameObject panelMenu;
    private GameObject mainView;


    private bool isPress = false;
    private bool isPressBtnInfo = false;
    private bool isOpenMenu = false;
    private bool isListSubNameOrgan = false;

    Animator ani;

    void Start() {
        initGUI();
        onClick();
        displayGUI();
        loadItemNameOrganSystem();
    }

    void Update() {
        if (OrganManager.IsRotating)
        {
            GameObject.Find("Gyroscope").transform.eulerAngles = new Vector3(
                                GameObject.Find("Gyroscope").transform.eulerAngles[0],
								GameObject.Find("Gyroscope").transform.eulerAngles[1] + 0.2f, 
								GameObject.Find("Gyroscope").transform.eulerAngles[2]);
        }

        if (TagHandler.Instance.addedTags.Count > 0)
        {
            TagHandler.Instance.OnMove();
        }
    }

    void initGUI() {
        btnHome = GameObject.Find("Home");
        btnMove = GameObject.Find("Move");
        btnRotate = GameObject.Find("Rotate");
        btnTag = GameObject.Find("Tag");
        btnExit = GameObject.Find("Exit");
        btnSound = GameObject.Find("Sound");
        btnInfo = GameObject.Find("Info");
        btnAR = GameObject.Find("AR");
        btnQuiz = GameObject.Find("Quiz");
        btnExitInfo = GameObject.Find("ExitInfo");
        btnMenu = GameObject.Find("btnMenu");
        btnDraw = GameObject.Find("btnDraw");
        btnGuide = GameObject.Find("btnGuide");
        btnSetting = GameObject.Find("btnSetting");
        
        mainView = GameObject.Find("MainView");
        panelInfo = GameObject.Find("PanelInfo");
        panelMenu = GameObject.Find("PanelMenu");

        listSubOrgan = GameObject.Find("ListSubOrgan");
    }

    void displayGUI() {
        listSubOrgan.SetActive(false);
        panelMenu.SetActive(false);
    }

    void onClick() {
        btnHome.GetComponent<Button>().onClick.AddListener(backHome);
        btnMove.GetComponent<Button>().onClick.AddListener(initMove);
        btnRotate.GetComponent<Button>().onClick.AddListener(toggleRotate);
        btnTag.GetComponent<Button>().onClick.AddListener(showTag);
        btnExit.GetComponent<Button>().onClick.AddListener(exitListSubOrgan);
        btnSound.GetComponent<Button>().onClick.AddListener(onSound);
        btnInfo.GetComponent<Button>().onClick.AddListener(showInfo);
        btnAR.GetComponent<Button>().onClick.AddListener(loadSceneARView);
        btnQuiz.GetComponent<Button>().onClick.AddListener(loadSceneQuiz);
        btnExitInfo.GetComponent<Button>().onClick.AddListener(showInfo);
        btnMenu.GetComponent<Button>().onClick.AddListener(showMenu);
        btnDraw.GetComponent<Button>().onClick.AddListener(drawNote);
        btnGuide.GetComponent<Button>().onClick.AddListener(showInfoGuide);
        btnSetting.GetComponent<Button>().onClick.AddListener(showSetting);
    }

    // detail handle UI functions

    void backHome() {
        SceneManager.LoadScene(SceneConfig.homeScene);
	}

    void initMove() {
        OrganManager.IsMoving = !OrganManager.IsMoving;
        if (OrganManager.IsMoving) 
        {
            btnMove.GetComponent<Image>().color = new Color32(111, 229, 223, 255);
        }
        else
        {
            btnMove.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
	}

    void toggleRotate() {
        OrganManager.IsRotating = !OrganManager.IsRotating;
        if (OrganManager.IsRotating)
        {
            btnRotate.GetComponent<Image>().color = new Color32(111, 229, 223, 255);
        }
        else
        {
            btnRotate.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    void showTag() {
        isPress = !isPress;
        listSubOrgan.SetActive(isPress);
        if (isPress) {
            btnTag.GetComponent<Image>().color = new Color32(111, 229, 223, 255);
            TagHandler.Instance.loadTags();
            if (!isListSubNameOrgan) {
                loadItemSubNameOrgan();
            }
        } else {
            btnTag.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if (TagHandler.Instance.addedTags.Count > 0)
            {
                foreach( GameObject obj in TagHandler.Instance.addedTags) {
                    Destroy(obj);
                }
                TagHandler.Instance.addedTags.Clear();
            }
        }
    }

    void exitListSubOrgan() {
        listSubOrgan.SetActive(false);
    }

    void onSound() {
        isPress = !isPress;
        if (isPress) {
            btnSound.GetComponent<Image>().color = new Color32(111, 229, 223, 255);
        } else {
            btnSound.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    void showInfo() {
        isPressBtnInfo = !isPressBtnInfo;
        ani = panelInfo.GetComponent<Animator>();
        bool isShow = ani.GetBool("show");
        GameObject contentInfo = panelInfo.transform.GetChild(1).gameObject;
        contentInfo.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = OrganManager.DataOrgan.description;
        ani.SetBool("show", !isShow);
        if (isPressBtnInfo) {
            btnInfo.GetComponent<Image>().color = new Color32(111, 229, 223, 255);  
        } else {
            btnInfo.GetComponent<Image>().color = new Color32(255, 255, 255, 255);   
        }
    }

    void loadSceneARView() {
        SceneManager.LoadScene(SceneConfig.arScene);
    }

    void loadSceneQuiz() {
        
    }

    void showMenu() {
        isOpenMenu = !isOpenMenu;
        if (isOpenMenu) {
            btnMenu.GetComponent<Image>().color = new Color32(111, 229, 223, 255);
        } else {
            btnMenu.GetComponent<Image>().color = new Color32(255, 255, 255, 255);   
        }
        panelMenu.SetActive(isOpenMenu);
    }

    void drawNote() {

    }
    
    void showInfoGuide() {

    }

    void showSetting() {

    }

    // component functions
    void loadItemSubNameOrgan() {
        isListSubNameOrgan = true;
        GameObject templateNameOrgan = listSubOrgan.transform.GetChild(0).gameObject;
        GameObject templateItem = listSubOrgan.transform.GetChild(1).gameObject;
        GameObject itemInstance;

        int numberOrgan = Helper.fakeDataSubOgran().Count;
        templateNameOrgan.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = OrganManager.CurrentOrgan;

        foreach (Organ itemOrgan in Helper.fakeDataSubOgran()) {
            itemInstance = Instantiate(templateItem, listSubOrgan.transform);
            itemInstance.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = itemOrgan.name;
        }
        Destroy(templateItem);
    }

    public void itemSubOrganClick(GameObject obj) {
        GameObject objectSubOrganClicked;
        string nameSubOrganClicked;

        if (obj.GetComponent<Image>().color == new Color32(255, 255, 255, 255)) {
            obj.GetComponent<Image>().color = new Color32(209, 200, 200, 255);
            nameSubOrganClicked = obj.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
            objectSubOrganClicked = Helper.FindObject(OrganManager.CurrentOrganObject, nameSubOrganClicked);
            if (objectSubOrganClicked != null) {
                objectSubOrganClicked.SetActive(false);
            }
            foreach (GameObject tagObject in TagHandler.Instance.addedTags) {
                if (tagObject.transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshPro>().text == nameSubOrganClicked) {
                    tagObject.SetActive(false);
                }
            }
        } else {
            obj.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            nameSubOrganClicked = obj.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
            objectSubOrganClicked = Helper.FindObject(OrganManager.CurrentOrganObject, nameSubOrganClicked);
            if (objectSubOrganClicked != null) {
                objectSubOrganClicked.SetActive(true);
            }
            foreach (GameObject tagObject in TagHandler.Instance.addedTags) {
                if (tagObject.transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshPro>().text == nameSubOrganClicked) {
                    tagObject.SetActive(true);
                }
            }
        }
    }

    public void loadItemNameOrganSystem() {
        GameObject parentNameOrgan = GameObject.Find("ContentNameOrganSystem");
        GameObject templateItem = parentNameOrgan.transform.GetChild(0).gameObject;
        GameObject itemInstance;

        foreach (string itemNameOrgan in OrganSystem.ORGAN_SYSTEMS) {
            itemInstance = Instantiate(templateItem, parentNameOrgan.transform);
            itemInstance.transform.GetComponent<TMPro.TextMeshProUGUI>().text = itemNameOrgan;
        }
        Destroy(templateItem);
    }

    public void itemNameOrganSystemClick(GameObject obj) {
        Destroy(OrganManager.CurrentOrganObject);
        string nameOrgan = obj.GetComponent<TMPro.TextMeshProUGUI>().text;
        GameObject currentOrgan = Resources.Load(nameOrgan) as GameObject;
        if (currentOrgan != null) {
            PlayerPrefs.SetString("nameOrgan", nameOrgan);
            PlayerPrefs.Save();
            Organ fakeDataOrgan = new Organ(10, nameOrgan, 1, nameOrgan + " Paragraphs are the building blocks", 0, 0);
            OrganManager.InitOrgan(nameOrgan, Instantiate(currentOrgan), fakeDataOrgan, false, false);
        }
    }
}
