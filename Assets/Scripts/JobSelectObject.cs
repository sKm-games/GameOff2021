using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JobSelectObject : MonoBehaviour
{
    [SerializeField] private JobController jobController;

    private JobInfoClass jobInfo;
    private TextMeshProUGUI clientNameText;    
    private TextMeshProUGUI jobDescriptionText;
    private Image clientIcon;

    private Button jobSelectButton;


    private void Awake()
    {
        clientNameText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        jobDescriptionText = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        clientIcon = this.transform.GetChild(2).GetComponent<Image>();

        jobSelectButton = this.transform.GetChild(4).GetComponent<Button>();
        jobSelectButton.onClick.RemoveAllListeners();
        jobSelectButton.onClick.AddListener(() => jobController.TakeJob(jobInfo));
    }

    public void UpdateJobInfo(JobInfoClass ji)
    {
        jobInfo = new JobInfoClass();
        jobInfo = ji;

        string s = jobInfo.ClientName + "\n" + jobInfo.ClientAddress;
        clientNameText.text = s;
        clientIcon.sprite = jobInfo.ClientIcon;

        jobInfo.JobBugProfile.BugJobDescription.Shuffle();
        s = jobInfo.JobBugProfile.BugJobDescription[0] + "\nPayment: " + jobInfo.JobBugProfile.BugJobBasePayment;
        jobDescriptionText.text = s;        
    }    
}
