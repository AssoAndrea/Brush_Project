using System.Collections;
using UnityEngine;

namespace Scripts.Platforms
{
    public class PrefabInstantiator : MonoBehaviour
    {
        [SerializeField] public GameObject PlatformPrefab;
        [SerializeField] public KeyCode AddPPrefabKey;
        public float LerpSpeed = 50;

        bool canAddPrefab;
        GameObject prefab;
        Vector3 clickedMouse;

        // Use this for initialization
        void Start()
        {
            canAddPrefab = true;
        }

        // Update is called once per frame
        void Update()
        {
            //InstantiatePrefab();
            MovePrefab();

            if (Input.GetKeyDown(KeyCode.X))
            {
                DeleteChild();
            }
        }

        public void SelectPrefab()
        {
            if (Game_Manager.instance.inventory.InkToUse != TypeOfInk.Red)
            {
                PlatformPrefab = Game_Manager.instance.inventory.ItemToDraw.PrefabToSpawn;

                InstantiatePrefab();
            }
        }

        public void InstantiatePrefab()
        {
            if (canAddPrefab)  //&& Input.GetKeyDown(AddPPrefabKey))
            {
                canAddPrefab = false;

                Debug.Log("Add platform");
                prefab = Instantiate(PlatformPrefab, this.transform);
                ConfigColliderTF(false);
                //prefab.GetComponent<Collider2D>().enabled = false;
            }
   
        }

        public void UpdatePosition(Transform trs)
        {
            UpdatePosition(trs.position);
        }

        public void UpdatePosition(Vector3 position)
        {
            transform.position = position;
        }

        private void MovePrefab()
        {
            if (prefab != null)
            {
                clickedMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickedMouse.z = 0;
                prefab.transform.position = Vector3.Lerp(prefab.transform.position,clickedMouse,LerpSpeed*Time.deltaTime);

                if (Input.GetMouseButtonDown(1))
                {
                    //prefab.GetComponent<Collider2D>().enabled = true;
                    ConfigColliderTF(true);
                    prefab = null;
                    canAddPrefab = true;
                }
            }
        }

        private void ConfigColliderTF(bool truefalse)
        {
            prefab.GetComponent<Collider2D>().enabled = truefalse;
        }

        public void DeleteChild()
        {
            int numChild = transform.childCount;

            if (Game_Manager.instance.inventory.InkToUse == TypeOfInk.White)
            {
                // ciclo sui figli alla ricerca del primo avente tag bianco per cancellarlo
                for (int i = 0; i < numChild; i++)
                {
                    if (transform.GetChild(i).tag == "WhiteInk")
                    {
                        float ink = transform.GetChild(i).gameObject.GetComponent<platforms>().InkAmount;
                        Game_Manager.instance.inventory.AddInk(TypeOfInk.White, ink);
                        
                        //Debug.Log($"Distruggo figlio {i} colore BIANCO");
                        Destroy(transform.GetChild(i).gameObject);
                        break;
                    }
                }
            }
            else if (Game_Manager.instance.inventory.InkToUse == TypeOfInk.Green)
            {
                // ciclo sui figli alla ricerca del primo avente tag verde per cancellarlo
                for (int i = 0; i < numChild; i++)
                {
                    if (transform.GetChild(i).tag == "GreenInk")
                    {    
                        if(transform.GetChild(i).gameObject.name == "LeafStairs(Clone)")
                        {
                            float ink = transform.GetChild(i).gameObject.GetComponent<StairsScript>().InkAmount;
                            Game_Manager.instance.inventory.AddInk(TypeOfInk.Green, ink);                            
                        }
                        else
                        {
                            float ink = transform.GetChild(i).gameObject.GetComponent<Trampolino>().InkAmount;
                            Game_Manager.instance.inventory.AddInk(TypeOfInk.Green, ink);                            
                        }

                        //Debug.Log($"Distruggo figlio {i} colore VERDE");
                        Destroy(transform.GetChild(i).gameObject);
                        break;
                    }
                }
            }
        }
    }
}