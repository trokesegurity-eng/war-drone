using UnityEngine;

namespace SubDrone.Prototype3D
{
    public sealed class MantaGame3DBuilder : MonoBehaviour
    {
        [SerializeField] private int ringCount = 14;
        [SerializeField] private float ringSpacing = 45f;
        [SerializeField] private Vector2 depthRange = new(-26f, -6f);
        [SerializeField] private Material neonMaterial;

        private int _nextRing;
        private int _score;
        private MantaDroneController3D _drone;

        private void Start()
        {
            BuildLighting();
            BuildDrone();
            BuildRingCourse();
            BuildSeafloor();
            BuildCamera();
        }

        private void BuildLighting()
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.02f, 0.2f, 0.32f, 1f);
            RenderSettings.fogDensity = 0.03f;

            var key = new GameObject("OceanKeyLight").AddComponent<Light>();
            key.type = LightType.Directional;
            key.color = new Color(0.45f, 0.85f, 1f);
            key.intensity = 0.85f;
            key.transform.rotation = Quaternion.Euler(55f, -30f, 0f);

            var fill = new GameObject("OceanFillLight").AddComponent<Light>();
            fill.type = LightType.Directional;
            fill.color = new Color(0.05f, 0.45f, 0.75f);
            fill.intensity = 0.55f;
            fill.transform.rotation = Quaternion.Euler(25f, 120f, 0f);
        }

        private void BuildDrone()
        {
            var body = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            body.name = "MantaDrone";
            body.transform.position = new Vector3(0f, -10f, 0f);
            body.transform.localScale = new Vector3(2f, 0.7f, 2.8f);
            body.GetComponent<Renderer>().material.color = new Color(0.1f, 0.73f, 0.8f);

            var wingLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wingLeft.transform.SetParent(body.transform);
            wingLeft.transform.localPosition = new Vector3(-2.25f, 0f, 0f);
            wingLeft.transform.localScale = new Vector3(2.4f, 0.1f, 3.2f);

            var wingRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wingRight.transform.SetParent(body.transform);
            wingRight.transform.localPosition = new Vector3(2.25f, 0f, 0f);
            wingRight.transform.localScale = new Vector3(2.4f, 0.1f, 3.2f);

            var rb = body.AddComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            _drone = body.AddComponent<MantaDroneController3D>();
            _drone.ConfigureWings(wingLeft.transform, wingRight.transform);
        }

        private void BuildRingCourse()
        {
            for (var i = 0; i < ringCount; i++)
            {
                var ringRoot = new GameObject($"Ring_{i:00}");
                ringRoot.tag = "Untagged";
                ringRoot.transform.position = new Vector3(
                    Mathf.Sin(i * 0.65f) * 18f,
                    Mathf.Lerp(depthRange.x, depthRange.y, Random.value),
                    30f + i * ringSpacing);

                for (var k = 0; k < 16; k++)
                {
                    var piece = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    piece.transform.SetParent(ringRoot.transform);
                    var angle = (k / 16f) * Mathf.PI * 2f;
                    piece.transform.localPosition = new Vector3(Mathf.Cos(angle) * 5f, Mathf.Sin(angle) * 5f, 0f);
                    piece.transform.localScale = Vector3.one * 0.7f;
                    piece.GetComponent<Collider>().isTrigger = true;
                    var renderer = piece.GetComponent<Renderer>();
                    renderer.material = neonMaterial != null ? neonMaterial : renderer.material;
                    renderer.material.color = new Color(0f, 0.95f, 1f, 1f);
                }

                ringRoot.AddComponent<RingCheckpoint>().Setup(this, i);
            }
        }

        private void BuildSeafloor()
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floor.transform.position = new Vector3(0f, -35f, ringCount * ringSpacing * 0.5f);
            floor.transform.localScale = new Vector3(8f, 1f, 12f);
            floor.GetComponent<Renderer>().material.color = new Color(0.05f, 0.2f, 0.24f, 1f);

            for (var i = 0; i < 100; i++)
            {
                var rock = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                rock.transform.position = new Vector3(
                    Random.Range(-80f, 80f),
                    Random.Range(-34f, -28f),
                    Random.Range(0f, ringCount * ringSpacing + 120f));
                var scale = Random.Range(1f, 6.5f);
                rock.transform.localScale = Vector3.one * scale;
                rock.GetComponent<Renderer>().material.color = new Color(0.09f, 0.25f, 0.3f, 1f);
            }
        }

        private void BuildCamera()
        {
            var cam = Camera.main;
            if (cam == null)
            {
                cam = new GameObject("Main Camera").AddComponent<Camera>();
                cam.tag = "MainCamera";
            }

            var follow = cam.gameObject.AddComponent<SmoothFollowCamera3D>();
            follow.Target = _drone.transform;
        }

        public void NotifyRingPassed(int ringIndex)
        {
            if (ringIndex != _nextRing)
            {
                return;
            }

            _nextRing++;
            _score += 100;
        }

        private void OnGUI()
        {
            if (_drone == null)
            {
                return;
            }

            const int width = 330;
            GUILayout.BeginArea(new Rect(20, 20, width, 160), GUI.skin.box);
            GUILayout.Label("Aqua Manta Runner (3D)");
            GUILayout.Label($"Anéis: {_nextRing}/{ringCount}");
            GUILayout.Label($"Pontuação: {_score}");
            GUILayout.Label($"Velocidade: {_drone.Speed:0.0} m/s");
            GUILayout.Label($"Bateria: {_drone.BatteryNormalized * 100f:0}%");
            GUILayout.EndArea();
        }

        private sealed class RingCheckpoint : MonoBehaviour
        {
            private MantaGame3DBuilder _game;
            private int _index;
            private bool _triggered;

            public void Setup(MantaGame3DBuilder game, int index)
            {
                _game = game;
                _index = index;
            }

            private void OnTriggerEnter(Collider other)
            {
                if (_triggered || _game == null)
                {
                    return;
                }

                if (other.GetComponentInParent<MantaDroneController3D>() == null)
                {
                    return;
                }

                _triggered = true;
                _game.NotifyRingPassed(_index);
                gameObject.SetActive(false);
            }
        }
    }
}
