using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScroller : MonoBehaviour
{
    readonly float areaSize_Z = 30.0f;

    Vector3 basePosition;
    StageControl control;

    private void Start()
    {
        control = StageControl.Instance;
        basePosition = transform.position;
    }
    private void Update()
    {
        var positionZ = Mathf.Round((control.transform.position.z - basePosition.z) / areaSize_Z);

        var nowPosition = this.transform.position;
        nowPosition.z = areaSize_Z * positionZ + basePosition.z;
        transform.position = nowPosition;
    }


}
