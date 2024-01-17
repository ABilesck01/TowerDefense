using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    [SerializeField] private SerializableDictionary<BaseUnit.UnitState, AnimationClip> unitStates;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void OnStateChanged(BaseUnit.UnitState state)
    {
        unitStates.TryGetValue(state, out AnimationClip clip);
        if(clip == null) 
        {
            Debug.LogError($"There is no animation for state {state}.", this);
            return;
        }

        animator.CrossFade(clip.name, 0);
    }
}
