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
    Shop = 6,
    MapChanging = 7,
    Upgrade = 8,
    LoadingGame = 9,
    ChangeScene = 10,
    ManageTeam = 11
}
public enum ParticleType
{
    MeleeHit = 0,
    GunHit = 1,
    ZombieHit = 2,
    SmokeEffect = 3,
    FireEffect = 4,
    ExplosionEffect = 5,
    BlackSmoke1 = 6,
    BlackSmoke2 = 7,
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
    RiffleCarHero = 3,

    ZombieNumOne = 50,
    ZombieNumTwo = 51,
    ZombieRun = 52,
    //ZombieArmor = 53,
    ZombieBoss = 53,

    BusCar = 100,
    Things = 101,

    Shield = 120,
    Pistol = 121,
    Riffle = 122,
    ZombieHand = 123,
    RiffleCar = 124,

    BulletPistol = 200,
    BulletRiffle = 201,

}
public enum MapType
{
    Ground = 0,
    Tree = 1,
    Building = 2
}
public enum MapObjectType
{
    SmallConcrete = 0,
    BigTree = 1,
    FirTree = 2,
    BuildingLevel01 = 3,
    BuildingLevel02 = 4,
    BuildingLevel03 = 5,
    BuildingLevel04 = 6,
    BuildingLevel05 = 7,
    BuildingLevel06 = 8,
    BuildingLevel07 = 9,
    BuildingLevel08 = 10,
    BuildingLevel09 = 11,
    BuildingLevel10 = 12,
}
public class Constant : MonoBehaviour
{
    public const string ANIM_NORMAL = "Normal";
    public const string ANIM_IDLE = "Idle";
    public const string ANIM_WALK = "Walk";
    public const string ANIM_RUN = "Run";
    public const string ANIM_KNEELDOWN = "KneelSitdown";
    public const string ANIM_ATTACK = "Attack";
    public const string ANIM_DEATH = "Death";
    public const string ATTACK_SPEED = "AttackSpeed";
    public const string MOVE_SPEED = "MoveSpeed";
    public const string ANIM_APPEAR = "Appear";
    public const string ANIM_DISSAPPEAR = "Dissappear";
    public const string ANIM_STANDUP = "Standup";
    public const string ANIM_FLYING_BACK = "FlyingBack";
    public const string LAYER_LOCKED_LEVEL = "LockedLevel";
    public const string LAYER_UNLOCKED_LEVEL = "UnlockedLevel";
    public const string INDEX_MAP_LEVEL = "IndexMapLevel";
    public const string COIN = "Coin";
    public const string STAR = "Star";
    public static IdleState IDLE_STATE = new IdleState();
    public static WalkState WALK_STATE = new WalkState();
    public static RunState RUN_STATE = new RunState();
    public static ReloadingState RELOADING_STATE = new ReloadingState();
    public static AttackState ATK_STATE = new AttackState();
    public static StandUpState STANDUP_STATE = new StandUpState();
    public static DeathState DEATH_STATE = new DeathState();
    public static FlyingBackState FLYINGBACKDEATH_STATE = new FlyingBackState();
}
