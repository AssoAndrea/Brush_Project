using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrawableItem_SO: ScriptableObject
{
    public TypeOfInk ink;
    public Sprite ImageToDisplay;
    public Texture2D Mask;
    public GameObject PrefabWithCheckPoint;
    public GameObject PrefabToSpawn;

    public List<RectTransform> GetCheckpoints()
    {
        List<RectTransform> points = new List<RectTransform>();
        foreach (RectTransform i in PrefabWithCheckPoint.GetComponentsInChildren<RectTransform>())
        {
            if (!(i.name == PrefabWithCheckPoint.name))
            {
                points.Add(i);
            }

        }
        return points;
    }

}
