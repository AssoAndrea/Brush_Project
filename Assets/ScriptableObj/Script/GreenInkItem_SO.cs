using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GreenInkItem", menuName = "DrawableItem/GreenInkItem")]
public class GreenInkItem_SO : DrawableItem_SO
{
    public GreenItem TypeOfItem;
}
public enum GreenItem { Stairs,Trampoline,toAdd_1,toAdd_2,Last}
