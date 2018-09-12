namespace Assets.Scripts
{
    public class Zombie : MovableEnemy
    {
        //[SerializeField] private float speed = 1.0F;

        //new private Rigidbody2D rigidbody;
        //private Animator animator;
        //private SpriteRenderer sprite;

        //private bool IsWalk
        //{
        //    get { return animator.GetBool("IsWalk"); }
        //    set { animator.SetBool("IsWalk", value); }
        //}

        //private void Awake()
        //{
        //    rigidbody = GetComponent<Rigidbody2D>();
        //    animator = GetComponent<Animator>();
        //    sprite = GetComponentInChildren<SpriteRenderer>();

        //    //wall = Resources.Load<Wall>("Wall");

        //    _goalX = (int)transform.position.x;
        //    _goalY = (int)transform.position.y;

        //    //var position = Vector3.zero;
        //    //Instantiate(wall, position, wall.transform.rotation);
        //}

        // Use this for initialization
        //void Start()
        //{

        //}

        // Update is called once per frame
        //void Update()
        //{
        //    Move();
        //} 

        //[SerializeField] private bool _shouldMove = true;

        //private void OnTriggerEnter2D(Collider2D collider)
        //{
        //    var wall = collider.GetComponent<Wall>();

        //    if (wall) // or if(gameObject.CompareTag("YourWallTag"))
        //    {
        //        _shouldMove = false;

        //        rigidbody.velocity = Vector3.zero;
        //        UpdateDirection();
        //    }
        //}


        //[SerializeField] private int _goalX;
        //[SerializeField] private int _goalY;

        //private void Walk()
        //{
        //    if ((int)transform.position.x == _goalX && (int)transform.position.y == _goalY)
        //        UpdateDirection();

        //    if (_goalX != (int)transform.position.x)
        //        GoToPointX(_goalX);

        //    if (_goalY != (int)transform.position.y)
        //        GoToPointY(_goalY);

        //    IsWalk = true;
        //}

        //public void UpdateDirection()
        //{
        //    var currentX = (int)transform.position.x;
        //    var currentY = (int)transform.position.y;

        //    var availablePoints = new List<Point>();

        //    if (!GameSessionData.GetInstance().Maze[currentX, currentY])
        //        availablePoints.Add(new Point { X = currentX, Y = currentY - 1 });

        //    if (!GameSessionData.GetInstance().Maze[currentX, currentY + 1])
        //        availablePoints.Add(new Point { X = currentX, Y = currentY + 1 });

        //    if (!GameSessionData.GetInstance().Maze[currentX - 1, currentY])
        //        availablePoints.Add(new Point { X = currentX - 1, Y = currentY });

        //    if (!GameSessionData.GetInstance().Maze[currentX + 1, currentY])
        //        availablePoints.Add(new Point { X = currentX + 1, Y = currentY });

        //    var rand = new System.Random();

        //    var point = availablePoints[rand.Next(0, availablePoints.Count)];

        //    _goalX = point.X;
        //    _goalY = point.Y;
        //}




        //private void GoToPointX(int goalX)
        //{
        //    var currentX = (int)transform.position.x;

        //    var direction = transform.right * (goalX > currentX ? 1 : -1);

        //    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.smoothDeltaTime);
        //    sprite.flipX = direction.x > 0;
        //}


        //private void GoToPointY(int goalY)
        //{
        //    var currentY = (int)transform.position.y;

        //    var direction = transform.up * (goalY > currentY ? 1 : -1);

        //    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.smoothDeltaTime);
        //}
    }
}
