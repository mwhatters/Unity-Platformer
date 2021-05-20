using UnityEngine;

namespace Player {
    public class Controller : MonoBehaviour
    {
        // move is the applied movement to the physics body, using delta time
        private float moveX = 0f;
        private float moveY = 0f;

        // speed is the virtual speed of the character, calculated in FixedUpdate, 
        // passed as a parameter to move x and y in Move()
        private float speedX = 0f;
        private float speedY = 0f;

        private float movementSpeed = 10f;
        private float moveXAccel = 0.8f;
        private float fallSpeed = -11f;
        private float gravity = 100f;
        private float halfGravityThreshold = 4f;
        private float varJumpBoost = 7f;
        private float jumpSpeed = 16f;
        private float jumpGraceY = 0f;

        private Facings facing;
        public int startFacing = Facings.Right;

        // Components
        private Player.Controls Controls;
        private Rigidbody2D Body;
        private BoxCollider2D Hitbox;
        private SpriteRenderer Sprite;

        // Timers
        private Timer t_VarJump;
        private Timer t_JumpGrace;

        // Collision
        private ContactFilter2D groundFilter;

        void Awake()
        {
            // Components
            Controls = GetComponent<Player.Controls>();
            Body = GetComponent<Rigidbody2D>();
            Hitbox = GetComponent<BoxCollider2D>();
            Sprite = GetComponent<SpriteRenderer>();

            // Timers
            t_JumpGrace = Timer.New(gameObject, 0.15f);
            t_VarJump = Timer.New(gameObject, 0.2f);

            // State
            facing = new Facings(startFacing);
        }

        void Update()
        {
        }

        void FixedUpdate()
        {
            var onGround = CheckSolid(0, -0.01f);
            var onWallRight = CheckSolid(0.2f, 0f);
            var onWallLeft = CheckSolid(-0.2f, 0f);

            if (onGround) 
            {
                t_JumpGrace.Reset();
                jumpGraceY = transform.position.y;
            }
        
            // gravity and falling
            if (!onGround)
            {
                var yMult = 1f;

                if (Mathf.Abs(speedY) < halfGravityThreshold && Controls.Jump.Held()) 
                {
                    yMult = 0.5f;
                }

                speedY -= gravity * yMult * Time.deltaTime;

                if (speedY < fallSpeed) 
                {
                    speedY = Calc.Approach(speedY, fallSpeed, 1600f * Time.deltaTime);
                } 
                else 
                {
                    speedY = Mathf.Max(speedY, fallSpeed);
                }
            }


            // variable jumping
            if (t_VarJump.running) 
            {
                if (Controls.Jump.Held()) 
                {
                    speedY = Mathf.Max(speedY, varJumpBoost);
                } 
                else 
                {
                    t_VarJump.Clear();
                }
            }

            // jump
            if (Controls.Jump.Pressed()) 
            {
                if (t_JumpGrace.running) 
                {
                    Controls.Jump.Clear();
                    Jump();
                } 
                else 
                {
                    if (onWallRight && onWallLeft) 
                    {
                        Controls.Jump.Clear();
                        WallJump(-1);
                    }
                    else if (onWallRight) 
                    {
                        Controls.Jump.Clear();
                        WallJump(-1);
                    } 
                    else if (onWallLeft) 
                    {
                        Controls.Jump.Clear();
                        WallJump(1);
                    }
                }
            }

            // move_x
            speedX = Calc.Approach(speedX, Controls.moveX * movementSpeed, moveXAccel);
            Move();
            Animate();
        }

        private void Jump() 
        {
            t_JumpGrace.Clear();
            t_VarJump.Reset();
            speedY = jumpSpeed;
            transform.position = new Vector2(transform.position.x, jumpGraceY);
        }

        private void WallJump(float dir) 
        {
            t_JumpGrace.Clear();
            t_VarJump.Reset();
            speedX = 10f * dir;
            speedY = jumpSpeed;
        }

        private void Move()
        {
            MoveX();
            MoveY();
            var velocity = new Vector2(moveX, moveY);
            Body.MovePosition(Body.position + velocity);
        }
        private void MoveX()
        {
            moveX = speedX * Time.deltaTime;
            var hit = CheckSolid(moveX, 0);
            if (hit) 
            {
                moveX = (hit.distance - 0.01f) * Mathf.Sign(moveX);
                speedX = 0;
            }
        }

        private void MoveY()
        {
            moveY = speedY * Time.deltaTime;
            var hit = CheckSolid(0, moveY);
            if (hit)
            {
                moveY = (hit.distance - 0.01f) * Mathf.Sign(moveY);
                speedY = 0;
            }
        }

        private RaycastHit2D CheckSolid(float x = 0f, float y = 0f)
        {
            var hit = new RaycastHit2D[1];
            var positionMoved = new Vector2(x, y);
            Hitbox.Cast(positionMoved.normalized, groundFilter, hit, positionMoved.magnitude);
            return hit[0];
        }


        void Animate() {
            if (Controls.moveX != 0f) {
                if (Controls.moveX != facing.current) {
                    facing.Flip();
                    Sprite.flipX = facing.current == Facings.Left;
                }
            }
        }
    }
}

