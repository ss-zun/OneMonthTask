using UnityEngine;

public class AnimationData
{
    #region ParameterNames
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string dieParameterName = "Die";
    [SerializeField] private string hitParameterName = "Hit";
    #endregion

    #region ParameterHashs
    public int WalkParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    #endregion

    public void Init()
    {
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        DieParameterHash = Animator.StringToHash(dieParameterName);
        HitParameterHash = Animator.StringToHash(hitParameterName);
    }
}
