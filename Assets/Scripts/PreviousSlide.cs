using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviousSlide : MonoBehaviour
{
    public MainWorkspace mws;
    // Start is called before the first frame update
    void Start()
    {
        //mws = mwsObj.GetComponent<MainWorkspace>();
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() => {
            Debug.Log("Prev");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
