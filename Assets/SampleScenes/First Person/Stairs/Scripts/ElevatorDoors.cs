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

public class ElevatorDoors : MonoBehaviour
{
    public Transform doorL;
    public Transform doorR;
    public float doorOpenDistance;
    public bool isClosed;
    public float speed;
    private Vector3 doorLInitialPosition;
    private Vector3 doorRInitialPosition;

    void Awake()
    {
        doorLInitialPosition = doorL.position;
        doorRInitialPosition = doorR.position;
    }

    public IEnumerator MoveDoor(bool openDoor)
    {
        Vector3 doorLDestination, doorRDestination;
        if (openDoor)
        {
            isClosed = false;
            doorLDestination = new Vector3(doorLInitialPosition.x + doorOpenDistance, doorL.position.y, doorL.position.z);
            doorRDestination = new Vector3(doorRInitialPosition.x - doorOpenDistance, doorR.position.y, doorR.position.z);
        } 
        else
        {
            doorLDestination = new Vector3(doorLInitialPosition.x, doorL.position.y, doorL.position.z);
            doorRDestination = new Vector3(doorRInitialPosition.x, doorR.position.y, doorR.position.z);
        }

        while (!Mathf.Approximately(doorL.position.x, doorLDestination.x))
        {
            doorL.position = Vector3.MoveTowards(doorL.position, doorLDestination, speed * Time.deltaTime);
            doorR.position = Vector3.MoveTowards(doorR.position, doorRDestination, speed * Time.deltaTime);
            yield return null;
        }
        if (!openDoor)
        {
            isClosed = true;
        }
    }
}