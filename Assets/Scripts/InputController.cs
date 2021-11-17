using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private GameController gameController;
    private LevelController levelController;
    private UIController uiController;
    private JobController jobController;

    private void Awake()
    {
        levelController = this.GetComponent<LevelController>();
        uiController = this.GetComponent<UIController>();
        jobController = this.GetComponent<JobController>();
        gameController = this.GetComponent<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (uiController.CheckUIActive("clueScreen"))
            {
                uiController.ToggleClueScreen(false);
            }
            else
            {
                MouseRayCast();
            }            
        }
    }

    private void MouseRayCast()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (rayHit.collider == null)
        {
            //Debug.Log("Rayhit == null");
            return;
        }
        if (gameController.GetGamePhase == EnumDataClass.GamePhasesEnum.Investigation)
        {
            InvestigationTool(rayHit.transform);
        }
        else if (gameController.GetGamePhase == EnumDataClass.GamePhasesEnum.Extermination)
        {
            ExterminateTool(rayHit.transform);
        }
    }

    private void InvestigationTool(Transform t)
    {
        //check tool type etc later
        if (t.tag == "Clue")
        {
            string c = levelController.GetActiveLevelController.GetClue();
            uiController.ToggleClueScreen(true, c);
            uiController.UpdateClues(c);
            t.gameObject.SetActive(false);
        }
    }

    private void ExterminateTool(Transform t)
    {
        //check tool type etc later

    }
}
