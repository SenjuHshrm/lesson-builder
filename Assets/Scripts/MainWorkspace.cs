using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MainWorkspace : MonoBehaviour
{
    public Text totalScene;
    public Text currentScene;
    public ToggleGroup ObjTxtColor;
    public InputField Objectives;
    public int[,] objTxtColors = new int[,] {
        {3, 3, 3},
        {112, 112, 112},
        {226, 226, 129},
        {42, 82, 140},
        {124, 0, 0}
    };

    // Start is called before the first frame update
    void Start()
    {

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

    public void changeObjectiveTextColor() {
        Toggle[] objTxtColorToggle = ObjTxtColor.GetComponentsInChildren<Toggle>();
        foreach(var selectedToggle in objTxtColorToggle) {
            if(selectedToggle.isOn) {
                
                int color = int.Parse((selectedToggle.name).Substring(0, 1));
                (Objectives.transform.Find("Text").GetComponent<Text>()).color = new Color(objTxtColors[color, 0], objTxtColors[color, 1], objTxtColors[color, 2]);
            }
        }
    }
}
