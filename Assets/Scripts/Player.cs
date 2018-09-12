using System.Security;
using Assets.Classes;
using Assets.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Player : Unit, IMovable
    {
        public float Speed { get; set; }

        protected override void Awake()
        {
            Speed = 2.0F;

            base.Awake();
        }


        private void OnTriggerEnter2D(Collider2D collider)
        {
            var coin = collider.GetComponent<Coin>();

            if (coin) // or if(gameObject.CompareTag("YourWallTag"))
            {
                coin.PickUp();

                GameSessionData.GetInstance().Game.UpdateScore();
            }
        }


        private void Update()
        {
            //Physics.defaultContactOffset = 0.01F;

            State = UnitState.Idle;

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                Move();
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
