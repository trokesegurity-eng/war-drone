using System;
using UnityEngine;

namespace SubDrone.Data
{
    public enum CreatureArchetype
    {
        Patrol,
        Chase,
        Ambush,
        Flee
    }

    [Serializable]
    public sealed class CreatureStats
    {
        public float maxHealth = 50f;
        public float speed = 2.5f;
        public float damage = 10f;
        public float aggression = 0.5f;
        public float detectionRadius = 6f;
    }

    [Serializable]
    public sealed class CreatureDefinition
    {
        public string id;
        public BiomeType biome;
        public CreatureArchetype archetype;
        public Color bodyTint = Color.white;
        public string headPartId;
        public string bodyPartId;
        public string tailPartId;
        public CreatureStats stats = new();
        public int rarity;
        public int seed;
    }
}
