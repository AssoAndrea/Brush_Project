using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { NoWeapon,Sword,Hammer,Bow,Last}

public class WeaponSystem : MonoBehaviour
{
    public WeaponType WeaponHandle;
    public WeaponType OldWeapon;
    public GameObject SwordOBJ;
    public Animator SwordAnim;
    public GameObject HammerOBJ;
    public Animator HammerAnim;
    public GameObject BowOBJ;
    public Animator BowAnim;


    // Start is called before the first frame update
    void Start()
    {
        OldWeapon = WeaponHandle;
        ResetWeapons(WeaponHandle);

    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponHandle != OldWeapon)
        {
            ResetWeapons(WeaponHandle);
            OldWeapon = WeaponHandle;
        }

        switch (WeaponHandle)
        {
            case WeaponType.NoWeapon:
                break;
            case WeaponType.Sword:
                Sword();
                break;
            case WeaponType.Hammer:
                Hammer();
                break;
            case WeaponType.Bow:
                Bow();
                break;
            case WeaponType.Last:
                break;
            default:
                break;
        }

        
    }
    void ResetWeapons(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.NoWeapon:
                SwordOBJ.SetActive(false);
                BowOBJ.SetActive(false);
                HammerOBJ.SetActive(false);
                break;
            case WeaponType.Sword:
                SwordOBJ.SetActive(true);
                BowOBJ.SetActive(false);
                HammerOBJ.SetActive(false);
                break;
            case WeaponType.Hammer:
                SwordOBJ.SetActive(false);
                BowOBJ.SetActive(false);
                HammerOBJ.SetActive(true);
                break;
            case WeaponType.Bow:
                SwordOBJ.SetActive(false);
                BowOBJ.SetActive(true);
                HammerOBJ.SetActive(false);
                break;
            case WeaponType.Last:
                break;
            default:
                break;
        }
    }

    public void Sword()
    {
        if (Input.GetMouseButton(0))
            SwordAnim.SetBool("Attack",true);
        else
            SwordAnim.SetBool("Attack",false);
    }

    public void Hammer()
    {
        if (Input.GetMouseButton(0))
            HammerAnim.SetBool("Attack",true);
        else
            HammerAnim.SetBool("Attack", false);

    }

    public void Bow()
    {
        if (Input.GetMouseButton(0))
        {
            SwordAnim.SetTrigger("Attack");
        }
    }
}
