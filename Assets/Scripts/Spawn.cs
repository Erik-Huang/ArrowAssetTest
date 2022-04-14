using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cogobyte.ProceduralIndicators;
using Cogobyte.Demo.ProceduralIndicators;

// [AddComponentMenu("Scripts/AutoRotate")]
public class Spawn : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SpawnPathArrow(new Vector3(-1,0,0), new Vector3(1,0,0), 1);
        SpawnCircularArrow(new Vector3(5, 0, 0), 0, 0, 0);
    }

    // 0 degree is (0, 1, 0)
    // Origin is the arrow head
    void SpawnCircularArrow(Vector3 origin, float startAngle, float endAngle, float radius)
    {
        GameObject obj = new GameObject();
        obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        obj.name = "CircularArrowGenerated";

        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        MeshRenderer mr = obj.GetComponent<MeshRenderer>();
        mr.material = Resources.Load("DefaultIndicatorsMaterial", typeof(Material)) as Material;


        obj.AddComponent<UpdatableArrowObject>();
        UpdatableArrowObject arrow = obj.GetComponent<UpdatableArrowObject>();
        arrow.arrowPath = (ArrowPath)AssetDatabase.LoadAssetAtPath("Assets/Resources/MyPointPath.asset", typeof(ArrowPath));
        arrow.arrowTail = (ArrowTip)AssetDatabase.LoadAssetAtPath("Assets/Resources/MyPointPathTip.asset", typeof(ArrowTip));

        arrow.arrowPath.widthFunction = AnimationCurve.Constant(0, 1, 0.05f);
        arrow.arrowPath.heightFunction = AnimationCurve.Constant(0, 1, 0.05f);

        int firstPoint = 0;
        int lastPoint = 40;
        Vector3[] points = new Vector3[40];
        float fi = 0;
        for (int i = firstPoint; i < lastPoint; i++)
        {
            // TODO: This formula should change to represent the radius
            arrow.arrowPath.editedPath[i] = origin + new Vector3(Mathf.Sin(fi), 0, Mathf.Cos(fi));
            fi += 0.1f; // TODO: This value should change to represent the degree.
        }

        arrow.arrowTail.widthFunction = AnimationCurve.Linear(0, 0.5f, 1, 0);
        arrow.arrowTail.heightFunction = AnimationCurve.Linear(0, 0.5f, 1, 0);
        arrow.arrowTail.pathAlongXFunction = AnimationCurve.Constant(0, 1, 0);
        arrow.arrowTail.pathAlongYFunction = AnimationCurve.Constant(0, 1, 0);
        arrow.arrowTail.pathAlongZFunction = AnimationCurve.Constant(0, 1, 0);

        //obj.AddComponent<CurvePathDemo>();
        //CurvePathDemo curvePath = obj.GetComponent<CurvePathDemo>();
        //curvePath.arrowObject = arrow;

        obj.AddComponent<MeshUpdate>();
        obj.GetComponent<MeshUpdate>().arrowObject = obj.GetComponent<ArrowObject>();
    }

    // Start point is where the point is pointing at
    void SpawnPathArrow(Vector3 start, Vector3 end, float curvatureKey)
    {
        GameObject obj = new GameObject();
        obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        obj.name = "PathArrowGenerated";

        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        MeshRenderer mr = obj.GetComponent<MeshRenderer>();
        mr.material = Resources.Load("DefaultIndicatorsMaterial", typeof(Material)) as Material;


        obj.AddComponent<UpdatableArrowObject>();
        UpdatableArrowObject arrow = obj.GetComponent<UpdatableArrowObject>();
        arrow.arrowPath = (ArrowPath)AssetDatabase.LoadAssetAtPath("Assets/Resources/MyCurvePath.asset", typeof(ArrowPath));
        arrow.arrowTail = (ArrowTip)AssetDatabase.LoadAssetAtPath("Assets/Resources/MyCurveTip.asset", typeof(ArrowTip));

        arrow.arrowPath.startPoint = start;
        arrow.arrowPath.endPoint = end;
        arrow.arrowPath.widthFunction = AnimationCurve.Constant(0, 1, 0.05f);
        arrow.arrowPath.heightFunction = AnimationCurve.Constant(0, 1, 0.05f);

        arrow.arrowPath.pathAlongXFunction = AnimationCurve.Constant(0, 1, 0);
        AnimationCurve myCurve = new AnimationCurve();
        myCurve.AddKey(0, 0); // arrowHead
        myCurve.AddKey(0.5f, curvatureKey); // curvature key
        myCurve.AddKey(1, 0);  // arrowTail
        arrow.arrowPath.pathAlongYFunction = myCurve;

        arrow.arrowPath.pathAlongZFunction = AnimationCurve.Constant(0, 1, 0);

        arrow.arrowTail.widthFunction = AnimationCurve.Linear(0, 0.2f, 1, 0);
        arrow.arrowTail.heightFunction = AnimationCurve.Linear(0, 0.2f, 1, 0);
        arrow.arrowTail.pathAlongXFunction = AnimationCurve.Constant(0, 1, 0);
        arrow.arrowTail.pathAlongYFunction = AnimationCurve.Constant(0, 1, 0);
        arrow.arrowTail.pathAlongZFunction = AnimationCurve.Constant(0, 1, 0);

        //obj.AddComponent<CurvePathDemo>();
        //CurvePathDemo curvePath = obj.GetComponent<CurvePathDemo>();
        //curvePath.arrowObject = arrow;

        obj.AddComponent<MeshUpdate>();
        obj.GetComponent<MeshUpdate>().arrowObject = obj.GetComponent<ArrowObject>();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
