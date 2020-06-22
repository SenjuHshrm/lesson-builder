using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CreateLesson : MonoBehaviour
{

    public CanvasGroup lessonCreator;
    public CanvasGroup workSpace;
    public Dropdown drpSubj;
    public Dropdown drpGradeLvl;
    public Dropdown drpQrt;
    public Dropdown drpWeek;
    public Dropdown drpDay;
    public Text lessonName;


    // Start is called before the first frame update
    void Start()
    {
        lessonCreator.alpha = 0;
        workSpace.alpha = 0;
        StartCoroutine(FadeCanvasGroup(lessonCreator, lessonCreator.alpha, 1));
        StartCoroutine(FadeCanvasGroup(workSpace, workSpace.alpha, 1));
        GradeSelected(drpGradeLvl, drpSubj);
        drpGradeLvl.onValueChanged.AddListener(delegate { GradeSelected(drpGradeLvl, drpSubj); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GradeSelected(Dropdown dpGr, Dropdown dpSub) {
        int index = dpGr.value;
        int grLvl = int.Parse(dpGr.options[index].text);
        List<Subjects> sub = new List<Subjects>();
        sub = JsonConvert.DeserializeObject<List<Subjects>>(SubjectList.GetSubjectList());
        foreach(Subjects x in sub) {
            if(x.grade == grLvl) {
                dpSub.options.Clear();
                foreach(string i in x.subjects) {
                    dpSub.options.Add(new Dropdown.OptionData() { text = i });
                }
                break;
            }
        }
        dpSub.RefreshShownValue();
        dpSub.value = 0;
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.5f) {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while(true) {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;
            float currentValue = Mathf.Lerp(start, end, percentageComplete);
            cg.alpha = currentValue;
            yield return new WaitForEndOfFrame();
        }
    }

    public void startLesson() {
        string subj = (drpSubj.options[drpSubj.value].text == "Mother Tounge") ? "MT" : (drpSubj.options[drpSubj.value].text).Substring(0, 1),
            grdLvl = drpGradeLvl.options[drpGradeLvl.value].text,
            qrtr = drpQrt.options[drpQrt.value].text,
            week = drpWeek.options[drpWeek.value].text,
            day = drpDay.options[drpDay.value].text;
        lessonName.text = subj + grdLvl + "Q" + qrtr + "W" + week + "D" + day;
        StartCoroutine(FadeCanvasGroup(lessonCreator, lessonCreator.alpha, 0));
        lessonCreator.blocksRaycasts = false;
    }

}
