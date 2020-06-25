using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SceneClassHolder;

public class MainWorkspace : MonoBehaviour
{
    public Text totalScene, currentScene, screenshotTxt, drawingTxt, storyTxt;
    public ToggleGroup ObjTxtColor, titleTxtColor, contentTxtColor;
    public CanvasGroup popUpOverlay, objctvBoard, sceneBg, titleBoard, contentBoard;
    public InputField Objectives, title, board;
    public Scrollbar inspScroll;
    public Slider screenshot, drawing, story;
    public GameObject sceneWndF,sceneWndM, sceneWndL, sceneTmbF, sceneTmbM, sceneTmbL, sceneTmbContainer;
    public float[,] objTxtColors = new float[,] {
        {0.01f, 0.01f, 0.01f},
        {0.44f, 0.44f, 0.44f},
        {0.89f, 0.89f, 0.51f},
        {0.16f, 0.32f, 0.55f},
        {0.49f, 0f, 0f}
    };
    public SceneClass scenes;

    // Start is called before the first frame update
    void Start()
    {
        functionTogglers("screenshot");
        functionTogglers("drawing");
        functionTogglers("story");
        scenes = new SceneClass();
        var sceneList = new List<SceneClass.SceneList[]>();
        
        
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
        GameObject nScnTmbCon = (GameObject)Instantiate(sceneTmbContainer);
        RectTransform rt = (RectTransform)nScnTmbCon.transform;
        float wdt = rt.rect.width;
        float ht = rt.rect.height + 300f;
        rt.sizeDelta = new Vector2(wdt, ht);
    }

    public void saveScene() {

    }

    public void deleteScene() {
        
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

    public void openObjctvBoardChooser() {
        objctvBoard.alpha = 1;
        popUpOverlay.alpha = 1;
        popUpOverlay.blocksRaycasts = true;
    }

    public void chooseObjectiveBoard() {
        objctvBoard.alpha = 0;
        popUpOverlay.alpha = 0;
        popUpOverlay.blocksRaycasts = false;
    }

    public void openSceneBg() {
        sceneBg.alpha = 1;
        popUpOverlay.alpha = 1;
        popUpOverlay.blocksRaycasts = true;
    }

    public void chooseSceneBg() {
        sceneBg.alpha = 0;
        popUpOverlay.alpha = 0;
        popUpOverlay.blocksRaycasts = false;
    }

    public void openTitleBoard() {
        titleBoard.alpha = 1;
        popUpOverlay.alpha = 1;
        popUpOverlay.blocksRaycasts = true;
    }

    public void chooseTitleBoard() {
        titleBoard.alpha = 0;
        popUpOverlay.alpha = 0;
        popUpOverlay.blocksRaycasts = false;
    }

    public void openContentBoard() {
        contentBoard.alpha = 1;
        popUpOverlay.alpha = 1;
        popUpOverlay.blocksRaycasts = true;
    }

    public void chooseContentBoard() {
        contentBoard.alpha = 0;
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
}

namespace SceneClassHolder {
    public class SceneClass {
        public class SceneList {
            public int SceneNumber = 1;
            public bool IsSelected = true;
            public class Properties {
                public class Scene {
                    public string Background = "";
                    public string BoardTitle = "";
                    public string TitleColor = "";
                    public string ContentColor = "";
                    public string BoardTitleAnimation = "";
                    public string BoardAnimation = "";
                    public string Title = "";
                    public string Board = "";
                    public string[] Assets = {};
                }
                public class Functions {
                    public bool Screenshot = false;
                    public bool Drawing = false;
                    public bool Story = false;
                }
                public class Assessment {
                    public bool MultipleChoice = false;
                    public bool Enumeration = false;
                    public bool MatchingType = false;
                    public bool FillInTheBlank = false;
                    public bool Essay = false;
                }
            }
        };
}
}
