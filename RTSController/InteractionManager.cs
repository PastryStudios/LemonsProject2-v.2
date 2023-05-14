using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] List<TestUnit> SelectedCharacters = new List<TestUnit>();
    [SerializeField] float MaxRayCastDistance = 9.25f;
    [SerializeField] LayerMask RaycastMask = ~0; //~0 returns "Everything" Layer Mask

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && SelectedCharacters.Count > 0)
        {
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ProcessClickCommand();

        }
    }

    void ProcessClickCommand()
    {
        //Convert Mouse position to rac
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log(cameraRay);
        RaycastHit hitInfo;
        if(Physics.Raycast(cameraRay, out hitInfo, MaxRayCastDistance, RaycastMask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
        }
    }

}
