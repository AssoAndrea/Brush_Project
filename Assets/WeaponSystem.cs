using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { NoWeapon,Sword,Hammer,Bow,Last}

public class WeaponSystem : MonoBehaviour
{
    public RedItem WeaponHandle;
    [HideInInspector]
    public RedItem OldWeapon;
    public GameObject SwordOBJ;
    public Animator SwordAnim;
    public GameObject HammerOBJ;
    public Animator HammerAnim;
    public GameObject BowOBJ;
    public Animator BowAnim;
    public GameObject BowRotObj;
    public GameObject ArrowPrefab;
    public float FireRate = 0.5f;
    float timer;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            case RedItem.NoWeapon:
                break;
            case RedItem.Sword:
                Sword();
                break;
            case RedItem.Hammer:
                Hammer();
                break;
            case RedItem.Bow:
                Bow();
                break;
        }

        
    }
    void ResetWeapons(RedItem type)
    {
        switch (type)
        {
            case RedItem.NoWeapon:
                SwordOBJ.SetActive(false);
                BowOBJ.SetActive(false);
                HammerOBJ.SetActive(false);
                break;
            case RedItem.Sword:
                SwordOBJ.SetActive(true);
                BowOBJ.SetActive(false);
                HammerOBJ.SetActive(false);
                break;
            case RedItem.Hammer:
                SwordOBJ.SetActive(false);
                BowOBJ.SetActive(false);
                HammerOBJ.SetActive(true);
                break;
            case RedItem.Bow:
                SwordOBJ.SetActive(false);
                BowOBJ.SetActive(true);
                HammerOBJ.SetActive(false);
                break;
        }
    }

    public void Sword()
    {
        if (Input.GetMouseButton(0) && Game_Manager.instance.inventory.DrawSpaceOpen == false)
            SwordAnim.SetBool("Attack",true);
        else
            SwordAnim.SetBool("Attack",false);
    }

    public void Hammer()
    {
        if (Input.GetMouseButton(0) && Game_Manager.instance.inventory.DrawSpaceOpen == false)
            HammerAnim.SetBool("Attack",true);
        else
            HammerAnim.SetBool("Attack", false);

    }

    public void Bow()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir =  mousePos - rb.position;
        Vector2 normDir = lookDir.normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        BowRotObj.transform.rotation =Quaternion.Euler(new Vector3(0,0,angle));

        if (Input.GetMouseButton(0) && BowAnim.GetBool("Return") == true && Game_Manager.instance.inventory.DrawSpaceOpen == false)
        {
            BowAnim.SetBool("Fire", true);
            BowAnim.SetBool("Return", false);

            GameObject go = Instantiate(ArrowPrefab, BowRotObj.transform.position+new Vector3(normDir.x,normDir.y,0)*1, BowRotObj.transform.rotation);
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            go.GetComponent<Rigidbody2D>().AddForce(normDir*500);
        }
        else
        {
            BowAnim.SetBool("Fire", false);

        }

        if (BowAnim.GetBool("Return") == false)
        {
            timer += Time.deltaTime;
            if (timer >= FireRate)
            {
                timer = 0;
                BowAnim.SetBool("Return", true);
            }
        }
    }
}
