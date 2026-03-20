using System;
using UnityEngine;

namespace SubDrone.Data
{
    [Serializable]
    public sealed class MissionObjective
    {
        public string id;
        public string description;

        [Tooltip("Tipo do objetivo. Ex.: ScanSpecies, CollectSample, MapArea, RecoverItem.")]
        public string type;

        [Tooltip("Quantidade necessária. Ex.: 3 scans.")]
        public int targetCount = 1;

        [Tooltip("Tag/Chave do alvo. Ex.: 'species_rare_01' ou 'sample_silt'.")]
        public string targetKey;
    }

    [Serializable]
    public sealed class MissionDefinition
    {
        public string id;
        public GameModeType mode;
        public BiomeType biome;
        public string title;
        [TextArea] public string briefing;
        public MissionObjective[] objectives;

        [Header("Scoring")]
        public int baseScore = 1000;
        public int timeBonusPerSecond = 3;
        public int integrityBonusPerPercent = 5;
        public int penaltyPerCollision = 40;
    }
}
