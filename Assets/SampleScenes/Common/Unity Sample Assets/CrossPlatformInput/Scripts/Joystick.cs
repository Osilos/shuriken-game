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
using UnityEngine.EventSystems;
using UnitySampleAssets.CrossPlatformInput;

public class Joystick : MonoBehaviour , IPointerUpHandler , IPointerDownHandler , IDragHandler {

    public int MovementRange = 100;

    public enum AxisOption
    {                                                    // Options for which axes to use                                                     
        Both,                                                                   // Use both
        OnlyHorizontal,                                                         // Only horizontal
        OnlyVertical                                                            // Only vertical
    }

    public AxisOption axesToUse = AxisOption.Both;   // The options for the axes that the still will use
    public string horizontalAxisName = "Horizontal";// The name given to the horizontal axis for the cross platform input
    public string verticalAxisName = "Vertical";    // The name given to the vertical axis for the cross platform input 

    private Vector3 startPos;
    private bool useX;                                                          // Toggle for using the x axis
    private bool useY;                                                          // Toggle for using the Y axis
    private CrossPlatformInputManager.VirtualAxis horizontalVirtualAxis;               // Reference to the joystick in the cross platform input
    private CrossPlatformInputManager.VirtualAxis verticalVirtualAxis;                 // Reference to the joystick in the cross platform input
      
    void OnEnable () {

        startPos = transform.position;
        CreateVirtualAxes ();
    }

    private void UpdateVirtualAxes (Vector3 value) {

        var delta = startPos - value;
        delta.y = -delta.y;
        delta /= MovementRange;
        if(useX)
        horizontalVirtualAxis.Update (-delta.x);

        if(useY)
        verticalVirtualAxis.Update (delta.y);

    }

    private void CreateVirtualAxes()
    {
        // set axes to use
        useX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
        useY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

        // create new axes based on axes to use
        if (useX)
            horizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
        if (useY)
            verticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
    }


    public  void OnDrag(PointerEventData data) {

        Vector3 newPos = Vector3.zero;

        if (useX) {
            int delta = (int) (data.position.x - startPos.x);
            delta = Mathf.Clamp(delta,  - MovementRange,  MovementRange);
            newPos.x = delta;
        }

        if (useY)
        {
            int delta = (int)(data.position.y - startPos.y);
            delta = Mathf.Clamp(delta, -MovementRange,  MovementRange);
            newPos.y = delta;
        }
        transform.position = new Vector3(startPos.x + newPos.x , startPos.y + newPos.y , startPos.z + newPos.z);
        UpdateVirtualAxes (transform.position);
    }


    public  void OnPointerUp(PointerEventData data)
    {
        transform.position = startPos;
        UpdateVirtualAxes (startPos);
    }


    public  void OnPointerDown (PointerEventData data) {
    }

    void OnDisable () {
        // remove the joysticks from the cross platform input
        if (useX)
        {
            horizontalVirtualAxis.Remove();
        }
        if (useY)
        {
            verticalVirtualAxis.Remove();
        }
    }
}
