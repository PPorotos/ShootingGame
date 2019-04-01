using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUICameraRaycast : MonoBehaviour
{

    RaycastHit hit;
    IPointerClickHandler clickHandler;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 50f))
            {
                if(hit.collider.tag == "BUTTON")
                {
                    clickHandler = hit.collider.gameObject.GetComponent<IPointerClickHandler>();
                    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                    clickHandler.OnPointerClick(pointerEventData);
                }
                Debug.Log("ray");
            }

        }

    }
}
