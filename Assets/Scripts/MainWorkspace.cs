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
    public Text totalScene, currentScene, screenshotTxt, drawingTxt, storyTxt;
    public Toggle SceneThumbnail;
    public ToggleGroup sceneTmbContainer, ObjTxtColor, titleTxtColor, contentTxtColor, ObjBoard, BackgroundImg, TitleBoard, ContentBoard;
    public CanvasGroup popUpOverlay, objctvBoard, sceneBg, titleBoard, contentBoard, deleteSceneWarn;
    public InputField Objectives, ObjBoardCh, title, board, BgCh, BoardTitleCh, BoardCh;
    public Scrollbar inspScroll;
    public Slider screenshot, drawing, story;
    public GameObject objCon, sceneCon, asmtCon, sceneWnd, sceneWndCon;
    public Image objToggler, sceneToggler, asmtToggler;
    public float[,] objTxtColors = new float[,] {
        {0.01f, 0.01f, 0.01f},
        {0.44f, 0.44f, 0.44f},
        {0.89f, 0.89f, 0.51f},
        {0.16f, 0.32f, 0.55f},
        {0.49f, 0f, 0f}
    };
    public List<SceneClass.SceneList> scnLs = new List<SceneClass.SceneList>();
    public int selectedScene;
    public float yAxis = -533.8f;
    public int generatedSlides = 1;

    // Start is called before the first frame update
    void Start()
    {
        functionTogglers("screenshot");
        functionTogglers("drawing");
        functionTogglers("story");
        SceneClass.SceneList sL1 = new SceneClass.SceneList() {
            SceneContainer = sceneWnd,
            SceneNumber = 1,
            IsSelected = true,
            Background = "",
            BoardTitle = "",
            TitleColor = "",
            ContentColor = "",
            BoardTitleAnimation = "",
            BoardAnimation = "",
            Title = "",
            Board = "",
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
        totalScene.text = "1";
        currentScene.text = "1";
        Transform scnCon = (Transform)sceneWndCon.transform;
        GameObject scnWn = (GameObject)Instantiate(sceneWnd);
        scnWn.transform.SetParent(scnCon, false);
        Transform t = scnWn.transform.GetChild(3);
        Button b = t.GetComponent<Button>(); 
        b.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Button onClick Event Handlers
    public void previewLesson() {

    }

    public void saveLesson() {

    }

    public void viewOtherLessons() {

    }

    public void addScene() {
        int ln = scnLs.Count;
        SceneClass.SceneList sL1 = new SceneClass.SceneList() {
            SceneContainer = sceneWnd,
            SceneNumber = ln + 1,
            IsSelected = true,
            Background = "",
            BoardTitle = "",
            TitleColor = "",
            ContentColor = "",
            BoardTitleAnimation = "",
            BoardAnimation = "",
            Title = "",
            Board = "",
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
        Transform trScn = sceneWndCon.transform.GetChild(0);
        Destroy(trScn.gameObject);
        Transform scnCon = (Transform)sceneWndCon.transform;
        GameObject scnWn = (GameObject)Instantiate(sL1.SceneContainer);
        scnWn.transform.SetParent(scnCon, false);
        Transform trf = null;
        
        Transform slideCon = (Transform)sceneTmbContainer.transform;
        Toggle tmb = (Toggle)Instantiate(SceneThumbnail);
        tmb.isOn = false;
        tmb.group = sceneTmbContainer;
        tmb.transform.SetParent(slideCon, false);
        Transform t = tmb.transform.GetChild(2);
        Text txt = t.GetComponent<Text>();
        txt.text = "Scene " + (generatedSlides).ToString();
        EventTrigger trg = tmb.GetComponent<EventTrigger>();
        EventTrigger.Entry entr = new EventTrigger.Entry();
        entr.eventID = EventTriggerType.PointerUp;
        entr.callback.AddListener((_) => { selectScene((Text)txt); });
        trg.triggers.Add(entr);
        totalScene.text = (ln + 1).ToString();
        Toggle[] tgl = sceneTmbContainer.GetComponentsInChildren<Toggle>();
        for(int i = 0; i < tgl.Length; i++) {
            if(tgl[i].isOn) {
                if(i == tgl.Length - 1) {
                    trf = scnWn.transform.GetChild(2);
                    Button b = trf.GetComponent<Button>(); 
                    b.gameObject.SetActive(false);
                } else if(i == 0) {
                    trf = scnWn.transform.GetChild(3);
                    Button b = trf.GetComponent<Button>(); 
                    b.gameObject.SetActive(false);
                }
                
            }
        }
    }

    public void saveScene() {
        Toggle[] tgl = sceneTmbContainer.GetComponentsInChildren<Toggle>();
        for(int i = 0; i < tgl.Length; i++) {
            if(tgl[i].isOn) {
                Transform tr = sceneWndCon.transform.GetChild(0);
                scnLs[i].SceneContainer = tr.gameObject;
                break;
            }
        }
        
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
        int sceneSize = scnLs.Count;
        Toggle[] currToggle = sceneTmbContainer.GetComponentsInChildren<Toggle>();
        List<Toggle> toggleLs = new List<Toggle>(currToggle);
        for(int i = 0; i < toggleLs.Count; i++) {
            if(toggleLs[i].isOn) {
                Destroy(toggleLs[i].gameObject);
                toggleLs.RemoveAt(i);
                if(i == 0) {
                    currToggle[0].isOn = true;
                } else {
                    currToggle[i-1].isOn = true;
                }
                break;
            }
        }
        currToggle = toggleLs.ToArray();
        generatedSlides -= 1;
        scnLs.RemoveAt(sceneSize - 1);
        totalScene.text = (sceneSize - 1).ToString();
        for(int i = 0; i < currToggle.Length; i++) {
            Transform t = currToggle[i].transform.GetChild(2);
            Text txt = t.GetComponent<Text>();
            txt.text = "Scene " + (i + 1).ToString();
            if(currToggle[i].isOn) {
                selectScene(txt);
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
        Transform trScn = sceneWndCon.transform.GetChild(0);
        Destroy(trScn.gameObject);
        string str = txt.text;
        string[] s = str.Split(' ');
        currentScene.text = s[1];
        int i = int.Parse(s[1]);
        SceneClass.SceneList scn = scnLs[i-1];
        Transform scnCon = (Transform)sceneWndCon.transform;
        GameObject scnWn = (GameObject)Instantiate(scn.SceneContainer);
        scnWn.transform.SetParent(scnCon, false);
        Transform t = null;
        if(i == 1) {
            t = scnWn.transform.GetChild(3);
            Button b = t.GetComponent<Button>(); 
            b.gameObject.SetActive(false);
        } else if(i == scnLs.Count) {
            t = scnWn.transform.GetChild(2);
            Button b = t.GetComponent<Button>(); 
            b.gameObject.SetActive(false);
        }
    }

    public void changeObjectiveTextColor(int x) {
        (Objectives.transform.Find("Text").GetComponent<Text>()).color = new Color(objTxtColors[x, 0], objTxtColors[x, 1], objTxtColors[x, 2], 1f);
    }

    public void showInspScroll() {
        float scllbr = (inspScroll.GetComponent<CanvasGroup>()).alpha;
        if(scllbr == 1f) {
            (inspScroll.GetComponent<CanvasGroup>()).alpha = 0f;
        } else {
            (inspScroll.GetComponent<CanvasGroup>()).alpha = 1f;
        }
    }

    public void addAsset() {

    }

    public void openFileDlg() {
        string path = EditorUtility.OpenFilePanel("Select asset to use", "", "png");
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
        contentBoard.alpha = 0;
        contentBoard.blocksRaycasts = false;
        popUpOverlay.alpha = 0;
        popUpOverlay.blocksRaycasts = false;
    }

    public void functionTogglers(string fnc) {
        switch(fnc) {
            case "screenshot":
                switchState(screenshot, screenshot.value, screenshotTxt);
                break;
            case "drawing":
                switchState(drawing, drawing.value, drawingTxt);
                break;
            case "story":
                switchState(story, story.value, storyTxt);
                break;
        }
    }

    private void switchState(Slider sl, float state, Text txt) {
        if(state == 1f) {
            sl.value = 0f;
            txt.text = "OFF";
            sl.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(0.61f, 0.61f, 0.61f, 1f);
            sl.gameObject.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = new Color(0.47f, 0.47f, 0.47f, 1f); 
        } else {
            sl.value = 1f;
            txt.text = "ON";
            sl.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(0f, 0.38f, 0f, 1f);
            sl.gameObject.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = new Color(0f, 0.38f, 0f, 1f); 
        }
    }

    public void toggleObjective() {
        Animator animCn = objCon.GetComponent<Animator>(),
                animTglr = objToggler.GetComponent<Animator>();
        if(animCn != null && animTglr != null) {
            bool cnIsOpen = animCn.GetBool("open"),
                tglrIsOpen = animTglr.GetBool("open");
            animCn.SetBool("open", !cnIsOpen);
            animTglr.SetBool("open", !tglrIsOpen);
        }
    }

    public void toggleScene() {
        Animator animCn = sceneCon.GetComponent<Animator>(),
                animTglr = sceneToggler.GetComponent<Animator>();
        if(animCn != null) {
            bool cnIsOpen = animCn.GetBool("open"),
                tglrIsOpen = animTglr.GetBool("open");
            animCn.SetBool("open", !cnIsOpen);
            animTglr.SetBool("open", !tglrIsOpen);
        }
    }

    public void toggleAsmt() {
        Animator animCn = asmtCon.GetComponent<Animator>(),
                animTglr = asmtToggler.GetComponent<Animator>();
        if(animCn != null) {
            bool cnIsOpen = animCn.GetBool("open"),
                tglrIsOpen = animTglr.GetBool("open");
            animCn.SetBool("open", !cnIsOpen);
            animTglr.SetBool("open", !tglrIsOpen);
        }
    }

}

namespace SceneClassHolder {
    public class SceneClass {
        public class SceneList {
            public GameObject SceneContainer { get; set; }
            public int SceneNumber { get; set; }
            public bool IsSelected { get; set; }
            //Scene
            public string Background { get; set; }
            public string BoardTitle { get; set; }
            public string TitleColor { get; set; }
            public string ContentColor { get; set; }
            public string BoardTitleAnimation { get; set; }
            public string BoardAnimation { get; set; }
            public string Title { get; set; }
            public string Board { get; set; }
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
        };
    }
}
