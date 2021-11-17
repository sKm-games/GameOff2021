using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private UIController uiController;
    private JobController jobController;
    [SerializeField] private Transform levelHolder;
    [SerializeField] private GameObject levelPresetPrefabs;
    private LevelPresetController activeLevelController;
    public LevelPresetController GetActiveLevelController
    {
        get
        {
            return activeLevelController;
        }
    }


    private void Awake()
    {
        uiController = this.GetComponent<UIController>();
        jobController = this.GetComponent<JobController>();
    }
    
    private void Start()
    {
        SpawnTestLevel();
    }

    
    private void Update()
    {
        //Testing
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetUpInvestigationPhase(jobController.GetActiveJobInfo);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetUpExterminationPhase();
        }
    }

    public void SpawnTestLevel()
    {
        GameObject go = Instantiate(levelPresetPrefabs, levelHolder);
        go.transform.localPosition = Vector3.zero;
        
        activeLevelController = go.GetComponent<LevelPresetController>();

        activeLevelController.SetUpLevel();
        uiController.ToggleInvestigationScreen(false);
        uiController.ToggleExterminationScreen(false);
    }

    public void GoToJob()
    {
        
    }

    public void SetUpInvestigationPhase(JobInfoClass ji)
    {
        Debug.Log("LevelController:SetUpInvestigation");
        activeLevelController.SetUpInvestigationPhase(ji.JobBugProfile);        
    }

    public void SetUpExterminationPhase()
    {
        activeLevelController.LevelCleanUp();
        activeLevelController.SetUpExterminatePhase();
    }
}
