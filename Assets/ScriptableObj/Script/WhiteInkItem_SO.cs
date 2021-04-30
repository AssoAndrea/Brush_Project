using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WhiteInkItem", menuName = "DrawableItem/WhiteInkItem")]
public class WhiteInkItem_SO : DrawableItem_SO
{
    //public TypeOfInk ink = TypeOfInk.Red;
    public WhiteItem TypeOfItem;


}
public enum WhiteItem { Platform, toAdd_1, toAdd_2, to_Add_3, Last }
