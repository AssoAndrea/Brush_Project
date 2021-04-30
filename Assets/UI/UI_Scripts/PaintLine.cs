using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaintLine : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;
    public Transform parent;
    public AnimationCurve WidthCurve;
    public bool canDraw;

    LineRenderer currentLineRenderer;
    GameObject brushInstance;
    Vector2 lastPos;

    private void Update()
    {
        Drawing();
    }

    void Drawing()
    {
        if (canDraw)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CreateBrush();
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                if (brushInstance == null)
                {
                    CreateBrush();
                }
                PointToMousePos();
            }
        }
        else
        {
            StopDraw();
        }
    }
    public void StopDraw()
    {
        Destroy(brushInstance);
        currentLineRenderer = null;
    }

    void CreateBrush()
    {
        brushInstance = Instantiate(brush);
        brushInstance.transform.SetParent(parent);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //because you gotta have 2 points to start a line renderer, 
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
        currentLineRenderer.widthCurve = WidthCurve;
    }

    void AddAPoint(Vector2 pointPos)
    {
        
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }

}
