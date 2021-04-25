using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrawableItem_SO: ScriptableObject
{
    public Sprite ImageToDisplay;
    public Texture2D Mask;
    public GameObject Prefab;
    public ItemType itemType;

    public List<RectTransform> GetCheckpoints()
    {
        List<RectTransform> points = new List<RectTransform>();
        foreach (RectTransform i in Prefab.GetComponentsInChildren<RectTransform>())
        {
            if (!(i.name == Prefab.name))
            {
                points.Add(i);
            }

        }
        return points;
    }

}
