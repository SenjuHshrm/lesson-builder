using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SceneClassHolder;
public class Preview : MonoBehaviour
{
    public List<SceneList> lists;
    public int previewCount = 0, lsCount = 0;
    public GameObject prevCon, prevRegion;
    public Button prev, nxt, exitBtn;

    public void initPreview(List<SceneList> a, GameObject con) {
        lists = new List<SceneList>(a);
        lsCount = lists.Count - 1;
        setBtns();
        if(lsCount == 0) {
            nxt.gameObject.SetActive(false);
        }
    }

    public void nextSlide() {
        previewCount++;
        Transform t = (Transform)prevCon.transform.GetChild(0);
        Destroy(t.gameObject);
        GameObject gmObj = (GameObject)Instantiate(lists[previewCount].SceneContainer);
        Transform prvCon = (Transform)prevCon.transform;
        gmObj.transform.SetParent(prvCon, false);
        setBtns();
    }

    public void previousSlide() {
        previewCount--;
        Transform t = (Transform)prevCon.transform.GetChild(0);
        Destroy(t.gameObject);
        GameObject gmObj = (GameObject)Instantiate(lists[previewCount].SceneContainer);
        Transform prvCon = (Transform)prevCon.transform;
        gmObj.transform.SetParent(prvCon, false);
        setBtns();
    }

    public void setBtns() {
        if(previewCount == 0) {
            prev.gameObject.SetActive(false);
            nxt.gameObject.SetActive(true);
        } else if(previewCount == lsCount) {
            prev.gameObject.SetActive(true);
            nxt.gameObject.SetActive(false);
        } else {
            prev.gameObject.SetActive(true);
            nxt.gameObject.SetActive(true);
        }
    }
}
