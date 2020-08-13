using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SceneClassHolder;

public class MainWorkspace : MonoBehaviour
{
    public Text totalScene, currentScene;
    public Image assetCon;
    public Toggle SceneThumbnail, AssetItem;
    public ToggleGroup sceneTmbContainer, ObjTxtColor, titleTxtColor, contentTxtColor, ObjBoard, BackgroundImg, TitleBoard, ContentBoard, assetsContainer;
    public CanvasGroup popUpOverlay, objctvBoard, sceneBg, titleBoard, contentBoard, deleteSceneWarn, previewWnd;
    public InputField Objectives, ObjBoardCh, title, board, BgCh, BoardTitleCh, BoardCh;
    public GameObject sceneWnd, sceneWndCon, animateObj, wsFncObj, previewObj, titleScroll, boardScroll, previewCon;
    public float[,] objTxtColors = new float[,] {
        {0.01f, 0.01f, 0.01f},
        {0.44f, 0.44f, 0.44f},
        {0.89f, 0.89f, 0.51f},
        {0.16f, 0.32f, 0.55f},
        {0.49f, 0f, 0f}
    };
    public List<SceneList> scnLs = new List<SceneList>();
    public int generatedSlides = 1, assetsCount = 0, previewCount = 0;
    public Animations anim;
    public WorkspaceFunctions wsFnc;
    public Preview prev;

