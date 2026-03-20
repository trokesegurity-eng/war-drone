using UnityEngine;

namespace SubDrone.Data
{
    [CreateAssetMenu(menuName = "SubDrone/Biome Config", fileName = "BiomeConfig")]
    public sealed class BiomeConfigSO : ScriptableObject
    {
        public BiomeType biome;

        [Header("Hydrodynamics")]
        [Tooltip("Força da correnteza (direção e magnitude).")]
        public Vector2 currentForce = new(1f, 0f);

        [Tooltip("Drag extra simulando água mais densa.")]
        [Range(0f, 20f)]
        public float linearDrag = 3.5f;

        [Tooltip("Drag angular para estabilizar rotações.")]
        [Range(0f, 20f)]
        public float angularDrag = 2f;

        [Header("Visibility")]
        [Tooltip("0 = cristalino, 1 = muito turvo.")]
        [Range(0f, 1f)]
        public float turbidity = 0.35f;

        [Tooltip("Iluminação base do bioma (0 escuro, 1 claro).")]
        [Range(0f, 1f)]
        public float ambientLight = 0.6f;

        [Header("Pressure & Temperature (for future)")]
        public float baselinePressure = 1f;
        public float baselineTemperature = 18f;
    }
}
