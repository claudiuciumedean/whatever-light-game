using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 1f;

    public Transform movePoint;
    public LayerMask stopsMovement;
    public LayerMask boxMask;
    public Animator animation;
    GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        RaycastHit2D upRay = Physics2D.Raycast(transform.position, Vector2.up * transform.localScale.y, distance, boxMask);
        RaycastHit2D downRay = Physics2D.Raycast(transform.position, Vector2.down * transform.localScale.y, distance, boxMask);
        RaycastHit2D leftRay = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance, boxMask);
        RaycastHit2D rightRay = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                Vector3 xPos = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                if (!Physics2D.OverlapCircle(movePoint.position + xPos, .2f, stopsMovement))
                {
                    movePoint.position += xPos;
                
                    if(Input.GetAxisRaw("Horizontal") == 1 && rightRay.collider != null && rightRay.collider.gameObject.tag == "Pushable") {
                        Vector3 xPos_Right = rightRay.collider.gameObject.transform.position + xPos;
                        if(!Physics2D.OverlapCircle(xPos_Right, .2f, stopsMovement)) {
                            this.pushBox(rightRay, xPos);
                        }
                    }
                    else if(Input.GetAxisRaw("Horizontal") == -1 && leftRay.collider != null && leftRay.collider.gameObject.tag == "Pushable")
                    {
                        Vector3 yPos_Left = leftRay.collider.gameObject.transform.position + xPos;
                        if(!Physics2D.OverlapCircle(yPos_Left, .2f, stopsMovement)) {
                            this.pushBox(leftRay, xPos);
                        }
                    }
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {

                Vector3 yPos = new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                
                if (!Physics2D.OverlapCircle(movePoint.position + yPos, .2f, stopsMovement))
                {
                    movePoint.position += yPos;

                    if(Input.GetAxisRaw("Vertical") == 1 && upRay.collider != null && upRay.collider.gameObject.tag == "Pushable") {
                        Vector3 yPos_Up = upRay.collider.gameObject.transform.position + yPos;
                        if(!Physics2D.OverlapCircle(yPos_Up, .2f, stopsMovement)) {
                            this.pushBox(upRay, yPos);
                        }
                    }
                    else if(Input.GetAxisRaw("Vertical") == -1 && downRay.collider != null && downRay.collider.gameObject.tag == "Pushable")
                    {
                        Vector3 yPos_Down = downRay.collider.gameObject.transform.position + yPos;
                        if(!Physics2D.OverlapCircle(yPos_Down, .2f, stopsMovement)) {
                            this.pushBox(downRay, yPos);
                        }
                    }
                }
            }

            animation.SetBool("moving", false);
            return;
        }       

        animation.SetBool("moving", true);
    }

    void OnDrawGizmos()
	{
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.up * transform.localScale.y * distance);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * transform.localScale.y * distance);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x * distance);
	}

    void pushBox(RaycastHit2D ray, Vector3 position)
    {       
        box = ray.collider.gameObject;
        box.transform.position += position;        
    }
}
