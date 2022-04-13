// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Utilities
{
    /// <summary>
    /// A simple line with two points.
    /// </summary>
    [AddComponentMenu("Scripts/AdjustableLineDataProvider")]
    public class AdjustableLineDataProvider : BaseMixedRealityLineDataProvider
    {
        [Tooltip("The starting point of this line.")]
        [SerializeField]
        private MixedRealityPose startPoint = MixedRealityPose.ZeroIdentity;

        /// <summary>
        /// The starting point of this line which is always located at the GameObject's transform position
        /// </summary>
        /// <remarks>Always located at this <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>'s <see href="https://docs.unity3d.com/ScriptReference/Transform-position.html">Transform.position</see></remarks>
        public MixedRealityPose StartPoint => startPoint;

        //[SerializeField]
        //[Tooltip("The point where this line will end.\nNote: Start point is always located at the GameObject's transform position.")]
        private MixedRealityPose endPoint = new MixedRealityPose(Vector3.down, Quaternion.identity);

        private float yIncr = 1.0f;
        /// <summary>
        /// The point where this line will end.
        /// </summary>
        public MixedRealityPose EndPoint
        {
            get => endPoint;
            set => endPoint = value;
        }

        #region Line Data Provider Implementation

        /// <inheritdoc />
        public override int PointCount => 2;

        /// <inheritdoc />
        protected override Vector3 GetPointInternal(int pointIndex)
        {

            switch (pointIndex)
            {
                case 0:
                    return StartPoint.Position;
                case 1:
                    return EndPoint.Position;
                default:
                    Debug.LogError("Invalid point index");
                    return Vector3.zero;
            }
        }

        /// <inheritdoc />
        protected override void SetPointInternal(int pointIndex, Vector3 point)
        {
            switch (pointIndex)
            {
                case 0:
                    startPoint.Position = point;
                    break;
                case 1:
                    endPoint.Position = point;
                    Debug.Log("Pos set to " + point);
                    break;
                default:
                    Debug.LogError("Invalid point index");
                    break;
            }
        }

        void Update()
        {
            yIncr = (yIncr + 0.005f) % 2.0f;
            transform.Translate(Vector3.left * 0.3f * Time.deltaTime);
            //startPoint.Position = new Vector3(0, 0f + yIncr, 0);
            endPoint.Position = new Vector3(0, 0.5f + yIncr, 0);
        }

        /// <inheritdoc />
        protected override Vector3 GetPointInternal(float normalizedDistance)
        {
            return startPoint.Position + normalizedDistance * endPoint.Position;
            //return Vector3.Lerp(StartPoint.Position, EndPoint.Position, normalizedDistance);
        }

        /// <inheritdoc />
        protected override float GetUnClampedWorldLengthInternal()
        {
            return Vector3.Distance(StartPoint.Position, EndPoint.Position);
        }

        /// <inheritdoc />
        protected override Vector3 GetUpVectorInternal(float normalizedLength)
        {
            return transform.up;
        }

        #endregion Line Data Provider Implementation
    }
}