using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextSlide : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Preview mws = gameObject.AddComponent<Preview>();
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() => {
            mws.nextSlide();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
