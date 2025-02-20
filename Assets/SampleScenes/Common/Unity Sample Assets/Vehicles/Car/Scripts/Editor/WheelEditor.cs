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
using UnityEditor;

namespace UnitySampleAssets.Vehicles.Car.Inspector
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof (Wheel))]
    public class WheelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // display the "original" inspector stuff
            base.OnInspectorGUI();


            // and add a button underneath
            if (GUILayout.Button("Align to assigned wheel model"))
            {
                foreach (var target in targets)
                {
                    Wheel wheel = (Wheel) target;
                    wheel.transform.position = wheel.wheelModel.transform.position;
                    var bounds = wheel.wheelModel.GetComponent<Renderer>().bounds;
                    wheel.GetComponent<WheelCollider>().radius = bounds.extents.y;
                }
            }
        }
    }
}