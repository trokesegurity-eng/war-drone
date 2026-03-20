using System;
using UnityEngine;

namespace SubDrone.Data
{
    [Serializable]
    public sealed class CreaturePart
    {
        public string id;
        public Sprite sprite;
        public Vector2 localOffset;
        public float localRotation;
        public Vector2 localScale = Vector2.one;
        public float hydrodynamicFactor = 1f;
        public int rarityWeight = 10;
    }

    [CreateAssetMenu(menuName = "SubDrone/Creature Parts Catalog", fileName = "CreaturePartsCatalog")]
    public sealed class CreaturePartsCatalogSO : ScriptableObject
    {
        public BiomeType biome;
        public CreaturePart[] heads;
        public CreaturePart[] bodies;
        public CreaturePart[] tails;

        [Header("Biome Color Palette")]
        public Gradient tintGradient;

        public CreaturePart PickWeighted(CreaturePart[] list, System.Random rng)
        {
            if (list == null || list.Length == 0)
            {
                return null;
            }

            double total = 0;
            for (var i = 0; i < list.Length; i++)
            {
                total += 1d / Mathf.Max(1, list[i].rarityWeight);
            }

            var roll = rng.NextDouble() * total;
            double accumulated = 0;
            for (var i = 0; i < list.Length; i++)
            {
                accumulated += 1d / Mathf.Max(1, list[i].rarityWeight);
                if (roll <= accumulated)
                {
                    return list[i];
                }
            }

            return list[^1];
        }
    }
}
