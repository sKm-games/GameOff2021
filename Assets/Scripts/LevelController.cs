using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private UIController uiController;
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
    [SerializeField] private List<BugProfileDataObject> bugProfiles;
    public List<BugProfileDataObject> GetBugProfiles
    {
        get
        {
            return bugProfiles;
        }
    }

    private void Awake()
    {
        uiController = this.GetComponent<UIController>();
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
            SetUpInvestigationPhase();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetUpExterminationPhase();
        }
    }

    private void SpawnTestLevel()
    {
        GameObject go = Instantiate(levelPresetPrefabs, levelHolder);
        go.transform.localPosition = Vector3.zero;
        
        activeLevelController = go.GetComponent<LevelPresetController>();

        activeLevelController.SetUpLevel();
        uiController.ToggleInvestigationScreen(false);
        uiController.ToggleExterminationScreen(false);
    }

    private void SetUpInvestigationPhase()
    {
        List<BugProfileDataObject> tempBugs = new List<BugProfileDataObject>(bugProfiles);
        tempBugs.Shuffle(); //temp so dont change main list

        activeLevelController.SetUpInvestigationPhase(tempBugs[0]);
        uiController.ToggleInvestigationScreen(true);
    }

    private void SetUpExterminationPhase()
    {
        activeLevelController.LevelCleanUp();
        activeLevelController.SetUpExterminatePhase();
        
        uiController.ToggleInvestigationScreen(false);
        uiController.ToggleExterminationScreen(true);
    }
}
