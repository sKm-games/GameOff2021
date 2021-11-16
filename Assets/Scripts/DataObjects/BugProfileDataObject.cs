using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BugProfileDataObject", order = 1)]
public class BugProfileDataObject : ScriptableObject
{
    [Header("Basic Info")]
    public string BugID;    
    public EnumDataClass.BugTypeEnum BugType;
    
    [Header("Clue Info")]
    public int BugClueCounts;
    public List<string> BugClues;    
    public List<GameObject> BugCluesSpritesPrfabs;
    public List<Color> BugCluesSpriteColors;
    public List<ParticleSystem> BugClueParticleSystems;
    public List<Color> BugCluesParticleColors;

    [Header("Bug Info")]
    public int BugCount;
    public List<Color> BugSpriteColors;
    public List<GameObject> BugSpritesPrefabs;
    public List<ParticleSystem> BugParticleSystems;
    public List<Color> BugParticleColors;

    [Header("Bugiary Info")]
    public bool BugUnlocked;
    public Sprite BugIcon;
    public string BugName;
    public string BugLatinName;
    public string BugDecription;

}
