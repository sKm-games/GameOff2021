using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobController : MonoBehaviour
{
    [SerializeField] private Transform jobSelectHolder;
    private List<JobSelectObject> jobSelectObjects;
    private UIController uiController;
    private LevelController levelController;

    [SerializeField] private List<BugProfileDataObject> bugProfiles;
    public List<BugProfileDataObject> GetBugProfiles
    {
        get
        {
            return bugProfiles;
        }
    }

    [SerializeField] private List<Sprite> clientIcons;
    [SerializeField] private List<string> clientFirstname;
    [SerializeField] private List<string> clientLastname;
    [SerializeField] private List<string> clientAddress;

    [SerializeField] private List<string> cluesFound;

    private JobInfoClass activeJobInfo;
    public JobInfoClass GetActiveJobInfo
    {
        get
        {
            return activeJobInfo;
        }
    }    

    private bool newJobs;

    private void Awake()
    {
        jobSelectObjects = new List<JobSelectObject>(jobSelectHolder.GetComponentsInChildren<JobSelectObject>());
        uiController = this.GetComponent<UIController>();
        levelController = this.GetComponent<LevelController>();
        newJobs = true;
    }

    public void CreateJobs()
    {
        if (!newJobs)
        {
            return;
        }

        foreach (JobSelectObject js in jobSelectObjects)
        {
            js.UpdateJobInfo(GetJobInfo());
        }

        newJobs = false;
        cluesFound = new List<string>();
    }

    private JobInfoClass GetJobInfo()
    {
        JobInfoClass ji = new JobInfoClass();
        clientAddress.Shuffle();
        clientFirstname.Shuffle();
        clientLastname.Shuffle();
        clientIcons.Shuffle();
        
        List<BugProfileDataObject> tempBugs = new List<BugProfileDataObject>(bugProfiles);
        tempBugs.Shuffle();

        ji.ClientName = clientFirstname[0] + " " + clientLastname[0];
        ji.ClientAddress = clientAddress[0];
        ji.ClientIcon = clientIcons[0];
        ji.JobBugProfile = tempBugs[0];

        return ji;
    }

    public void TakeJob(JobInfoClass ji)
    {
        activeJobInfo = ji;

        uiController.SetActiveJobInfo(activeJobInfo);
    }

    public void AddClue(string s)
    {
        cluesFound.Add(s);
    }
}
