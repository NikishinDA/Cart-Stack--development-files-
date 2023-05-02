// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

using System;
using UnityEngine;

public static class GameEventsHandler
{
    public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
    public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
    public static readonly PlayerProgressEvent PlayerProgressEvent = new PlayerProgressEvent();
    public static readonly MoneyCollectEvent MoneyCollectEvent = new MoneyCollectEvent();
    public static readonly CartCollectEvent CartCollectEvent = new CartCollectEvent();
    public static readonly CartDestroyEvent CartDestroyEvent = new CartDestroyEvent();
    public static readonly CartTailCutEvent CartTailCutEvent = new CartTailCutEvent();
    public static readonly CartPropelEvent CartPropelEvent = new CartPropelEvent();
    public static readonly CartStallSellEvent CartStallSellEvent= new CartStallSellEvent();
    public static readonly CartFallEvent CartFallEvent = new CartFallEvent();
    public static readonly ChainTotalCostChangeEvent ChainTotalCostChangeEvent = new ChainTotalCostChangeEvent();
    public static readonly CartSellContentsEvent CartSellContentsEvent = new CartSellContentsEvent();
    public static readonly FinisherStartEvent FinisherStartEvent = new FinisherStartEvent();
    public static readonly FinisherTakeAwayCartEvent FinisherTakeAwayCartEvent = new FinisherTakeAwayCartEvent();
    public static readonly BossDeathEvent BossDeathEvent = new BossDeathEvent();
    public static readonly PlayerTakeDamageEvent PlayerTakeDamageEvent = new PlayerTakeDamageEvent();
    public static readonly PlayerDPSUpgradeEvent PlayerDPSUpgradeEvent = new PlayerDPSUpgradeEvent();
    public static readonly PlayerArmorUpgradeEvent PlayerArmorUpgradeEvent = new PlayerArmorUpgradeEvent();
    public static readonly PlayerMoneyUpgradeEvent PlayerMoneyUpgradeEvent = new PlayerMoneyUpgradeEvent();
    public static readonly PlayerTargetChangeEvent PlayerTargetChangeEvent = new PlayerTargetChangeEvent();
    public static readonly PlayerCheckpointCrossEvent PlayerCheckpointCrossEvent = new PlayerCheckpointCrossEvent();
    public static readonly LevelFinishedSpawningEvent LevelFinishedSpawningEvent = new LevelFinishedSpawningEvent();
    public static readonly EnemyKilledEvent EnemyKilledEvent = new EnemyKilledEvent();
    public static readonly PlayerDamageChangeEvent PlayerDamageChangeEvent = new PlayerDamageChangeEvent();
    public static readonly PlayerMaxArmorChangeEvent PlayerMaxArmorChangeEvent = new PlayerMaxArmorChangeEvent();
    public static readonly LevelSpeedChangeEvent LevelSpeedChangeEvent = new LevelSpeedChangeEvent();
    public static readonly PlayerFinishLevelEvent PlayerFinishLevelEvent = new PlayerFinishLevelEvent();
    public static readonly TutorialShowEvent TutorialShowEvent = new TutorialShowEvent();
    public static readonly TutorialToggleEvent TutorialToggleEvent = new TutorialToggleEvent();
    public static readonly AmbienceChangeEvent AmbienceChangeEvent = new AmbienceChangeEvent();
    public static readonly PlayerModelChangeEvent PlayerModelChangeEvent = new PlayerModelChangeEvent();
    public static readonly DebugCallEvent DebugCallEvent = new DebugCallEvent();
}

public class GameEvent {}

public class GameStartEvent : GameEvent
{
}

public class GameOverEvent : GameEvent
{
    public bool IsWin;
}

public class PlayerProgressEvent : GameEvent
{
    
}

public class MoneyCollectEvent : GameEvent
{
    
}

public class CartCollectEvent : GameEvent
{
    public CartController Cart;
}
public class CartDestroyEvent : GameEvent
{
    public CartController Cart;
}

public class CartTailCutEvent : GameEvent
{
    public CartController Cart;
}

public class CartPropelEvent : GameEvent
{
    public CartController Cart;
    public bool Destroying;
}

public class CartStallSellEvent : GameEvent
{
    public CartController Cart;
}
public class CartFallEvent : GameEvent
{
    public CartController Cart;
}
public class ChainTotalCostChangeEvent : GameEvent
{
    public bool IsLoss;
    public Product Type;
}
public class CartSellContentsEvent : GameEvent
{
    public int Cost;
}
public class FinisherStartEvent : GameEvent
{
    
}
public class PlayerTargetChangeEvent : GameEvent
{
    public Transform Target;
}

public class PlayerTakeDamageEvent : GameEvent
{
    public int Damage;
}


public class PlayerDPSUpgradeEvent : GameEvent
{
    public int Level;
}
public class PlayerArmorUpgradeEvent : GameEvent
{
    public int Level;
}
public class PlayerMoneyUpgradeEvent : GameEvent
{
}
public class FinisherTakeAwayCartEvent : GameEvent
{
    
    public CartController Cart;
}

public class BossDeathEvent : GameEvent
{
}
public class PlayerCheckpointCrossEvent : GameEvent
{
    public int Section;
}

public class LevelFinishedSpawningEvent : GameEvent
{
}


public class EnemyKilledEvent : GameEvent
{
    public Transform Transform;
    public int Cost;
}

public class PlayerDamageChangeEvent : GameEvent
{
    public int Damage;
    public int UpgradeLevel;
}
public class PlayerMaxArmorChangeEvent : GameEvent
{
    public int MaxArmor;
}
public class LevelSpeedChangeEvent: GameEvent
{
    public float Speed;
}
public class  PlayerFinishLevelEvent : GameEvent{}

public class TutorialShowEvent : GameEvent
{
}

public class TutorialToggleEvent : GameEvent
{
    public bool Toggle;
}


public class AmbienceChangeEvent : GameEvent
{
    public int Number;
}
public class PlayerModelChangeEvent : GameEvent
{
    public bool Bin;
}
public class PlayerChangeModelRequestEvent : GameEvent
{
    public bool Bin;
}
public class DebugCallEvent : GameEvent
{
    public float Speed;
    public float Strafe;
}


