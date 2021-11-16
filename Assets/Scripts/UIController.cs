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

    [SerializeField] private GameObject gameHubScreen;
    private GameObject mainHubScreen;
    private GameObject shopHubScreen;
    private GameObject equipmentHubScreen;
    private GameObject jobHubScreen;    

    [SerializeField] private GameObject mainGameScreen;
    private GameObject investigationScreen;
    private CanvasGroup investigationClueScreen;
    private TextMeshProUGUI investigationClueText;

    [SerializeField] private GameObject bugairyScreen;

    private GameObject exterminationScreen;

    private void Awake()
    {
        inputController = this.GetComponent<InputController>();
        bugairyController = this.GetComponent<BugairyController>();
        levelController = this.GetComponent<LevelController>();

        mainHubScreen = gameHubScreen.transform.GetChild(0).gameObject;
        shopHubScreen = gameHubScreen.transform.GetChild(1).gameObject;
        equipmentHubScreen = gameHubScreen.transform.GetChild(2).gameObject;
        jobHubScreen = gameHubScreen.transform.GetChild(3).gameObject;

        investigationScreen = mainGameScreen.transform.GetChild(0).gameObject;
        investigationClueScreen = investigationScreen.transform.GetChild(1).GetComponent<CanvasGroup>();
        investigationClueText = investigationClueScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

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
        shopHubScreen.SetActive(false);
        equipmentHubScreen.SetActive(false);
        jobHubScreen.SetActive(false);
        bugairyScreen.SetActive(false);

        mainGameScreen.SetActive(false);
        investigationScreen.SetActive(false);
        investigationClueScreen.gameObject.SetActive(false);
        investigationClueScreen.alpha = 0;
        investigationClueText.text = "";
        exterminationScreen.SetActive(false);
        bugairyController.CreateBugEntryButtons(levelController.GetBugProfiles);
    }

    public void ToggleGameHubScreen(bool b)
    {   
        gameHubScreen.SetActive(b);
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
    }

    public void ToggleBugairyScreen(bool b)
    {
        bugairyScreen.SetActive(b);
        if (b)
        {
            bugairyController.UpdateBugEntries(levelController.GetBugProfiles);            
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
        investigationScreen.SetActive(b);
        if (b)
        {
            inputController.UpdateGamePhase(EnumDataClass.GamePhasesEnum.Investigation);
        }
    }

    public void ToggleExterminationScreen(bool b)
    {
        exterminationScreen.SetActive(b);
        if (b)
        {
            inputController.UpdateGamePhase(EnumDataClass.GamePhasesEnum.Extermination);
        }
    }

    public void ToggleClueScreen(bool b, string s = "")
    {
        investigationClueText.text = s;
        if (b)
        {
            investigationClueScreen.alpha = 0;
            investigationClueScreen.gameObject.SetActive(true);
            investigationClueScreen.DOFade(1, 0.5f);
        }
        else
        {
            investigationClueScreen.DOFade(0, 0.5f).OnComplete(() => investigationClueScreen.gameObject.SetActive(false));
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
                return investigationClueScreen.gameObject.activeInHierarchy;
        }
    }

}
