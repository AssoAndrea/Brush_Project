using System.Collections;
using UnityEngine;

namespace Scripts.Platforms
{
    public class PrefabInstantiator : MonoBehaviour
    {
        [SerializeField] public GameObject PlatformPrefab;
        [SerializeField] public KeyCode AddPPrefabKey;

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
            InstantiatePrefab();
            MovePrefab();
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
            if (canAddPrefab  && Input.GetKeyDown(AddPPrefabKey))
            {
                canAddPrefab = false;

                Debug.Log("Add platform");
                prefab = Instantiate(PlatformPrefab, this.transform);
                ConfigColliderTF(true);
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
                prefab.transform.position = clickedMouse;

                if (Input.GetMouseButtonDown(0))
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
    }
}