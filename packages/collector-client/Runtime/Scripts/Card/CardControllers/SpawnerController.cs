using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Enums;

/// <summary>
/// Здесь описывается логика поведения карты:
/// - Инициализация данных карты
/// - Изменение TimeCost
/// - Изменения CardState
/// ! Логика боевки должна быть отдельно, не здесь.
/// </summary>
public class SpawnerController : CardEntity
{
    List<string> creaturesToSpawn;
    int timerUntilSpawn = 0;
    bool repeatSpawn = false;


    public void InitiateController(List<string> creaturesToSpawn, bool repeatSpawn)
    {
        this.creaturesToSpawn = creaturesToSpawn;
        if (abilitiesAndStatus.ContainsKey(CardAbility.SpawnCreature))
            timerUntilSpawn = abilitiesAndStatus[CardAbility.SpawnCreature];
        this.repeatSpawn = repeatSpawn;
    }

    public override void TurnEnd(bool isPlayer, int row, int column)
    {
        base.TurnEnd(isPlayer, row, column);
    }

    public override void TurnStart()
    {
        base.TurnStart();
        Debug.Log(abilitiesAndStatus.Keys);
        if (abilitiesAndStatus.ContainsKey(CardAbility.SacrificeSpawn))
            SacrificeSpawn();
        if (abilitiesAndStatus.ContainsKey(CardAbility.SpawnCreature))
            SpawnAddionalCreatures();

    }

    void SacrificeSpawn() //Kill self and spawn stuff
    {
        if (abilitiesAndStatus[CardAbility.SacrificeSpawn] == 0)
        {
            if (creaturesToSpawn.Count == 1)
            {
                var replacementCreature = CreatureSpawner.instance.spawnCreatureByName(creaturesToSpawn[0], isEnemyEntity);
                replacementCreature.transform.position = transform.position;
                replacementCreature.transform.SetParent(gameBoardRegulator.transform, true);
                replacementCreature.transform.localScale = transform.localScale;
                cellHost.SetCardinCell(replacementCreature);
                Destroy(gameObject);
                return;
            }
            else
            {
                foreach (string creature in creaturesToSpawn) //summon all creatures and then kill ourself
                {
                    CreatureSpawner.instance.spawnCreatureByNameOnField(creature, isEnemyEntity);
                }
                cellHost.DestroyCardinCell();
            }

        }
        Debug.Log(abilitiesAndStatus[CardAbility.SacrificeSpawn].ToString());
        if (abilitiesAndStatus[CardAbility.SacrificeSpawn] > 0)
            abilitiesAndStatus[CardAbility.SacrificeSpawn]--;
        Debug.Log(abilitiesAndStatus[CardAbility.SacrificeSpawn].ToString());
    }

    void SpawnAddionalCreatures() //just spawn stuff whenever ability potency reaches 0
    {
        if (abilitiesAndStatus[CardAbility.SpawnCreature] == 0) // "Creatures / cooldown in turns"
        {
            foreach (string creature in creaturesToSpawn)
            {
                CreatureSpawner.instance.spawnCreatureByNameOnField(creature, isEnemyEntity);
            }
            if (repeatSpawn)
                abilitiesAndStatus[CardAbility.SpawnCreature] = timerUntilSpawn;
        }
        else
            abilitiesAndStatus[CardAbility.SpawnCreature]--;

    }

}
