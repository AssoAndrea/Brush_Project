using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrawableItem_SO: ScriptableObject
{
    public TypeOfInk ink;
    public Sprite ImageToDisplay;
    public Texture Mask;
    public GameObject Prefab;

    public RectTransform[] GetCheckPointsAndLastPoint(out RectTransform lastPoint)
    {
        lastPoint = new RectTransform();
        List<RectTransform> points = new List<RectTransform>();
        foreach (RectTransform i in Prefab.GetComponentsInChildren<RectTransform>())
        {
            if (i.tag == "Checkpoint")
            {
                points.Add(i);
            }
            else
            {
                lastPoint = i;
            }
        }

        if (lastPoint == null)
        {
            Debug.LogError("lastPoint non trovato");
        }
        RectTransform[] res = points.ToArray();
        return res;
    }
}
