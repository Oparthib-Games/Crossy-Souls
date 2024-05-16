using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHopCtrl : MonoBehaviour
{
    [SerializeField] private Vector3 hopPos;
    [SerializeField] private float faceYRot;

    public int perHopDist = 1;
    public float hopSpeed = 1;

    bool doHop;
    bool isHopping;

    Animator ANIM;

    void Start()
    {
        ANIM = GetComponent<Animator>();
    }

    void Update()
    {
        InputHandle();
        
        if(doHop)
        {
            transform.position = Vector3.Lerp(transform.position, hopPos, hopSpeed * Time.deltaTime);

            Quaternion targetRot = Quaternion.Euler(new Vector3(0, faceYRot, 0));
            transform.rotation = targetRot;
        }
    }

    public void InputHandle()
    {
        if(Input.GetKeyDown(KeyCode.W))
            Hop("F");
        if (Input.GetKeyDown(KeyCode.S))
            Hop("B");
        if (Input.GetKeyDown(KeyCode.A))
            Hop("L");
        if (Input.GetKeyDown(KeyCode.D))
            Hop("R");
    }

    public void Hop(string dir = "F")
    {
        if (isHopping) return;
        isHopping = true;

        ANIM.SetTrigger("hop");

        Vector3 pos = transform.position;

        switch (dir)
        {
            case "F":
                faceYRot = 0;
                hopPos = new Vector3(pos.x + perHopDist, pos.y, pos.z);
                break;
            case "B":
                faceYRot = 180;
                hopPos = new Vector3(pos.x - perHopDist, pos.y, pos.z);
                break;
            case "R":
                faceYRot = 90;
                hopPos = new Vector3(pos.x, pos.y, pos.z - perHopDist);
                break;
            case "L":
                faceYRot = 270;
                hopPos = new Vector3(pos.x, pos.y, pos.z + perHopDist);
                break;
            default:
                break;
        }
    }

    public void StartHopEvent()
    {
        doHop = true;
    }
    public void EndHopEvent()
    {
        doHop = false;
        transform.position = hopPos;
        isHopping = false;
    }
}
