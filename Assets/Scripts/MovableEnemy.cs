using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Classes;
using Assets.Classes.PathFinder;
using Assets.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovableEnemy : Unit, IMovable, IAttackable
    {
        private bool _shouldMove;

        private int _goalX;
        private int _goalY;

        public float Speed { get; set; }

        protected override void Awake()
        {
            base.Awake();

            State = UnitState.Move;
            Speed = 0F;
            //Speed = 1.5F;

            _goalX = (int)transform.position.x;
            _goalY = (int)transform.position.y;

            _shouldMove = true;

            _coin = Resources.Load("Coin", typeof(Coin)) as Coin;
        }

        private Coin _coin;
        protected virtual void Update()
        {
            if(_shouldMove)
                Move();

            //if (Input.GetButton("Jump"))
            //{
            //    var player = GameObject.Find("Player");
            //    var playerPoint = new Point((int)player.transform.position.x, (int)player.transform.position.y);

            //    var currentPoint = new Point((int)transform.position.x, (int)transform.position.y);
            //    var pathFinder = new AStarPathFinder();

            //    var list = pathFinder.FindPath(GameSessionData.GetInstance().Maze, currentPoint, playerPoint);
            //    foreach (var point in list)
            //    {
            //        Instantiate(_coin, new Vector3(point.X, point.Y), transform.rotation);
            //        Debug.Log(string.Format("{0}:{1}", point.X, point.Y));
            //    }
            //}
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var player = collider.GetComponent<Player>();

            if (player)
                Attack(player);

            //var wall = collider.GetComponent<Wall>();

            //if (wall) // or if(gameObject.CompareTag("YourWallTag"))
            //{
            //_shouldMove = false;

            //rigidbody.velocity = Vector3.zero;
            //UpdateDirection();
            //}
        }

        public void IncreaseSpeed()
        {
            //
            // Increase speed on 5%.
            //
            Speed += Speed * 0.05F;
            Debug.Log(Speed);
        }

        public virtual void Move()
        {
            if ((int)transform.position.x == _goalX && (int)transform.position.y == _goalY)
                UpdateDirection();

            if (_goalX != (int)transform.position.x)
                GoToPointX(_goalX);

            if (_goalY != (int)transform.position.y)
                GoToPointY(_goalY);
        }

        private void UpdateDirection()
        {
            var currentX = (int)transform.position.x;
            var currentY = (int)transform.position.y;

            var availablePoints = new List<Point>();

            if (!GameSessionData.GetInstance().Maze[currentX, currentY])
                availablePoints.Add(new Point { X = currentX, Y = currentY - 1 });

            if (!GameSessionData.GetInstance().Maze[currentX, currentY + 1])
                availablePoints.Add(new Point { X = currentX, Y = currentY + 1 });

            if (!GameSessionData.GetInstance().Maze[currentX - 1, currentY])
                availablePoints.Add(new Point { X = currentX - 1, Y = currentY });

            if (!GameSessionData.GetInstance().Maze[currentX + 1, currentY])
                availablePoints.Add(new Point { X = currentX + 1, Y = currentY });

            var rand = new System.Random();

            var point = availablePoints[rand.Next(0, availablePoints.Count)];

            _goalX = point.X;
            _goalY = point.Y;
        }

        private void GoToPointX(int goalX)
        {
            var currentX = (int)transform.position.x;

            var direction = transform.right * (goalX > currentX ? 1 : -1);

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.smoothDeltaTime);

            Sprite.flipX = direction.x > 0;


        }

        private void GoToPointY(int goalY)
        {
            var currentY = (int)transform.position.y;

            var direction = transform.up * (goalY > currentY ? 1 : -1);

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.smoothDeltaTime);


        }

        public void Attack(Unit targetUnit)
        {
            State = UnitState.Attack;
            _shouldMove = false;

            targetUnit.ReceiveDamage();

        }
    }
}
