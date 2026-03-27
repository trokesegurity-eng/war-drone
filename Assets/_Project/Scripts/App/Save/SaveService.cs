using UnityEngine;

namespace WarAquaDrone.App.Save
{
    public sealed class SaveService
    {
        private const string SaveKey = "WAR_AQUADRONE_SAVE_V1";

        public PlayerSave LoadOrCreate()
        {
            if (!PlayerPrefs.HasKey(SaveKey)) return new PlayerSave();

            var json = PlayerPrefs.GetString(SaveKey);
            var loaded = JsonUtility.FromJson<PlayerSave>(json);
            return loaded ?? new PlayerSave();
        }

        public void Save(PlayerSave save)
        {
            var json = JsonUtility.ToJson(save);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }
    }
}
