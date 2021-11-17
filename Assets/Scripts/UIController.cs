using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    private InputController inputController;
    private BugairyController bugairyController;
    private LevelController levelController;
    private JobController jobController;

    [SerializeField] private GameObject gameHubScreen;
    
    private GameObject mainHubScreen;
    private GameObject activeJobScreen;
    private TextMeshProUGUI activeJobNameText;
    private TextMeshProUGUI activeJobDescriptionText;
    private Image activeJobIcon;
    private GameObject mainHubCluesScreen;
    private TextMeshProUGUI mainHubCluesText;
    private Button mainHubJobsButton;

    private GameObject shopHubScreen;
    private GameObject equipmentHubScreen;
    private GameObject jobHubScreen;    

    [SerializeField] private GameObject mainGameScreen;
    private GameObject investigationScreen;
    private CanvasGroup investigationNewClueScreen;
    private TextMeshProUGUI investigationNewClueText;
    private TextMeshProUGUI investigationCluesText;

    [SerializeField] private GameObject bugairyScreen;

    private GameObject exterminationScreen;

    private void Awake()
    {
        inputController = this.GetComponent<InputController>();
        bugairyController = this.GetComponent<BugairyController>();
        levelController = this.GetComponent<LevelController>();
        jobController = this.GetComponent<JobController>();

        mainHubScreen = gameHubScreen.transform.GetChild(0).gameObject;
        activeJobScreen = mainHubScreen.transform.GetChild(3).gameObject;
        activeJobNameText = activeJobScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        activeJobDescriptionText = activeJobScreen.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        activeJobIcon = activeJobScreen.transform.GetChild(4).GetComponent<Image>();
        mainHubCluesScreen = mainHubScreen.transform.GetChild(4).gameObject;
        mainHubCluesText = mainHubCluesScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        mainHubJobsButton = mainHubScreen.transform.GetChild(2).GetChild(0).GetComponent<Button>();

        shopHubScreen = gameHubScreen.transform.GetChild(1).gameObject;
        equipmentHubScreen = gameHubScreen.transform.GetChild(2).gameObject;
        jobHubScreen = gameHubScreen.transform.GetChild(3).gameObject;

        investigationScreen = mainGameScreen.transform.GetChild(0).gameObject;
        investigationNewClueScreen = investigationScreen.transform.GetChild(1).GetComponent<CanvasGroup>();
        investigationNewClueText = investigationNewClueScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        investigationCluesText = investigationScreen.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>();

        exterminationScreen = mainGameScreen.transform.GetChild(1).gameObject;
    }
    
    void Start()
    {
        DefaultLayout();
    }

    private void DefaultLayout()
    {
        gameHubScreen.SetActive(true);
        mainHubScreen.SetActive(true);
        activeJobScreen.SetActive(false);
        mainHubCluesScreen.SetActive(false);
        mainHubCluesText.text = "";
        shopHubScreen.SetActive(false);
        equipmentHubScreen.SetActive(false);
        jobHubScreen.SetActive(false);
        bugairyScreen.SetActive(false);

        mainGameScreen.SetActive(false);
        investigationScreen.SetActive(false);
        investigationNewClueScreen.gameObject.SetActive(false);
        investigationNewClueScreen.alpha = 0;
        investigationNewClueText.text = "";
        exterminationScreen.SetActive(false);
        bugairyController.CreateBugEntryButtons(jobController.GetBugProfiles);
    }

    public void SetActiveJobInfo(JobInfoClass ji)
    {
        string s = ji.ClientName + "\n" + ji.ClientAddress;
        activeJobNameText.text = s;
        s = ji.JobBugProfile.BugJobDescription[0] + "\nPayment: " + ji.JobBugProfile.BugJobBasePayment;
        activeJobDescriptionText.text = s;

        activeJobIcon.sprite = ji.ClientIcon;

        activeJobScreen.SetActive(true);

        ToggleJobHubScreen(false);

        levelController.SpawnTestLevel();
        mainHubJobsButton.interactable = false;
    }

    public void ToggleGameHubScreen(bool b)
    {   
        gameHubScreen.SetActive(b);
    }

    public void ToggleMainHubCluesScreen(bool b)
    {
        mainHubCluesScreen.SetActive(b);
    }

    public void ToggleMainHubScreen(bool b)
    {
        mainHubScreen.SetActive(b);
    }

    public void ToggleShopHubScreen(bool b)
    {
        shopHubScreen.SetActive(b);
        mainHubScreen.SetActive(!b);
    }

    public void ToggleEquipmentHubScreen(bool b)
    {
        equipmentHubScreen.SetActive(b);
        mainHubScreen.SetActive(!b);
    }

    public void ToggleJobHubScreen(bool b)
    {
        jobHubScreen.SetActive(b);
        mainHubScreen.SetActive(!b);
        if (b)
        {
            jobController.CreateJobs();
        }
    }

    public void ToggleBugairyScreen(bool b)
    {
        bugairyScreen.SetActive(b);
        if (b)
        {
            bugairyController.UpdateBugEntries(jobController.GetBugProfiles);            
        }
        /*if (CheckUIActive("mainGameScreen"))
        {
            mainGameScreen.SetActive(!b);
        }
        else if (CheckUIActive("gameHubScreen"))
        {
            mainHubScreen.SetActive(!b);
        }*/
    }

    public void ToggleInvestigationScreen(bool b)
    {
        mainGameScreen.SetActive(b);
        investigationScreen.SetActive(b);
        gameHubScreen.SetActive(!b);
        if (b)
        {
            investigationCluesText.text = "";                        
        }
    }

    public void UpdateClues(string c)
    {
        jobController.AddClue(c);
        string s = "- " + c +"\n";
        investigationCluesText.text += s;
        mainHubCluesText.text += s;
    }

    public void ToggleExterminationScreen(bool b)
    {
        exterminationScreen.SetActive(b);
        if (b)
        {
            levelController.SetUpExterminationPhase();            
        }
    }

    public void ToggleClueScreen(bool b, string s = "")
    {
        investigationNewClueText.text = s;
        if (b)
        {
            investigationNewClueScreen.alpha = 0;
            investigationNewClueScreen.gameObject.SetActive(true);
            investigationNewClueScreen.DOFade(1, 0.5f);
        }
        else
        {
            investigationNewClueScreen.DOFade(0, 0.5f).OnComplete(() => investigationNewClueScreen.gameObject.SetActive(false));
        } 
    }

    public bool CheckUIActive(string id)
    {
        switch (id)
        {
            default:
                Debug.LogError("UIController: CheckUIActive: Invalid ID: " +id);
                return false;
            case "investigationScreen":
                return investigationScreen.activeInHierarchy;
            case "clueScreen":
                return investigationNewClueScreen.gameObject.activeInHierarchy;
        }
    }

}
