using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Your arrow sprite must be pointing right.
// Mudt be child of the player.
public class targetIndicator : MonoBehaviour
{
    public Transform target;
    public float hideDistance;

    [Header("Debug")]
    public Vector3 dir;
    public float angle;

    [Header("Options")]
    public bool hideCursorWhenInFieldOfView; // if the target is in the field of view, hide the cursor
    public bool showCrossWhenInFieldOfView; // if the target is in the field of view, show a cross on the target position

    [Header("Setup fields")]
    public Sprite arrowSprite;
    public Sprite crossSprite;
    public SpriteRenderer pointerSpriteRenderer;

    private void main() {
        dir = target.position - transform.position;

        if(dir.magnitude < hideDistance)
        {
            if(hideCursorWhenInFieldOfView)
            {
                SetChildrenActive(false);
            }
            else if(showCrossWhenInFieldOfView)
            {
                pointerSpriteRenderer.sprite = crossSprite;
                transform.position = target.position;
            }
        }
        else
        {
            SetChildrenActive(true);
            
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
        }
    }

    void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            main();
        }
    }
}
