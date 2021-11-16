using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPresetController : MonoBehaviour
{
    [SerializeField] private List<Color> floorColors;
    private List<SpriteRenderer> floorRenderer;
    
    [SerializeField] private List<Color> wallColors;
    private List<SpriteRenderer> wallRenderer;

    [SerializeField] private int furnitureCount;
    [SerializeField] private List<Sprite> furnitureSprites;
    [SerializeField] private List<Color> furnitureColors;
    

    private List<SpriteRenderer> spawnPoints;
    private BugProfileDataObject bugProfile;

    private List<GameObject> cleanUpObjects;

    private void Awake()
    {        
        floorRenderer = new List<SpriteRenderer>(this.transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>());
        wallRenderer = new List<SpriteRenderer>(this.transform.GetChild(1).GetComponentsInChildren<SpriteRenderer>());
        spawnPoints = new List<SpriteRenderer>(this.transform.GetChild(2).GetComponentsInChildren<SpriteRenderer>());
        cleanUpObjects = new List<GameObject>();
    }

    public void SetUpLevel()
    {
        LevelCleanUp();

        foreach (SpriteRenderer sp in floorRenderer)
        {
            floorColors.Shuffle();
            sp.color = floorColors[0];
        }
        

        foreach (SpriteRenderer sp in wallRenderer)
        {
            //wallColors.Shuffle();
            sp.color = wallColors[0];
        }
        

        foreach (SpriteRenderer sp in spawnPoints)
        {
            sp.enabled = false;
        }

        
        spawnPoints.Shuffle();
        int furnitureIndex = 0;        
        
        for (int i = 0; i < furnitureCount; i++)
        {
            if (i >= spawnPoints.Count)
            {
                break;
            }

            if (furnitureIndex >= furnitureSprites.Count-1)
            {
                furnitureIndex = 0;
            }
            
            furnitureSprites.Shuffle();
            spawnPoints[i].sprite = furnitureSprites[0];
            
            furnitureColors.Shuffle();
            
            spawnPoints[i].color = furnitureColors[0];
            spawnPoints[i].enabled = true;            
        }

        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        Camera.main.transform.position = pos;
    }

    public void SetUpInvestigationPhase(BugProfileDataObject bp)
    {        
        bugProfile = bp;

        spawnPoints.Shuffle();
        bugProfile.BugCluesSpritesPrfabs.Shuffle();
        bugProfile.BugClueParticleSystems.Shuffle();

        int spriteIndex = 0;

        int particleIndex = 0;

        for (int i = 0; i < bugProfile.BugClueCounts; i++)
        {
            if (i >= spawnPoints.Count)
            {
                break;
            }
            int r = Random.Range(0, 2);            
            if (r == 0) //sprite clue
            {
                Debug.Log("Spawn sprite clue " + i); 
                if (spriteIndex >= bugProfile.BugCluesSpritesPrfabs.Count-1)
                {
                    spriteIndex = 0;               
                }
                GameObject go = Instantiate(bugProfile.BugCluesSpritesPrfabs[spriteIndex], spawnPoints[i].transform);
                cleanUpObjects.Add(go);
                spriteIndex++;
                go.transform.name = "CluesSprites, " + i;
                SpriteRenderer[] sps = go.GetComponentsInChildren<SpriteRenderer>();
                bugProfile.BugCluesSpriteColors.Shuffle();
                Debug.Log("Set sprite clue color");
                foreach (SpriteRenderer sp in sps)
                {
                    sp.color = bugProfile.BugCluesSpriteColors[0];
                    float zRot = Random.Range(0, 4);
                    zRot *= 90;

                    go.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, zRot));
                }
            }
            else
            {
                Debug.Log("Spawn particle clue " + i);
                if (particleIndex >= bugProfile.BugClueParticleSystems.Count-1)
                {
                    particleIndex = 0;
                }

                ParticleSystem ps = Instantiate(bugProfile.BugClueParticleSystems[particleIndex], spawnPoints[i].transform);
                ps.transform.localPosition = new Vector3(0f, 0f, -5f);
                cleanUpObjects.Add(ps.gameObject);
                particleIndex++;
                ps.transform.name = "CluesParticles, " + i;

                bugProfile.BugClueParticleSystems.Shuffle();
                Debug.Log("Set particle clue color");
                ParticleSystem.MainModule main = ps.main;
                main.startColor = bugProfile.BugCluesParticleColors[0];              
            }
        }
    }

    public string GetClue()
    {
        bugProfile.BugClues.Shuffle();
        return bugProfile.BugClues[0];
    }

    public void SetUpExterminatePhase()
    {
        spawnPoints.Shuffle();
        bugProfile.BugSpritesPrefabs.Shuffle();
        bugProfile.BugParticleSystems.Shuffle();

        int spriteIndex = 0;       

        int particleIndex = 0;        

        for (int i = 0; i < bugProfile.BugCount; i++)
        {
            if (i >= spawnPoints.Count)
            {
                break;
            }
            int r = Random.Range(0, 2);
            if (r == 0) //sprite clue
            {
                if (spriteIndex >= bugProfile.BugSpritesPrefabs.Count-1)
                {
                    spriteIndex = 0;
                }

                GameObject go = Instantiate(bugProfile.BugSpritesPrefabs[spriteIndex], spawnPoints[i].transform);
                go.transform.name = "BugSprites, " + i;
                cleanUpObjects.Add(go);
                spriteIndex++;

                SpriteRenderer[] sps = go.GetComponentsInChildren<SpriteRenderer>();
                bugProfile.BugSpriteColors.Shuffle();
                foreach (SpriteRenderer sp in sps)
                {
                    sp.color = bugProfile.BugSpriteColors[0];
                    
                    float zRot = Random.Range(0, 4);
                    zRot *= 90;

                    go.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, zRot));
                }                
            }
            else
            {
                if (particleIndex >= bugProfile.BugParticleSystems.Count-1)
                {
                    particleIndex = 0;
                }

                ParticleSystem ps = Instantiate(bugProfile.BugParticleSystems[particleIndex], spawnPoints[i].transform);
                ps.transform.localPosition = new Vector3(0f, 0f, -5f);
                cleanUpObjects.Add(ps.gameObject);
                particleIndex++;
                ps.transform.name = "BugParticles, " + i;

                bugProfile.BugParticleColors.Shuffle();
                ParticleSystem.MainModule main = ps.main;
                main.startColor = bugProfile.BugParticleColors[0];
            }
        }
    }

    public void LevelCleanUp()
    {        
        foreach (GameObject go in cleanUpObjects)
        {
            Destroy(go);
        }

        cleanUpObjects = new List<GameObject>();
    }
}