    // Start is called before the first frame update
    void Start()
    {
        anim = animateObj.GetComponent<Animations>();
        wsFnc = wsFncObj.GetComponent<WorkspaceFunctions>();
        prev = previewObj.GetComponent<Preview>();
        anim.functionTogglers("screenshot");
        anim.functionTogglers("drawing");
        anim.functionTogglers("story");
        title.onValueChanged.AddListener(delegate { setTitle((string)title.text); });
        board.onValueChanged.AddListener(delegate { setContent((string)board.text); });
        titleScroll.GetComponent<ScrollRect>().gameObject.SetActive(false);
        boardScroll.GetComponent<ScrollRect>().gameObject.SetActive(false);
        initScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initScene() {
        totalScene.text = "1";
        currentScene.text = "1";
        //Set 1st scene template
        Transform scnCon = (Transform)sceneWndCon.transform;
        GameObject scnWnC = (GameObject)Instantiate(sceneWnd);
        Button btnC = scnWnC.transform.GetChild(2).GetComponent<Button>();
        btnC.gameObject.SetActive(false);
        scnWnC.transform.SetParent(scnCon, false);
        //Instantiate scene thumbnail
        Transform scnTmb = (Transform)sceneTmbContainer.transform;
        Toggle tmb = (Toggle)Instantiate(SceneThumbnail);
        tmb.isOn = true;
        tmb.group = sceneTmbContainer;
        Text txt = tmb.transform.GetChild(2).GetComponent<Text>();
        EventTrigger tmbEvTrg = tmb.GetComponent<EventTrigger>();
        EventTrigger.Entry evtEntr = new EventTrigger.Entry();
        evtEntr.eventID = EventTriggerType.PointerUp;
        evtEntr.callback.AddListener((_) => { selectScene((Text)txt); });
        tmbEvTrg.triggers.Add(evtEntr);
        tmb.transform.SetParent(scnTmb, false);
        //Set slide thumbnail
        Transform scnConT = tmb.transform.GetChild(1);
        GameObject scnWnT = (GameObject)Instantiate(sceneWnd);
        Button btnT = scnWnT.transform.GetChild(2).GetComponent<Button>();
        btnT.gameObject.SetActive(false);
        scnWnT.transform.SetParent(scnConT, false);
        //Set GameObject to list
        GameObject scnWnCL = (GameObject)Instantiate(sceneWnd);
        Button btnCL = scnWnCL.transform.GetChild(2).GetComponent<Button>();
        btnCL.gameObject.SetActive(false);
        SceneList sL1 = new SceneList() {
            SceneContainer = scnWnCL,
            SceneNumber = 1,
            Background = "",
            BoardTitle = "",
            TitleColor = 0,
            ContentColor = 0,
            BoardTitleAnimation = "",
            BoardAnimation = "",
            Title = "",
            Board = "",
            BoardContent = "",
            Assets = {},
            Screenshot = false,
            Drawing = false,
            Story = false,
            MultipleChoice = false,
            Enumeration = false,
            MatchingType = false,
            FillInTheBlank = false,
            Essay = false
        };
        scnLs.Add(sL1);
        //print(JsonConvert.SerializeObject(scnLs.ToArray()));
    }

    //Button onClick Event Handlers
    public void previewLesson() {
        //
        previewWnd.alpha = 1f;
        previewWnd.blocksRaycasts = true;
        Transform prvCon = previewCon.transform;
        GameObject frstScn = (GameObject)Instantiate(scnLs[0].SceneContainer);
        frstScn.transform.SetParent(prvCon, false);
        //Debug.Log(previewCon);
        prev.initPreview(scnLs);
    }
    
    public void exitPreview() {
        previewWnd.alpha = 0f;
        previewWnd.blocksRaycasts = false;
        Transform t = previewCon.transform.GetChild(0);
        Destroy(t.gameObject);
    }

    public void saveLesson() {
        //
    }

    public void viewOtherLessons() {
        //
    }

    public void addScene() {
        GameObject gmObj = (GameObject)Instantiate(sceneWnd);
        Button btn = gmObj.transform.GetChild(3).GetComponent<Button>();
        btn.gameObject.SetActive(false);
        int ln = scnLs.Count;
        totalScene.text = (ln + 1).ToString();
        SceneList sL1 = new SceneList() {
            SceneContainer = gmObj,
            SceneNumber = ln + 1,
            Background = "",
            BoardTitle = "",
            TitleColor = 0,
            ContentColor = 0,
            BoardTitleAnimation = "",
            BoardAnimation = "",
            Title = "",
            Board = "",
            BoardContent = "",
            Assets = {},
            Screenshot = false,
            Drawing = false,
            Story = false,
            MultipleChoice = false,
            Enumeration = false,
            MatchingType = false,
            FillInTheBlank = false,
            Essay = false
        };
        scnLs.Add(sL1);
        generatedSlides += 1;
        wsFnc.addThumbnail(sceneTmbContainer, SceneThumbnail, generatedSlides, selectScene, scnLs[scnLs.Count - 1].SceneContainer);
        wsFnc.modifyThumbnailsOnAdd(sceneTmbContainer, selectScene, scnLs);
        
    }

    public void saveScene() {
        Toggle[] tgl = sceneTmbContainer.GetComponentsInChildren<Toggle>();
        for(int i = 0; i < tgl.Length; i++) {
            if(tgl[i].isOn) {
                GameObject tr = (GameObject)Instantiate(sceneWndCon.transform.GetChild(0).gameObject);
                scnLs[i].SceneContainer = tr;
                scnLs[i].Background = BgCh.text;
                scnLs[i].Board = BoardCh.text;
                scnLs[i].BoardTitle = BoardTitleCh.text;
                scnLs[i].TitleColor = getSelectedColor(titleTxtColor);
                scnLs[i].ContentColor = getSelectedColor(contentTxtColor);
                scnLs[i].Title = title.text;
                scnLs[i].BoardContent = board.text;
                Destroy(tgl[i].transform.GetChild(1).transform.GetChild(0).gameObject);
                Transform t = (Transform)tgl[i].transform.GetChild(1);
                tr.transform.SetParent(t, false);
                break;
            }
        }
    }

    private int getSelectedColor(ToggleGroup tg) {
        Toggle[] tgl = tg.GetComponentsInChildren<Toggle>();
        int x = 0;
        for(int i = 0; i < tgl.Length; i++) {
            if(tgl[i].isOn) {
                x = i;
                break;
            }
        }
        return x;
    }

    public void deleteSceneWarning() {
        int sceneSize = scnLs.Count;
        if(sceneSize > 1) {
            popUpOverlay.alpha = 1;
            popUpOverlay.blocksRaycasts = true;
            deleteSceneWarn.alpha = 1;
            deleteSceneWarn.blocksRaycasts = true;
        }
    }

    public void deleteScene() {
        generatedSlides -= 1;
        totalScene.text = generatedSlides.ToString();
        Toggle[] currToggle = sceneTmbContainer.GetComponentsInChildren<Toggle>();
        List<Toggle> toggleLs = new List<Toggle>(currToggle);
        for(int i = 0; i < toggleLs.Count; i++) {
            if(toggleLs[i].isOn) {
                Destroy(toggleLs[i].gameObject);
                toggleLs.RemoveAt(i);
                scnLs.RemoveAt(i);
                if(i == 0) {
                    toggleLs[0].isOn = true;
                } else {
                    toggleLs[i-1].isOn = true;
                }
                break;
            }
        }
        currToggle = toggleLs.ToArray();
        Text txt = null;
        for(int i = 0; i < currToggle.Length; i++) {
            txt = currToggle[i].transform.GetChild(2).GetComponent<Text>();
            scnLs[i].SceneNumber = i + 1;
            txt.text = "Scene " + (i + 1).ToString();
        }
        for(int i = 0; i < currToggle.Length; i++) {
            GameObject gmObj = (GameObject)Instantiate(scnLs[i].SceneContainer);
            Button pbtn = currToggle[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<Button>(),
                    nbtn = currToggle[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).GetComponent<Button>(),
                    pbtnC = gmObj.transform.GetChild(2).GetComponent<Button>(),
                    nbtnC = gmObj.transform.GetChild(3).GetComponent<Button>();
            if(i == 0) {
                pbtn.gameObject.SetActive(false);
                nbtn.gameObject.SetActive(true);
                pbtnC.gameObject.SetActive(false);
                nbtnC.gameObject.SetActive(true);
            } else if(i == currToggle.Length - 1) {
                pbtn.gameObject.SetActive(true);
                nbtn.gameObject.SetActive(false);
                pbtnC.gameObject.SetActive(true);
                nbtnC.gameObject.SetActive(false);
            } else {
                pbtn.gameObject.SetActive(true);
                nbtn.gameObject.SetActive(true);
                pbtnC.gameObject.SetActive(true);
                nbtnC.gameObject.SetActive(true);
            }
            scnLs[i].SceneContainer = gmObj;
        }
        for(int i = 0; i < currToggle.Length; i++) {
            if(currToggle[i].isOn) {
                Text t = currToggle[i].transform.GetChild(2).GetComponent<Text>();
                selectScene(t);
                break;
            }
        }
        exitWarning();
    }

    public void exitWarning() {
        popUpOverlay.alpha = 0;
        popUpOverlay.blocksRaycasts = false;
        deleteSceneWarn.alpha = 0;
        deleteSceneWarn.blocksRaycasts = false;
    }

    public void selectScene(Text txt) {
        //Set current scene number
        string currScene = txt.text.Replace("Scene ", "");
        currentScene.text = currScene;
        int i = int.Parse(currScene) - 1;
        // //Remove current scene on the container
        Transform trScn = sceneWndCon.transform.GetChild(0);
        Destroy(trScn.gameObject);
        // //Instantiate scene from List<T>()
        Transform scnCon = (Transform)sceneWndCon.transform;
        GameObject scnWn = (GameObject)Instantiate(scnLs[i].SceneContainer);
        scnWn.transform.SetParent(scnCon, false);
        setProperties(i);
    }

    public void changeObjectiveTextColor(int x) {
        (Objectives.transform.Find("Text").GetComponent<Text>()).color = new Color(objTxtColors[x, 0], objTxtColors[x, 1], objTxtColors[x, 2], 1f);
    }

    public void changeTitleTextColor(int x) {
        Text txt = sceneWndCon.transform.GetChild(0).transform.GetChild(5).transform.GetChild(0).gameObject.GetComponent<Text>();
        txt.color = new Color(objTxtColors[x, 0], objTxtColors[x, 1], objTxtColors[x, 2], 1f);
    }

    public void changeContentTextColor(int x) {
        Text txt = sceneWndCon.transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).gameObject.GetComponent<Text>();
        txt.color = new Color(objTxtColors[x, 0], objTxtColors[x, 1], objTxtColors[x, 2], 1f);
    }

    public void addAsset() {
        assetsCount += 1;
        Transform t = (Transform)assetsContainer.transform,
                imgCon = (Transform)sceneWndCon.transform.GetChild(0);
        Toggle tg = (Toggle)Instantiate(AssetItem);
        Image img = (Image)Instantiate(assetCon);
        tg.isOn = (assetsCount == 1) ? true : false ;
        tg.group = assetsContainer;
        Text txt = tg.transform.GetChild(1).GetComponent<Text>();
        txt.text = "Asset " + assetsCount.ToString();
        EventTrigger trgr = tg.transform.GetChild(2).GetComponent<EventTrigger>();
        EventTrigger.Entry entr = new EventTrigger.Entry();
        entr.eventID = EventTriggerType.PointerUp;
        entr.callback.AddListener((_) => { deleteAsset((Text)txt); });
        trgr.triggers.Add(entr);
        img.transform.SetParent(imgCon, false);
        tg.transform.SetParent(t, false);
    }

    public void deleteAsset(Text txt) {
        assetsCount -= 1;
        foreach(var toggle in assetsContainer.ActiveToggles()) {
            Text t = toggle.transform.GetChild(1).GetComponent<Text>();
            if(t.text == txt.text) {
                Destroy(toggle.gameObject);
                break;
            }
        }
    }

    public void openObjctvBoardChooser() {
        objctvBoard.alpha = 1;
        objctvBoard.blocksRaycasts = true;
        popUpOverlay.alpha = 1;
        popUpOverlay.blocksRaycasts = true;
    }

    public void chooseObjectiveBoard() {
        foreach(var toggle in ObjBoard.ActiveToggles()) {
            if(toggle.isOn) {
                ObjBoardCh.text = toggle.name;
            }
        }
        objctvBoard.alpha = 0;
        objctvBoard.blocksRaycasts = false;
        popUpOverlay.alpha = 0;
        popUpOverlay.blocksRaycasts = false;
    }

    public void openSceneBg() {
        sceneBg.alpha = 1;
        sceneBg.blocksRaycasts = true;
        popUpOverlay.alpha = 1;
        popUpOverlay.blocksRaycasts = true;
    }

    public void chooseSceneBg() {
        foreach(var toggle in BackgroundImg.ActiveToggles()) {
            if(toggle.isOn) {
                BgCh.text = toggle.name;
            }
        }
        Sprite bg = Resources.Load<Sprite>("MyBackground/" + BgCh.text);
        Transform gobj = sceneWndCon.transform.GetChild(0);
        gobj.GetComponent<Image>().sprite = bg;
        gobj.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
        sceneBg.alpha = 0;
        sceneBg.blocksRaycasts = false;
        popUpOverlay.alpha = 0;
        popUpOverlay.blocksRaycasts = false;
    }

    public void openTitleBoard() {
        titleBoard.alpha = 1;
        titleBoard.blocksRaycasts = true;
        popUpOverlay.alpha = 1;
        popUpOverlay.blocksRaycasts = true;
    }

    public void chooseTitleBoard() {
        foreach(var toggle in TitleBoard.ActiveToggles()) {
            if(toggle.isOn) {
                BoardTitleCh.text = toggle.name;
            }
        }
        Sprite bg = Resources.Load<Sprite>("MyTitleBoard/" + BoardTitleCh.text);
        Transform gobj = sceneWndCon.transform.GetChild(0).transform.GetChild(5);
        gobj.GetComponent<Image>().sprite = bg;
        gobj.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
        titleScroll.GetComponent<ScrollRect>().gameObject.SetActive(true);
        title.interactable = true;
        titleBoard.alpha = 0;
        titleBoard.blocksRaycasts = false;
        popUpOverlay.alpha = 0;
        popUpOverlay.blocksRaycasts = false;
    }

    public void openContentBoard() {
        contentBoard.alpha = 1;
        contentBoard.blocksRaycasts = true;
        popUpOverlay.alpha = 1;
        popUpOverlay.blocksRaycasts = true;
    }

    public void chooseContentBoard() {
        foreach(var toggle in ContentBoard.ActiveToggles()) {
            if(toggle.isOn) {
                BoardCh.text = toggle.name;
            }
        }
        Sprite bg = Resources.Load<Sprite>("MyBoard/" + BoardCh.text);
        Transform gobj = sceneWndCon.transform.GetChild(0).transform.GetChild(4);
        gobj.GetComponent<Image>().sprite = bg;
        gobj.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
        boardScroll.GetComponent<ScrollRect>().gameObject.SetActive(true);
        board.interactable = true;
        contentBoard.alpha = 0;
        contentBoard.blocksRaycasts = false;
        popUpOverlay.alpha = 0;
        popUpOverlay.blocksRaycasts = false;
    }

    public void setProperties(int i) {
        BgCh.text = scnLs[i].Background;
        BoardTitleCh.text = scnLs[i].BoardTitle;
        BoardCh.text = scnLs[i].Board;
        title.text = scnLs[i].Title;
        board.text = scnLs[i].BoardContent;
        titleTxtColor.transform.GetChild(scnLs[i].TitleColor).GetComponent<Toggle>().isOn = true;
        contentTxtColor.transform.GetChild(scnLs[i].ContentColor).GetComponent<Toggle>().isOn = true;
        title.interactable = (scnLs[i].BoardTitle == "") ? false : true;
        board.interactable = (scnLs[i].Board == "") ? false : true;
        boardScroll.GetComponent<ScrollRect>().gameObject.SetActive((scnLs[i].Board == "") ? false : true);
        titleScroll.GetComponent<ScrollRect>().gameObject.SetActive((scnLs[i].BoardTitle == "") ? false : true);
        
    }

    public void setTitle(string x) {
        Text txt = sceneWndCon.transform.GetChild(0).transform.GetChild(5).transform.GetChild(0).gameObject.GetComponent<Text>();
        txt.text = x;
    }

    public void setContent(string x) {
        Text txt = sceneWndCon.transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).gameObject.GetComponent<Text>();
        txt.text = x;
    }
    

}

namespace SceneClassHolder {
    public class SceneList {
        public GameObject SceneContainer { get; set; }
        public int SceneNumber { get; set; }
        //Scene
        public string Background { get; set; }
        public string BoardTitle { get; set; }
        public int TitleColor { get; set; }
        public int ContentColor { get; set; }
        public string BoardTitleAnimation { get; set; }
        public string BoardAnimation { get; set; }
        public string Title { get; set; }
        public string Board { get; set; }
        public string BoardContent { get; set; }
        public string[] Assets { get; set; }
        //Functions
        public bool Screenshot { get; set; }
        public bool Drawing { get; set; }
        public bool Story { get; set; }
        //Assessment
        public bool MultipleChoice { get; set; }
        public bool Enumeration { get; set; }
        public bool MatchingType { get; set; }
        public bool FillInTheBlank { get; set; }
        public bool Essay { get; set; }
    }
}
