using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BugairyEntryObject : MonoBehaviour
{
    private TextMeshProUGUI bugNameText;
    private TextMeshProUGUI bugInfoText;
    private Image bugIcon;

    private void Awake()
    {
        bugNameText = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        bugInfoText = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        bugIcon = this.transform.GetChild(1).GetComponent<Image>();
    }

    public void SetEntryInfo(BugProfileDataObject  bp)
    {
        if (bugNameText == null)
        {
            bugNameText = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            bugInfoText = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            bugIcon = this.transform.GetChild(1).GetComponent<Image>();
        }

        if (bp.BugUnlocked)
        {
            bugNameText.text = bp.BugName + "\n<size=50%><i>" + bp.BugLatinName;
            bugInfoText.text = bp.BugDecription;
            bugIcon.sprite = bp.BugIcon;
        }
        else
        {
            bugNameText.text = "?????\n<size=50%><i>???";
            bugInfoText.text = "?????????\n??????????????";            
        }
    }
}
