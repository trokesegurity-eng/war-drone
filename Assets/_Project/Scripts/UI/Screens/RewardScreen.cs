using UnityEngine;
using UnityEngine.UI;

namespace WarAquaDrone.UI.Screens
{
    public sealed class RewardScreen : UIScreen
    {
        [SerializeField] private Text rewardText;

        public void SetRewards(int xp, int credits)
        {
            if (rewardText != null)
                rewardText.text = $"XP +{xp} | Créditos +{credits}";
        }
    }
}
