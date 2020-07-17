using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animations : MonoBehaviour
{
    #region "variable declarations"
    public Text screenshotTxt, drawingTxt, storyTxt;
    public Slider screenshot, drawing, story;
    public GameObject objCon, sceneCon, asmtCon;
    public Image objToggler, sceneToggler, asmtToggler;
    #endregion

    #region "function togglers"
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
    #endregion

    #region "inspector panel togglers"
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
    #endregion
}
