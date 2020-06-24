using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MainWorkspace : MonoBehaviour
{
    public Text totalScene, currentScene, screenshotTxt, drawingTxt, storyTxt;
    public ToggleGroup ObjTxtColor;
    public ToggleGroup titleTxtColor;
    public ToggleGroup contentTxtColor;
    public InputField Objectives;
    public Scrollbar inspScroll;
    public Slider screenshot, drawing, story;
    public float[,] objTxtColors = new float[,] {
        {0.01f, 0.01f, 0.01f},
        {0.44f, 0.44f, 0.44f},
        {0.89f, 0.89f, 0.51f},
        {0.16f, 0.32f, 0.55f},
        {0.49f, 0f, 0f}
    };

    // Start is called before the first frame update
    void Start()
    {
        functionTogglers("screenshot");
        functionTogglers("drawing");
        functionTogglers("story");
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

    }

    public void saveScene() {

    }

    public void deleteScene() {
        
    }

    public void changeObjectiveTextColor(int x) {
        // Toggle[] objTxtColorToggle = ObjTxtColor.GetComponentsInChildren<Toggle>();
        // foreach(var selectedToggle in objTxtColorToggle) {
        //     if(selectedToggle.isOn) {
        //         int color = int.Parse((selectedToggle.name).Substring(0, 1));
        //         (Objectives.transform.Find("Text").GetComponent<Text>()).color = new Color(objTxtColors[color, 0], objTxtColors[color, 1], objTxtColors[color, 2], 1f);
        //     }
        // }
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
        Debug.Log("Open Objectives Board Chooser");
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
