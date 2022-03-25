using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonInteractController : MonoBehaviour
{
    [SerializeField] private LayerMask interactableColliderLayerMak;
    [SerializeField] private float rangeInteract = 0.5f;
    [SerializeField] private LocomotionAssetsInputs locomotionAssetsInputs;

    [SerializeField] private Transform PlayerCameraRoot;

    public InteractableObject _selectedObject;

    private Camera _mainCamera;
    private Vector2 _screenCenterPoint;

    // Start is called before the first frame update
    private void Start()
    {
        _mainCamera = Camera.main;
        _screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    // Update is called once per frame
    private void Update()
    {
        SearchInteractables();
    }

    private void SearchInteractables()
    {
        Ray ray = _mainCamera.ScreenPointToRay(_screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, interactableColliderLayerMak))
        {
            InteractableObject currentObject;
            raycastHit.transform.TryGetComponent<InteractableObject>(out currentObject);

            if (currentObject != null && GetDistanceFromPlayer(raycastHit.transform) < rangeInteract)
            {
                if (currentObject == _selectedObject && locomotionAssetsInputs.use)
                {
                    Debug.Log("Use " + raycastHit.transform.name);
                    locomotionAssetsInputs.use = false;
                    currentObject.UseInteractable();
                }
                else if (currentObject != _selectedObject)
                {
                    LeavInteractable();
                    EnterInteractable(currentObject);
                }
            }
            else
            {
                LeavInteractable();
            }
        }
        else
        {
            Debug.DrawRay(PlayerCameraRoot.position, PlayerCameraRoot.TransformDirection(Vector3.forward) * 10, Color.white);
            LeavInteractable();
        }
    }

    private void LeavInteractable()
    {
        if (_selectedObject == null)
        {
            return;
        }

        Debug.Log("Leav interactable");
        _selectedObject.LeavOverInteractable();
        _selectedObject = null;
    }

    private void EnterInteractable(InteractableObject newInteractable)
    {
        Debug.Log("Inter interactable");
        _selectedObject = newInteractable;
        _selectedObject.OverInteractable();
    }

    private float GetDistanceFromPlayer(Transform hitPosition)
    {
        return Vector3.Distance(hitPosition.position, transform.position);
    }
}