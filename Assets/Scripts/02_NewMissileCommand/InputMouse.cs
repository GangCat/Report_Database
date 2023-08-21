using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour
{
    private InputMouse() { }

    private static InputMouse instance = null;

    public static InputMouse Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("[S] Input Mouse");
                instance = go.AddComponent<InputMouse>();
            }
            return instance;
        }
    }

    public bool Picking(string _tag, ref Vector3 _point, int _layerMaskIdx = ~0)
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, _layerMaskIdx))
        {
            if (hit.transform.CompareTag(_tag))
            {
                _point = hit.point;
                return true;
            }
        }
        return false;
    }
}