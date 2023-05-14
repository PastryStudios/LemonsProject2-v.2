using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RTSController : MonoBehaviour
{
    public Vector3 startPosition;
    private List<UnitController> selectedUnitRTSList;
    private BuildingController selectedBuildingList;
    private List<EnemyUnit> selectedEnemyRTSList;
    [SerializeField] private Transform selectedAreaTransform;
    [SerializeField] LayerMask RaycastMask = ~0;

    private void Awake()
    {
        selectedUnitRTSList = new List<UnitController>();
        selectedBuildingList = BuildingManager.Instance.GetHQBuilding();
        selectedEnemyRTSList = new List<EnemyUnit>();
        selectedAreaTransform.gameObject.SetActive(false);
    }

    public void Update()
    {
        #region Left Button
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //Unit Selection
            startPosition = MousePosition.MouseWorldPosition();
            selectedAreaTransform.gameObject.SetActive(true);

        }
        //Button Held
        if (Mouse.current.leftButton.isPressed)
        {
            //Selection Area
            Vector3 currentMousePosition = MousePosition.MouseWorldPosition();
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y));
            Vector3 upperRight = new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y));
            selectedAreaTransform.position = lowerLeft;
            selectedAreaTransform.localScale = upperRight - lowerLeft;
        }

        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            selectedAreaTransform.gameObject.SetActive(false);
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, MousePosition.MouseWorldPosition());
            //Collider2D[] collider2DArray = Physics2D.Raycast(startPosition, -Vector2.up, 6);

            foreach(UnitController unitRTS in selectedUnitRTSList)
            {
                unitRTS.SetSelectedVisible(false);
            }
            foreach(EnemyUnit enemyRTS in selectedEnemyRTSList)
            {
                enemyRTS.SetHealthbarVisible(false);
            }

            selectedUnitRTSList.Clear();
            selectedEnemyRTSList.Clear();

            foreach (Collider2D collider2D in collider2DArray)
            {
                UnitController unitRTS = collider2D.GetComponent<UnitController>();
                if(unitRTS != null)
                {
                    selectedUnitRTSList.Add(unitRTS);
                    unitRTS.SetSelectedVisible(true);
                }
                BuildingTypeHolder buildingRTS = collider2D.GetComponent<BuildingTypeHolder>();
                if(buildingRTS != null)
                {
                    
                }
                EnemyUnit enemyRTS = collider2D.GetComponent<EnemyUnit>();
                if(enemyRTS != null)
                {
                    enemyRTS.SetHealthbarVisible(true);
                    Debug.Log("Enemy in list");
                    selectedEnemyRTSList.Add(enemyRTS);
                }
            }

            //Debug.Log(selectedUnitRTSList.Count);
            //Debug.Log(selectedEnemyRTSList.Count);
        }
        #endregion

        #region Right Button

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //Command
            //Ray ray = Camera.main.ScreenPointToRay(MousePosition.MouseWorldPosition());
            RaycastHit2D hit = Physics2D.Raycast(MousePosition.MouseWorldPosition(), Vector2.zero);//, QueryTriggerInteraction.Ignore);
            //Debug.Log(hit.collider.gameObject.name);
            if(hit.collider != null)
            {
                #region Attack
                if (hit.collider.CompareTag("Target"))
                {
                    Debug.Log("Attack!");
                    Vector3 attackPosition = MousePosition.MouseWorldPosition();
                    List<Vector3> attackPositionList =
                        GetPositionListAround(attackPosition, new float[] { 3f, 4f, 6f }, new int[] { 10, 20, 30 });

                    //unitRTS.UnitMoveToAttackState.MoveTo;
                    int attackPositionListIndex = 0;
                    foreach (UnitController unitRTS in selectedUnitRTSList)
                    {
                        //Move command
                        //unitRTS.MoveTo(targetPosition);
                        unitRTS.MoveToAttack(attackPositionList[attackPositionListIndex]);
                        attackPositionListIndex = (attackPositionListIndex + 1) % attackPositionList.Count;
                    }

                }
                #endregion
            }
            #region Move Command
            else if (hit.collider == null)
            {
                Debug.Log("Move.");
                Vector3 moveToPosition = MousePosition.MouseWorldPosition();
                List<Vector3> targetPositionList =
                    GetPositionListAround(moveToPosition, new float[] { 2f, 4f, 6f }, new int[] { 10, 20, 30 });

                int targetPositionListIndex = 0;
                foreach (UnitController unitRTS in selectedUnitRTSList)
                {
                    //Move command
                    //unitRTS.UnitMoveState.MoveTo(moveToPosition);
                    unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
                    targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
                }
            }
            #endregion
           
        }
        //Button Held
        if (Mouse.current.rightButton.isPressed)
        {
            //Command Menu

        }

        if(Mouse.current.rightButton.wasReleasedThisFrame)
        {

        }
        #endregion

    }
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {
            positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for(int i =0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }
    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }
}
