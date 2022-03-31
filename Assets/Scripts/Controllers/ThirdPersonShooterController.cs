using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [Header("Character Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderMask;
    [SerializeField] private Transform targetTransform;

    [Header("Weapons Settings")]
    [SerializeField] private Gun currentGun;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private bool _hasAnimator;
    private Animator _animator;
    private int _animIDAiming;
    private int _animIDFire;

    private Camera _mainCamera;
    private Vector2 _screenCenterPoint;

    // Start is called before the first frame update
    private void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();

        _hasAnimator = TryGetComponent(out _animator);
        AssignAnimationIDs();

        _mainCamera = Camera.main;
        _screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    private void AssignAnimationIDs()
    {
        _animIDAiming = Animator.StringToHash("Aiming");
        _animIDFire = Animator.StringToHash("Fire");
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Ray ray = _mainCamera.ScreenPointToRay(_screenCenterPoint);
        mouseWorldPosition = ray.GetPoint(20);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            targetTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (starterAssetsInputs.aim)
        {
            _animator.SetBool(_animIDAiming, true);
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            thirdPersonController.SetRotationMove(false);

            if (currentGun.currentGunStats.GunWeaponType == WeaponType.Pistol)
            {
                _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 50f));
            }
            if (currentGun.currentGunStats.GunWeaponType == WeaponType.Rifle)
            {
                _animator.SetLayerWeight(2, 1f);
            }

            //Can Shoot if aimed
            if (starterAssetsInputs.shoot)
            {
                currentGun.Shoot(mouseWorldPosition);

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetTrigger(_animIDFire);
                }

                if (currentGun.currentGunStats.GunWeaponType == WeaponType.Pistol)
                {
                    starterAssetsInputs.shoot = false;
                }
                if (currentGun.currentGunStats.GunWeaponType == WeaponType.Rifle)
                {
                }
            }
        }
        else
        {
            _animator.SetBool(_animIDAiming, true);
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotationMove(true);

            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 50f));
            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 50f));
        }
    }
}