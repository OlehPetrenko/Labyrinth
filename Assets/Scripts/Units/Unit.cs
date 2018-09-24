using Assets.Classes;
using UnityEngine;

namespace Assets.Scripts.Units
{
    /// <summary>
    /// Provides basic logic for units.
    /// </summary>
    public class Unit : MonoBehaviour
    {
        protected Animator Animator { get; private set; }
        protected SpriteRenderer Sprite { get; private set; }

        protected UnitState State
        {
            get { return (UnitState)Animator.GetInteger("State"); }
            set { Animator.SetInteger("State", (int)value); }
        }

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            Sprite = GetComponentInChildren<SpriteRenderer>();
        }

        public virtual void ReceiveDamage()
        {
            Die();
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
