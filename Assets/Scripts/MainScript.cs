using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public CanvasGroup intro;
    public CanvasGroup lessonCreator;
    public CanvasGroup workSpace;

    public void createLesson() {
        StartCoroutine(FadeCanvasGroup(intro, intro.alpha, 0));
        // StartCoroutine(FadeCanvasGroup(lessonCreator, lessonCreator.alpha, 1));
        // StartCoroutine(FadeCanvasGroup(workSpace, workSpace.alpha, 1));
        SceneManager.LoadScene(1);
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

}
