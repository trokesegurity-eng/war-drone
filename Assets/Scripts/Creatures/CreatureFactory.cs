using UnityEngine;
using SubDrone.Data;

namespace SubDrone.Creatures
{
    /// <summary>Gera definição e instancia criatura 2D por partes.</summary>
    public sealed class CreatureFactory : MonoBehaviour
    {
        [Header("Catalogs (um por bioma)")]
        public CreaturePartsCatalogSO riverCatalog;
        public CreaturePartsCatalogSO seaCatalog;
        public CreaturePartsCatalogSO lakeCatalog;
        public CreaturePartsCatalogSO damCatalog;

        [Header("Prefab")]
        public GameObject creaturePrefab;

        public GameObject Spawn(BiomeType biome, Vector3 position, int seed, out CreatureDefinition definition)
        {
            var catalog = GetCatalog(biome);
            if (catalog == null || creaturePrefab == null)
            {
                definition = null;
                return null;
            }

            var rng = new System.Random(seed);
            var head = catalog.PickWeighted(catalog.heads, rng);
            var body = catalog.PickWeighted(catalog.bodies, rng);
            var tail = catalog.PickWeighted(catalog.tails, rng);
            definition = new CreatureDefinition
            {
                id = $"cr_{biome}_{seed}",
                biome = biome,
                seed = seed,
                archetype = PickArchetype(biome, rng),
                headPartId = head != null ? head.id : string.Empty,
                bodyPartId = body != null ? body.id : string.Empty,
                tailPartId = tail != null ? tail.id : string.Empty,
                bodyTint = catalog.tintGradient.Evaluate((float)rng.NextDouble()),
                rarity = ComputeRarity(head, body, tail),
                stats = BuildStats(head, body, tail, biome, rng),
            };

            var instance = Instantiate(creaturePrefab, position, Quaternion.identity);
            var view = instance.GetComponent<CreatureView>();
            if (view != null)
            {
                view.Apply(definition, head, body, tail);
            }

            var ai = instance.GetComponent<CreatureAI2D>();
            if (ai != null)
            {
                ai.SetDefinition(definition);
            }

            return instance;
        }

        private CreaturePartsCatalogSO GetCatalog(BiomeType biome)
        {
            return biome switch
            {
                BiomeType.River => riverCatalog,
                BiomeType.Sea => seaCatalog,
                BiomeType.Lake => lakeCatalog,
                BiomeType.Dam => damCatalog,
                _ => riverCatalog,
            };
        }

        private static CreatureArchetype PickArchetype(BiomeType biome, System.Random rng)
        {
            var roll = rng.Next(0, 100);
            return biome switch
            {
                BiomeType.River => roll < 40 ? CreatureArchetype.Chase : roll < 70 ? CreatureArchetype.Patrol : CreatureArchetype.Ambush,
                BiomeType.Sea => roll < 35 ? CreatureArchetype.Ambush : roll < 75 ? CreatureArchetype.Patrol : CreatureArchetype.Chase,
                BiomeType.Lake => roll < 50 ? CreatureArchetype.Patrol : CreatureArchetype.Flee,
                BiomeType.Dam => roll < 45 ? CreatureArchetype.Chase : CreatureArchetype.Patrol,
                _ => CreatureArchetype.Patrol,
            };
        }

        private static int ComputeRarity(CreaturePart head, CreaturePart body, CreaturePart tail)
        {
            var headWeight = head != null ? Mathf.Max(1, head.rarityWeight) : 10;
            var bodyWeight = body != null ? Mathf.Max(1, body.rarityWeight) : 10;
            var tailWeight = tail != null ? Mathf.Max(1, tail.rarityWeight) : 10;
            return Mathf.RoundToInt(300f / (headWeight + bodyWeight + tailWeight));
        }

        private static CreatureStats BuildStats(CreaturePart head, CreaturePart body, CreaturePart tail, BiomeType biome, System.Random rng)
        {
            var hydro = 1f;
            if (head != null)
            {
                hydro *= head.hydrodynamicFactor;
            }

            if (body != null)
            {
                hydro *= body.hydrodynamicFactor;
            }

            if (tail != null)
            {
                hydro *= tail.hydrodynamicFactor;
            }

            var biomeAggression = biome == BiomeType.River ? 0.7f : biome == BiomeType.Sea ? 0.65f : 0.45f;
            return new CreatureStats
            {
                maxHealth = Mathf.Lerp(35f, 120f, (float)rng.NextDouble()),
                speed = Mathf.Clamp(1.5f + (float)rng.NextDouble() * 2.5f * hydro, 1.2f, 5f),
                damage = Mathf.Lerp(6f, 24f, (float)rng.NextDouble()),
                aggression = Mathf.Clamp01(biomeAggression + ((float)rng.NextDouble() - 0.5f) * 0.25f),
                detectionRadius = Mathf.Lerp(4f, 10f, (float)rng.NextDouble()),
            };
        }
    }
}
