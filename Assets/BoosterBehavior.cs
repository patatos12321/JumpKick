using UnityEngine;

public class BoosterBehavior : MonoBehaviour
{
    public int nbFramePerLight = 3;
    public int MaxBoostLevel => BoosterLights.Length;
    public float Multiplicator => currentBoostLevel / (float)MaxBoostLevel;
    public GameObject[] BoosterLights;
    public GameObject MiniFire;
    public GameObject MegaFire;
    private int currentNbFrames = 0;
    private int currentBoostLevel = 0;
    private int nbBoostedFrames = 0;
    private bool showMegaFlame = false;

    private void Start()
    {
        nbBoostedFrames = 10;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        currentNbFrames++;
        if (NeedsToUpgradeBoostLevel())
        {
            currentBoostLevel++;
            currentNbFrames = 0;
        }

        for (int i = 0; i < BoosterLights.Length; i++)
        {
            BoosterLights[i].SetActive(i < currentBoostLevel);
        }
        if (nbBoostedFrames < 5)
        {
            if (showMegaFlame)
            {
                MegaFire.SetActive(true);
            }
            else
            {
                MegaFire.SetActive(false);
                MiniFire.SetActive(true);
            }
            nbBoostedFrames++;
        }
        else
        {
            MegaFire.SetActive(false);
            MiniFire.SetActive(false);
        }
    }

    private bool NeedsToUpgradeBoostLevel()
    {
        return currentNbFrames >= nbFramePerLight && currentBoostLevel < MaxBoostLevel;
    }

    public void Boost()
    {
        showMegaFlame = currentBoostLevel == MaxBoostLevel;

        currentNbFrames = 0;
        currentBoostLevel = 0;
        nbBoostedFrames = 0;
    }
}
