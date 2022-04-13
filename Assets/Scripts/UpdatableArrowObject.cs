using Cogobyte.ProceduralLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cogobyte.ProceduralIndicators
{
    //Used to view arrow objects in play mode
    //Drag this script on empty object and add a path asset, and optional head and tail arrow tip assets.
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class UpdatableArrowObject : ArrowObject
    {
        void LateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                updateArrow();
                Debug.Log("Arrow is updated.");
                Vector3 newStartPoint = new Vector3(Random.Range(-1, 1), Random.Range(-1, -1), Random.Range(-1, 1));
                arrowPath.startPoint = newStartPoint;
            }
            
        }

        void updateArrow()
        {
            arrowIndicator = ScriptableObject.CreateInstance<ArrowIndicator>();
            if (arrowPath == null)
            {
                arrowIndicator.arrowPath = ScriptableObject.CreateInstance<ArrowPath>();
                arrowPath = arrowIndicator.arrowPath;
                arrowIndicator.arrowPath.arrowPathMode = ArrowPath.ArrowPathMode.None;
            }
            arrowIndicator.arrowPath = arrowPath;
            if (arrowHead == null)
            {
                arrowHead = ScriptableObject.CreateInstance<ArrowTip>();
                arrowHead.arrowTipMode = ArrowTip.ArrowTipMode.None;
            }
            if (arrowTail == null)
            {
                arrowTail = ScriptableObject.CreateInstance<ArrowTip>();
                arrowTail.arrowTipMode = ArrowTip.ArrowTipMode.None;
            }
            arrowIndicator.arrowPath.arrowHead = arrowHead;
            arrowIndicator.arrowPath.arrowTail = arrowTail;
            if (permanentScriptableObjects)
            {
                if (arrowIndicator.arrowPath.arrowHead == arrowIndicator.arrowPath.arrowTail)
                {
                    arrowIndicator.arrowPath.arrowTail = Instantiate(arrowIndicator.arrowPath.arrowTail);
                    arrowTail = arrowIndicator.arrowPath.arrowTail;
                }
            }
            else
            {
                arrowIndicator.arrowPath.arrowHead = Instantiate(arrowIndicator.arrowPath.arrowHead);
                arrowHead = arrowIndicator.arrowPath.arrowHead;
                arrowIndicator.arrowPath.arrowTail = Instantiate(arrowIndicator.arrowPath.arrowTail);
                arrowTail = arrowIndicator.arrowPath.arrowTail;
            }
            if (flatShading)
            {
                arrowIndicator.extrudeObject = ScriptableObject.CreateInstance<FlatShadeExtrude>();
            }
            else
            {
                arrowIndicator.extrudeObject = ScriptableObject.CreateInstance<ComplexExtrude>();
            }
        }
    }
}