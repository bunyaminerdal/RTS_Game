using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/UnitStats")]
public class UnitStats : ScriptableObject {
    public float health;
    public int armour;
    public int attackSpeed;
    public int attackRange;
    public int attackDamage;
    public float gatheringSpeed;
}
