using UnityEngine;

namespace SubDrone.Sonar
{
    /// <summary>Qualquer alvo escaneável.</summary>
    public interface ISonarTarget
    {
        string TargetKey { get; }
        Transform Transform { get; }
        bool CanScan { get; }
    }
}
