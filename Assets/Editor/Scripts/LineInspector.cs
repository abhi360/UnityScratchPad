using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor
{
    private const float handleSize = 0.04f;
    private const float pickSize = 0.06f;

    private int selectedIndex = 0;

    private void OnSceneGUI()
    {
        Line line = target as Line;
        Transform handleTransform = line.transform;
        Vector3 p0 = handleTransform.TransformPoint(line.p0);
        Vector3 p1 = handleTransform.TransformPoint(line.p1);

        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Handles.color = Color.white;
        Handles.DrawLine(p0, p1);

        float size = HandleUtility.GetHandleSize(p0);
        if (Handles.Button(p0, handleRotation, size * handleSize, size * pickSize, Handles.DotCap))
        {
            selectedIndex = 0;
            Repaint();
        }
        if (Handles.Button(p1, handleRotation, size * handleSize, size * pickSize, Handles.DotCap))
        {
            selectedIndex = 1;
            Repaint();
        }

        if (selectedIndex == 0)
        {
            EditorGUI.BeginChangeCheck();
            p0 = Handles.DoPositionHandle(p0, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(line, "Move Point");
                EditorUtility.SetDirty(line);
                line.p0 = handleTransform.InverseTransformPoint(p0);
            }
        }

        if (selectedIndex == 1)
        {
            EditorGUI.BeginChangeCheck();
            p1 = Handles.DoPositionHandle(p1, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(line, "Move Point");
                EditorUtility.SetDirty(line);
                line.p1 = handleTransform.InverseTransformPoint(p1);
            }
        }
    }
}
