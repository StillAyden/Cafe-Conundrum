using System.Collections.Generic;
using UnityEngine;

public class SwitchLights : MonoBehaviour
{

    public static SwitchLights Instance;

    public Light poeple;

    public Texture2D[] darkLightmapDir, darkLightmapColor;

    public Texture2D[] brightLightmapDir, brightLightmapColor;

    private LightmapData[] darkLightmap, brightLightmap;


    private void Start()
    {
        Instance = this;

        //Dark LightMap
        List<LightmapData> dlightmap = new List<LightmapData>();

        for (int i = 0; i < darkLightmapDir.Length; i++)
        {
            LightmapData lmData = new LightmapData();

            lmData.lightmapDir = darkLightmapDir[i];
            lmData.lightmapColor = darkLightmapColor[i];
        
            dlightmap.Add(lmData);
        }

        darkLightmap = dlightmap.ToArray();


        //Bright LightMap
        List<LightmapData> blightmap = new List<LightmapData>();

        for (int i = 0; i < brightLightmapDir.Length; i++)
        {
            LightmapData lmData = new LightmapData();

            lmData.lightmapDir = brightLightmapDir[i];
            lmData.lightmapColor = brightLightmapColor[i];

            blightmap.Add(lmData);
        }

        brightLightmap = blightmap.ToArray();

    }

    public void LightsOn()
    {
        LightmapSettings.lightmaps = brightLightmap;
        poeple.intensity = 1;
    }

    public void LightsOff()
    {
        LightmapSettings.lightmaps = darkLightmap;
        poeple.intensity = 0.1f;
    }
}
