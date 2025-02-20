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

namespace UnitySampleAssets.Utility
{

    public class DynamicShadowSettings : MonoBehaviour
    {

        public Light sunLight;
        public float minHeight = 10;
        public float minShadowDistance = 80;
        public float minShadowBias = 1;
        public float maxHeight = 1000;
        public float maxShadowDistance = 10000;
        public float maxShadowBias = 0.1f;
        public float adaptTime = 1;

        private float smoothHeight;
        private float changeSpeed;
        private float originalStrength = 1;

        private void Start()
        {
            originalStrength = sunLight.shadowStrength;
        }

        // Update is called once per frame
        private void Update()
        {

            Ray ray = new Ray(Camera.main.transform.position, -Vector3.up);
            RaycastHit hit;
            float height = transform.position.y;
            if (Physics.Raycast(ray, out hit))
            {
                height = hit.distance;
            }

            if (Mathf.Abs(height - smoothHeight) > 1)
            {
                smoothHeight = Mathf.SmoothDamp(smoothHeight, height, ref changeSpeed, adaptTime);
            }


            float i = Mathf.InverseLerp(minHeight, maxHeight, smoothHeight);

            QualitySettings.shadowDistance = Mathf.Lerp(minShadowDistance, maxShadowDistance, i);
            sunLight.shadowBias = Mathf.Lerp(minShadowBias, maxShadowBias, 1 - ((1 - i)*(1 - i)));
            sunLight.shadowStrength = Mathf.Lerp(originalStrength, 0, i);

        }
    }
}