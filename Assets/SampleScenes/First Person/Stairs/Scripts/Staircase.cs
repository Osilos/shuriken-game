﻿/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.2 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.2

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using UnityEngine;
using System.Collections;

public class Staircase : MonoBehaviour
{
    public Transform stepObject;
    public MeshCollider[] accurateColliders;
    public MeshCollider[] smoothColliders;

    public void SetStepWidth(float stepWidth)
    {
        transform.localScale = new Vector3(stepWidth, transform.localScale.y, transform.localScale.z);
    }
    
    public void SetStepHeight(float stepHeight)
    {
        stepObject.localScale = new Vector3(stepObject.localScale.x, stepHeight, stepObject.localScale.z);
    }
    
    public void SetStepDepth(float stepDepth)
    {
        stepObject.localScale = new Vector3(stepObject.localScale.x, stepObject.localScale.y, stepDepth);
    }

    public void SetStepAngle(float stepAngle)
    {
        // TODO Set angle
    }

    public void SetStepSmoothCollider(bool isSmooth)
    {
        for (int i = 0; i < accurateColliders.Length; i++)
        {
            MeshCollider accurateCollider = accurateColliders [i];
            accurateCollider.enabled = !isSmooth;
        }
        for (int i = 0; i < smoothColliders.Length; i++)
        {
            MeshCollider smoothCollider = smoothColliders [i];
            smoothCollider.enabled = isSmooth;
        }
    }
}
