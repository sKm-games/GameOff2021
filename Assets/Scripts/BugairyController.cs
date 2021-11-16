using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugairyController : MonoBehaviour
{
    [SerializeField] private Transform bugairyEntriesHolder;
    
    [SerializeField] private GameObject bugairyEntryPrefab;
    private List<BugairyEntryObject> bugairyEntrys;

    public void CreateBugEntryButtons(List<BugProfileDataObject> bugs)
    {
        bugairyEntrys = new List<BugairyEntryObject>();
        foreach (BugProfileDataObject bp in bugs)
        {
            GameObject go = Instantiate(bugairyEntryPrefab, bugairyEntriesHolder);
            BugairyEntryObject be = go.GetComponent<BugairyEntryObject>();
            bugairyEntrys.Add(be);            
            be.SetEntryInfo(bp);            
        }
    }

    public void UpdateBugEntries(List<BugProfileDataObject> bugs)
    {
        for (int i = 0; i < bugairyEntrys.Count; i++)
        {
            bugairyEntrys[i].SetEntryInfo(bugs[i]);
        }
    }
}
