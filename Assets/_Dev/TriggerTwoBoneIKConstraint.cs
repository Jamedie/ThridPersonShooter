using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TriggerTwoBoneIKConstraint : MonoBehaviour
{
    [SerializeField] private float TimeToRotate = 10f;
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;

    private TwoBoneIKConstraint rig;
    private float targetWeight;

    // Start is called before the first frame update
    private void Awake()
    {
        rig = GetComponent<TwoBoneIKConstraint>();
    }

    private void Update()
    {
        rig.weight = Mathf.Lerp(rig.weight, targetWeight, TimeToRotate);

        //if (starterAssetsInputs.aim)
        //{
        //    targetWeight = 1f;
        //}
        //else
        //{
        //    targetWeight = 0f;
        //}
    }
}