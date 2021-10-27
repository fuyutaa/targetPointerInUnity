using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Fixed version, independent object.
public class targetIndicatorIndependent : MonoBehaviour
{
    //[Header("Debug")]
    Vector3 dir;
    float angle;

    [Header("Options")]
    public bool hideCursorWhenInFieldOfView; // if the target is in the field of view, hide the cursor
    public float hideDistance; // the distance from which we'll show/hide our pointer or switch to an arrow/cross sprite (depending on the pointer type you choose).
    public bool showCrossWhenInFieldOfView; // if the target is in the field of view, show a cross on the target position

    [Header("Setup fields")]
    public Transform player;
    public Transform target;
    public Sprite arrowSprite;
    public Sprite crossSprite;
    public SpriteRenderer pointerSpriteRenderer;
    public float positionXOffsetFromPlayer; // we'll make a new Vector3 for our pointer position and we'll give an x position + offset to our cursor.
    public float positionYOffsetFromPlayer; // we'll make a new Vector3 for our pointer position and we'll give an y position + offset to our cursor.

    private void Update() 
    {
        if(target != null)
        {
            PointerUpdate();
        }
    }

    void PointerUpdate()
    {
        dir = target.position - player.position;

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
            pointerSpriteRenderer.sprite = arrowSprite;
            
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.position = new Vector3(player.position.x + positionXOffsetFromPlayer, player.position.y + positionYOffsetFromPlayer, 0f); // follow-player code. Depends on the offset values.
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // look to the target.
            
        }
    }

    void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
