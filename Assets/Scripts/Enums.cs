using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ���� Enum
/// </summary>
public static class Enums
{
    public enum CardType
    {
        Creature = 0,
        Spell,
        Artifact,
        Field
    }

    public enum CardRarity
    {
        Common = 0,
        Rare,
        Epic,
        Legendary
    }

    public enum CardUniverse
    {
        Dragons = 0,
        Pirates,
        Circus
    }

    /// <summary>
    /// ��������� ����� �� ������� ����. � ����\�� �����.
    /// </summary>
    public enum CardState
    {
        OnHand = 0,
        OnBoard
    }

    public enum CardAbility
    {
        DefaultVerticalLinearAttack = 0,
        DefaultHorizontalLinearAttack,
        EveryoneAttack,
        Sleep,
        IgniteCreature,
        Ignited


    }
}
