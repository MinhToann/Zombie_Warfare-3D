using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu = 0,
    Gameplay = 1,
    Win = 2,
    Lose = 3,
    Setting = 4,
    Map = 5,
    Shop = 6
}
public enum PoolType
{
    Heroes = 0,
    Zombies = 1,
    Towers = 2,
    GameIcon = 3,
    Weapon = 4,
    Bullet = 5,
}
public enum GameObjectType
{
    MeleeHero = 0,
    PistolHero = 1,
    RiffleHero = 2,

    ZombieNumOne = 50,
    ZombieNumTwo = 51,

    BusCar = 100,
    Things = 101,

    Shield = 120,
    Pistol = 121,
    Riffle = 122,
    ZombieHand = 123,

    BulletPistol = 200,
    BulletRiffle = 201,

}
public class Constant : MonoBehaviour
{
    public const string ANIM_WALK = "Walk";
    public const string ANIM_RUN = "Run";
    public const string ANIM_ATTACK = "Attack";
    public const string ANIM_DEATH = "Death";
    public const string ATTACK_SPEED = "AttackSpeed";
    public const string ANIM_WAVE_TEXT = "Appear";
    public static IdleState IDLE_STATE = new IdleState();
    public static WalkState WALK_STATE = new WalkState();
    public static RunState RUN_STATE = new RunState();
    public static ReloadingState RELOADING_STATE = new ReloadingState();
    public static AttackState ATK_STATE = new AttackState();
    public static StandUpState STANDUP_STATE = new StandUpState();
    public static DeathState DEATH_STATE = new DeathState();
}
