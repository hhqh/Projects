namespace Game.Models
{
    /// <summary>
    /// Example of Battle Settings Control
    /// </summary>
    public class BattleSettingsModel
    {
        // The Battle Model (Simple, Map, etc.)
        public BattleModeEnum BattleModeEnum = BattleModeEnum.SimpleNext;

        // Monster always Hit or Miss or Default
        public HitStatusEnum MonsterHitEnum = HitStatusEnum.Default;

        // Characters always Hit or Miss or Default
        public HitStatusEnum CharacterHitEnum = HitStatusEnum.Default;

        // Are Critical Hits Allowed?
        public bool AllowCriticalHit = false;

        // Are Critical Misses Allowed?
        public bool AllowCriticalMiss = false;

        // Can monsters have Items and weapons?
        public bool AllowMonsterItems = false;

        // Can monsters steal items?
        public bool AllowMonstersToStealItems = false;

        // Is Boss Mode Enabled?
        public bool AllowSlimeBoss = false;

        // Can monsters be zombies?
        public bool AllowZombieMonsters = false;

        //Is the order of the round determined by ascending speed?
        public bool ReverseSpeedOrder = false;
        //Var for faking roll for if ReverseSpeedOrder takes effect
        public int ReverseInitiativePercentage = 5;

        public bool AllowMiracleMax = false;

        public bool WeakMonsterSpawn = false;
    }
}