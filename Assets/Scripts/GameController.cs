using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private UIController uiController;
    private LevelController levelController;
    private JobController jobController;
    [SerializeField] private EnumDataClass.GamePhasesEnum gamePhase;
    public EnumDataClass.GamePhasesEnum GetGamePhase
    {
        get
        {
            return gamePhase;
        }
    }

    private void Awake()
    {
        uiController = this.GetComponent<UIController>();
        levelController = this.GetComponent<LevelController>();
        jobController = this.GetComponent<JobController>();
    }

    public void StartInvestigationPhase()
    {
        gamePhase = EnumDataClass.GamePhasesEnum.Investigation;
        uiController.ToggleInvestigationScreen(true);
        levelController.SetUpInvestigationPhase(jobController.GetActiveJobInfo);
    }

    public void EndInvestigationPhase()
    {
        uiController.ToggleInvestigationScreen(false);
        uiController.ToggleMainHubCluesScreen(true);
    }

    public void StartExterminationPhase()
    {
        gamePhase = EnumDataClass.GamePhasesEnum.Extermination;        
        uiController.ToggleExterminationScreen(true);
    }

    public void EndExterminationPhase()
    {
        gamePhase = EnumDataClass.GamePhasesEnum.Hub;
        //Do scoring
    }


}
