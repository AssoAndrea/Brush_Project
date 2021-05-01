using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Platforms
{
    public class platforms : MonoBehaviour
    {
        //public mouse KeyPressed;
        bool isFreezed, isSelected;
        Vector3 clickedMouse;

        float xMin, xMax, offsetX;


        // Start is called before the first frame update
        void Start()
        {
            isFreezed = false;
            offsetX = transform.localScale.x * 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdatePosition(Transform trs)
        {
            UpdatePosition(trs.position);
        }

        public void UpdatePosition(Vector3 position)
        {
            transform.position = position;
        }

        public void MoveTillInputFreeze()
        {
            if (Input.GetMouseButtonDown(0) && !isFreezed)
            {
                isFreezed = true;
            }
            else if (!isFreezed)
            {
                clickedMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickedMouse.z = 0;
                UpdatePosition(clickedMouse);
            }
        }
    }
}