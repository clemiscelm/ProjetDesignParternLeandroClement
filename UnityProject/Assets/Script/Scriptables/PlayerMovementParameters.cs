using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerMovementParameters", order = 1)]
public class PlayerMovementParameters : ScriptableObject
{

    #region Run
    [Header("Run")]
    [Range(0, 50)]
    [Tooltip("Vitesse maximale horizontale du joueur")] public float maxSpeed = 5;
    [Range(0, 10)]
    [Tooltip("Temps que prend le joueur a Acc�lerer")] public float accelerationTime = 5;
    [Range(0, 10)]
    [Tooltip("Temps que prend le joueur a D�c�l�rer")] public float decelerationTime = 5;

    [Range(0, 10)]
    public float RotationSpeed = 5;

    public bool normalizeVelocity = true;
    #endregion

    [Header("Attack")]
    [Range(0, 10)]
    public float AttackSpeed = 5;
    [Range(0, 10)]
    public float VelocityPenaltyOnAttack = 0.5f;

    [Header("Hit")]
    [Range(0, 10)]
    public float StaggerTime = 5;

}