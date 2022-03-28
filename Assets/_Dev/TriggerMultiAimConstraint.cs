using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TriggerMultiAimConstraint : MonoBehaviour
{
    [SerializeField] private float TimeToRotate = 10f;
    [SerializeField] private LocomotionAssetsInputs starterAssetsInputs;
    [SerializeField] private LayerMask aimColliderLayerMak;

    [SerializeField] private bool displayLineRenderer = false;

    private MultiAimConstraint rig;
    private float targetWeight;

    private Camera _mainCamera;
    private Vector2 screenCenterPoint;

    private Transform _head;
    private Transform _target;

    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        rig = GetComponent<MultiAimConstraint>();
        _mainCamera = Camera.main;
        screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        _target = rig.data.sourceObjects[0].transform;
        _head = rig.data.constrainedObject;

        TryGetComponent<LineRenderer>(out lineRenderer);
        if (displayLineRenderer == false && lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    private void Update()
    {
        rig.weight = Mathf.Lerp(rig.weight, targetWeight, TimeToRotate);

        HeadFeedBack();

        //if (starterAssetsInputs.aim)
        //{
        //    targetWeight = 1f;
        //}
        //else
        //{
        //    targetWeight = 0f;
        //}
    }

    private void HeadFeedBack()
    {
        Ray ray = _mainCamera.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMak))
        {
            _target.position = raycastHit.point;
        }
        else
        {
            _target.position = ray.GetPoint(20f);
        }

        if (lineRenderer && displayLineRenderer)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, _head.position);
            lineRenderer.SetPosition(1, _target.position);
        }
    }
}