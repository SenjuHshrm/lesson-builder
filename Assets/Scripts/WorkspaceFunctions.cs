using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SceneClassHolder;

public delegate void SelectSceneDelegate(Text txt);

public class WorkspaceFunctions : MonoBehaviour
{
    public void addThumbnail(ToggleGroup tg, Toggle tgl, int i, SelectSceneDelegate selScn, GameObject sceneWnd) {
        Transform slideCon = (Transform)tg.transform;
        Toggle tmb = (Toggle)Instantiate(tgl);
        tmb.isOn = false;
        tmb.group = tg;
        tmb.transform.SetParent(slideCon, false);
        //Set scene label
        Text txt = tmb.transform.GetChild(2).GetComponent<Text>();
        txt.text = "Scene " + i.ToString();
        //Set event trigger
        EventTrigger tmbEvTrg = tmb.GetComponent<EventTrigger>();
        EventTrigger.Entry evtEntr = new EventTrigger.Entry();
        evtEntr.eventID = EventTriggerType.PointerUp;
        evtEntr.callback.AddListener((_) => { selScn((Text)txt); });
        tmbEvTrg.triggers.Add(evtEntr);
        //Set the thumbnail
        Transform t = tmb.transform.GetChild(1);
        sceneWnd.transform.SetParent(t, false);
    }

    public void modifyThumbnailsOnAdd(ToggleGroup tgrp, SelectSceneDelegate selScn, List<SceneList> ls) {
        Toggle[] tgl = tgrp.GetComponentsInChildren<Toggle>();
        for(int i = 0; i < tgl.Length; i++) {
            Button pbtn = tgl[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<Button>(),
                    nbtn = tgl[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).GetComponent<Button>(),
                    pbtnC = ls[i].SceneContainer.transform.GetChild(2).GetComponent<Button>(),
                    nbtnC = ls[i].SceneContainer.transform.GetChild(3).GetComponent<Button>();

            if(i == 0) {
                pbtn.gameObject.SetActive(false);
                nbtn.gameObject.SetActive(true);
                pbtnC.gameObject.SetActive(false);
                nbtnC.gameObject.SetActive(true);
            } else if(i == tgl.Length - 1) {
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
            if(tgl[i].isOn) {
                Text txt = tgl[i].transform.GetChild(2).GetComponent<Text>();
                selScn(txt);
            }
            continue;
        }
    }
}
