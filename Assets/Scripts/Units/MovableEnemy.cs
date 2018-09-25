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

        private Point _currentPosition;
        private Point _currentPositionShift;

        protected float Speed { get; set; }

        private Point PlayerPoint
        {
            get
            {
                return new Point(
                    GameSessionData.Instance.Player.transform.position.x > 1 ? (int)GameSessionData.Instance.Player.transform.position.x : 1,
                    GameSessionData.Instance.Player.transform.position.y > 1 ? (int)GameSessionData.Instance.Player.transform.position.y : 1);
            }
        }


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
        }

        private void UpdatePath()
        {
            _currentPosition = new Point((int)transform.position.x + _currentPositionShift.X, (int)transform.position.y + _currentPositionShift.Y);

            //
            // Let enemy finish already constructed path, after that create a new one. 
            // It makes game more playable.
            //
            if (_path != null && _path.Any() && _path.First.Value == _currentPosition)
            {
                _path.RemoveFirst();

                UpdateCurrentPositionShift();
            }

            if (_path != null && _path.Any())
                return;

            _path = GameSessionData.Instance.PathFinder.FindPath(GameSessionData.Instance.Maze, _currentPosition, PlayerPoint);

            UpdateCurrentPositionShift();
        }

        public virtual void Move()
        {
            UpdatePath();

            if (_path != null && _path.Any())
                MoveToPoint(_path.First.Value);
        }

        private void MoveToPoint(Point goal)
        {
            var direction = Vector3.zero;

            if (_path.First.Value.X != _currentPosition.X)
            {
                direction = transform.right * (goal.X > _currentPosition.X ? 1 : -1);
                Sprite.flipX = direction.x > 0;
            }

            if(_path.First.Value.Y != _currentPosition.Y)
                direction = transform.up * (goal.Y > _currentPosition.Y ? 1 : -1);

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.smoothDeltaTime);
        }

        private void UpdateCurrentPositionShift()
        {
            _currentPositionShift = new Point(0, 0);

            if (_path == null || !_path.Any())
                return;

            _currentPositionShift.X = _path.First.Value.X < _currentPosition.X ? 1 : 0;
            _currentPositionShift.Y = _path.First.Value.Y < _currentPosition.Y ? 1 : 0;
        }

        public virtual void Attack(Unit targetUnit)
        {
            State = UnitState.Attack;
            _shouldMove = false;

            targetUnit.ReceiveDamage();
        }

        public void IncreaseSpeed()
        {
            //
            // Increase speed on 5%.
            //
            Speed += Speed * 0.05F;
        }
    }
}
