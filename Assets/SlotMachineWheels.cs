//EZPZ Interaction Toolkit
//by Matt Cabanag
//created 09 Mar 2024
//re-edited by Ruonan 11 Oct 2025

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineWheels : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Rigidbody rBody;
    public int slotcount = 8;
    public float spintimer = 2f;
    public float forceFactor = 10;
    public float randomComponent = 0;
    public Transform[] stoppoints;
    public Transform arrow;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public void SpinAxis(Vector3 axis, float force)
    {
        rBody.isKinematic = false;
        rBody.AddRelativeTorque(axis * force * (forceFactor + RandomRoll() + Random.Range(0f, 30f)));
        StartCoroutine(StopAfterTime(spintimer));
    }

    public void StopeAxisX(float force)
    {
        StopAxis(Vector3.right, force);
    }

    public void StopeAxisY(float force)
    {
        StopAxis(Vector3.up, force);
    }

    public void StopeAxisZ(float force)
    {
        StopAxis(Vector3.forward, force);
    }

    public void SpinAxisX(float force)
    {
        SpinAxis(Vector3.right, force);
    }

    public void SpinAxisY(float force)
    {
        SpinAxis(Vector3.up, force);
    }

    public void SpinAxisZ(float force)
    {
        SpinAxis(Vector3.forward, force);
    }

    public float RandomRoll()
    {
        return Random.Range(0, randomComponent);
    }

    private void StopAxis(Vector3 axis, float force)
    {
        rBody.isKinematic = true;
        if (stoppoints.Length == 0) return;
        int stopindex = Random.Range(0, stoppoints.Length);
        Vector3 currentEuller = transform.localEulerAngles;
        float targetx = stoppoints[stopindex].localEulerAngles.z;
        transform.localEulerAngles = new Vector3(targetx, currentEuller.y, currentEuller.z);
    }

    IEnumerator StopAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopAxis(Vector3.forward, 0f); 
    }
    
}