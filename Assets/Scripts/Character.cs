using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Inspired by https://gist.github.com/InfiniteAmmoInc/039e5c28a876286c103d369f9ab61a0c
//My notes are mainly for me to get what's happening here.

public class Character : MonoBehaviour
{
    public enum Slide
    {
        None,
        Perpendicular,
    }

    public Vector3 boxScale, boxOffset;
    private const float shim = .01f;
    private bool overlapping;
    private int layerMask;

    // Start is called before the first frame update
    void Awake()
    {
        layerMask = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    public bool Move(Vector3 move, Slide slide = Slide.None)
    {
        var distance = move.magnitude;
        var direction = move / distance;
        var pos = transform.position;
        float castDistance;

        if (BoxCastHit(pos, direction, distance, out castDistance))
        {
            var bailPos = pos + (castDistance - shim) * direction;
            if (slide == Slide.None)
            {
                transform.position = bailPos;
                return false;
            }

            var newDirection = Vector3.zero;

            if (!TrySlide(direction, distance, ref newDirection))
            {
                transform.position = bailPos;
                return false;
            }

            transform.position = pos + newDirection * distance;
            return true;
        }

        pos += move;
        transform.position = pos;
        return true;
    }

    public bool MoveX(float xAmount, Slide slide = Slide.None)
    {
        return Move(Vector3.right * xAmount, slide);
    }

    public bool MoveY(float yAmount, Slide slide = Slide.None)
    {
        return Move(Vector3.up * yAmount, slide);
    }

    private bool TrySlide(Vector3 direction, float distance, ref Vector3 newDirection)
    {
        var maxAdjust = 1.5f;

        if (direction == Vector3.down)
            maxAdjust = .5f;

        const float adjustStep = .1f;
        var pos = transform.position;
        float castDistance;

        for (var adjust = adjustStep; adjust < maxAdjust; adjust += adjustStep)
        {
            if(direction == Vector3.right || direction == Vector3.left)
            {
                newDirection = (direction + Vector3.up * adjust).normalized;
                if (!BoxCastHit(pos, newDirection, distance, out castDistance))
                    return true;
            }
            else if (direction == Vector3.down)
            {
                newDirection = (direction + Vector3.right * adjust).normalized;
                if (!BoxCastHit(pos, newDirection, distance, out castDistance))
                    return true;

                newDirection = (direction + Vector3.left * adjust).normalized;
                if (!BoxCastHit(pos, newDirection, distance, out castDistance))
                    return true;
            }
        }
        return false;
    }

    private bool BoxCastHit(Vector3 pos, Vector3 direction, float distance, out float castDistance)
    {
        RaycastHit hit;

        if (Physics.BoxCast(pos + boxOffset, boxScale * .5f, direction, out hit, Quaternion.identity, distance, layerMask))
        {
            castDistance = hit.distance;
            return true;
        }

        castDistance = distance;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(boxOffset, boxScale);
    }
}
