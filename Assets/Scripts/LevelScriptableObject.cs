using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "Scriptable Objects/LevelScriptableObject")]
public class LevelScriptableObject : ScriptableObject
{
    public EnemyScriptableObject[] enemyTypes;
    public float enemies;
    public float NextLevelMoney;
    public Sprite Background;
}
