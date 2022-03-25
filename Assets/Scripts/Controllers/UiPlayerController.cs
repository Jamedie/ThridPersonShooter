using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPlayerController : MonoBehaviour
{
    public RadialMenu radialMenu;
    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonShooterController;

    // Start is called before the first frame update

    private void Awake()
    {
        starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
        thirdPersonShooterController = FindObjectOfType<ThirdPersonController>();
        radialMenu.OnValidate += OnRadialMenuValidate;
    }

    // Update is called once per frame
    private void Update()
    {
        //if (starterAssetsInputs.use)
        //{
        //    radialMenu.Toggle();
        //    starterAssetsInputs.use = false;
        //    thirdPersonShooterController.SetAllowRotateCamera(!radialMenu.isOpen);
        //    starterAssetsInputs.SetCursorState(!radialMenu.isOpen);
        //    thirdPersonShooterController.SetRotationMove(!radialMenu.isOpen);
        //}
    }

    private void OnRadialMenuValidate(RadialMenuEntry selectedEntry)
    {
        radialMenu.Toggle();
    }
}