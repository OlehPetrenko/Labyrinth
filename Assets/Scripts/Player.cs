using System;
using System.Security;
using Assets.Classes;
using Assets.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Player : Unit, IMovable
    {
        public event Action OnDestroyEvent;

        public float Speed { get; set; }


        protected override void Awake()
        {
            Speed = 2.0F;

            base.Awake();
        }

        private void Update()
        {
            State = UnitState.Idle;

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                Move();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var coin = collider.GetComponent<Coin>();

            if (coin)
                coin.PickUp();
        }

        private void OnDestroy()
        {
            if (this.OnDestroyEvent != null)
                OnDestroyEvent();
        }

        public void Move()
        {
            var direction = Vector3.zero;

            if (Input.GetButton("Horizontal"))
                direction = transform.right * Input.GetAxis("Horizontal");

            if (Input.GetButton("Vertical"))
                direction = transform.up * Input.GetAxis("Vertical");

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.smoothDeltaTime);

            Sprite.flipX = direction.x < 0;

            State = UnitState.Move;
        }
    }
}
