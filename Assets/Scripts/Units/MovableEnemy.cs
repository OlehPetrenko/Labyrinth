using System.Collections.Generic;
using System.Linq;
using Assets.Classes;
using Assets.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Units
{
    /// <summary>
    /// Provides base logic for a enemy that can move.
    /// </summary>
    public class MovableEnemy : Unit, IMovable, IAttackable
    {
        private bool _shouldMove;
        private LinkedList<Point> _path;

        protected float Speed { get; set; }


        protected override void Awake()
        {
            base.Awake();

            State = UnitState.Move;
            Speed = 1f;

            _shouldMove = true;
        }

        protected virtual void Update()
        {
            if (_shouldMove)
                Move();
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
        }


        private void UpdatePath()
        {
            var currentPoint = new Point((int)transform.position.x, (int)transform.position.y);

            if (_path != null && _path.Any() && _path.First.Value == currentPoint)
                _path.RemoveFirst();

            if (_path != null && _path.Any())
                return;

            var playerPoint = new Point(
                (int)GameSessionData.Instance.Player.transform.position.x,
                (int)GameSessionData.Instance.Player.transform.position.y);

            _path = GameSessionData.Instance.PathFinder.FindPath(GameSessionData.Instance.Maze, currentPoint, playerPoint);
        }

        public virtual void Move()
        {
            UpdatePath();

            if (_path.First.Value.X != (int)transform.position.x)
                GoToPointX(_path.First.Value.X);

            if (_path.First.Value.Y != (int)transform.position.y)
                GoToPointY(_path.First.Value.Y);


            //if ((int)transform.position.x == _goalX && (int)transform.position.y == _goalY)
            //    UpdateDirection();

            //if (_goalX != (int)transform.position.x)
            //    GoToPointX(_goalX);

            //if (_goalY != (int)transform.position.y)
            //    GoToPointY(_goalY);
        }

        //private void UpdateDirection()
        //{
        //    var currentX = (int)transform.position.x;
        //    var currentY = (int)transform.position.y;

        //    var availablePoints = new List<Point>();

        //    if (!GameSessionData.Instance.Maze[currentX, currentY])
        //        availablePoints.Add(new Point { X = currentX, Y = currentY - 1 });

        //    if (!GameSessionData.Instance.Maze[currentX, currentY + 1])
        //        availablePoints.Add(new Point { X = currentX, Y = currentY + 1 });

        //    if (!GameSessionData.Instance.Maze[currentX - 1, currentY])
        //        availablePoints.Add(new Point { X = currentX - 1, Y = currentY });

        //    if (!GameSessionData.Instance.Maze[currentX + 1, currentY])
        //        availablePoints.Add(new Point { X = currentX + 1, Y = currentY });

        //    var rand = new System.Random();

        //    var point = availablePoints[rand.Next(0, availablePoints.Count)];

        //    _goalX = point.X;
        //    _goalY = point.Y;
        //}

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

        public virtual void Attack(Unit targetUnit)
        {
            State = UnitState.Attack;
            _shouldMove = false;

            targetUnit.ReceiveDamage();
        }
    }
}
