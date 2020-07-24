using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SceneClassHolder;

public delegate void SelectSceneDelegate(Text txt);

public class WorkspaceFunctions : MonoBehaviour
{
    public void setProperties() {
        
    }


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

    public void modifyThumbnailsOnAdd(ToggleGroup tgrp) {
        Toggle[] tgl = tgrp.GetComponentsInChildren<Toggle>();
        for(int i = 0; i < tgl.Length; i++) {
            Transform t = tgl[i].transform.GetChild(1).transform.GetChild(0);
            if(i == tgl.Length - 1) {
                Button pbtn = t.transform.GetChild(2).GetComponent<Button>();
                pbtn.gameObject.SetActive(true);
                Button nbtn = t.transform.GetChild(3).GetComponent<Button>();
                nbtn.gameObject.SetActive(false);
            } else if(i == 0) {
                Button pbtn = t.transform.GetChild(2).GetComponent<Button>();
                pbtn.gameObject.SetActive(false);
                Button nbtn = t.transform.GetChild(3).GetComponent<Button>();
                nbtn.gameObject.SetActive(true);
            } else {
                Button pbtn = t.transform.GetChild(2).GetComponent<Button>();
                pbtn.gameObject.SetActive(true);
                Button nbtn = t.transform.GetChild(3).GetComponent<Button>();
                nbtn.gameObject.SetActive(true);
            }
        }
    }
}
