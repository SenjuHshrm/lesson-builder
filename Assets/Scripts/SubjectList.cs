// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
using Newtonsoft.Json;

public class SubjectList{
    public static string GetSubjectList() {
        string[] subjectLs = { "English", "Math", "Science", "Filipino", "Mother Tounge" };
        Subjects[] subj = new Subjects[6];
        for(int i = 0; i < 6; i++) {
            subj[i] = new Subjects();
            subj[i].grade = i + 1;
            switch(subj[i].grade) {
                case 1:
                    subj[i].subjects = new string[] { subjectLs[0], subjectLs[1], subjectLs[3], subjectLs[4] };
                    break;
                case 2:
                    subj[i].subjects = new string[] { subjectLs[0], subjectLs[1], subjectLs[3] };
                    break;
                default:
                    subj[i].subjects = new string[] { subjectLs[0], subjectLs[1], subjectLs[2], subjectLs[3] };
                    break;
            }
        }
        return JsonConvert.SerializeObject(subj);
    }
}

public class Subjects {
    public string[] subjects;
    public int grade;
}
