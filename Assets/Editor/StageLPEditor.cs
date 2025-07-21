using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(StageLP))]
public class StageLPEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("LP 감소", EditorStyles.boldLabel);

        StageLP stageLP = (StageLP)target;

        if (GUILayout.Button("LP Down"))
        {
            stageLP.LPdown();
        }

        EditorGUILayout.Space();
        DrawDefaultInspector();
    }
}

[CustomEditor(typeof(StageManager))]
public class StageManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("테스트 버튼", EditorStyles.boldLabel);
        StageManager stageManager = (StageManager)target;

        if (GUILayout.Button("StartGame"))
        {
            stageManager.StartGame();
        }

        if (GUILayout.Button("NextFloor"))
        {
            stageManager.NextFloor();
        }

        if (GUILayout.Button("StartNextMiniGame"))
        {
            stageManager.StartNextMiniGame();
        }
        EditorGUILayout.Space();
        DrawDefaultInspector();
    }
}

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("테스트 버튼", EditorStyles.boldLabel);
        GameManager gameManager = (GameManager)target;
        if (GUILayout.Button("스테미나 추가"))
        {
            gameManager.stamina.AddStamina();
        }

        if (GUILayout.Button("스테미나 감소"))
        {
            gameManager.stamina.UseStamina();
        }

        EditorGUILayout.Space();
        DrawDefaultInspector();
    }
}