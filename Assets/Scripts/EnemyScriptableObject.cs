using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public float Health;
    public float Damage;
    public Sprite Sprite;
}
