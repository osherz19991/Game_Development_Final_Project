using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{

    public float Tension;
    private bool _pressed;

    public Transform RopeTransform;

    public GameObject Bow_in_Hand;
    public Vector3 RopeNearLocalPosition;
    public Vector3 RopeFarLocalPosition;

    public AnimationCurve RopeReturnAnimation;

    public float ReturnTime;

    public ArrowScript CurrentArrow;
    private int ArrowIndex = 0;
    public float ArrowSpeed;

    public AudioSource BowTensionAudioSource;
    public AudioSource BowWhistlingAudioSource;

    public ArrowScript[] ArrowsPool;
    void Start()
    {
        RopeNearLocalPosition = RopeTransform.localPosition;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Bow_in_Hand != null) { 
            _pressed = true;

            ArrowIndex++;
            if (ArrowIndex >= ArrowsPool.Length)
            {
                ArrowIndex = 0;
            }
            CurrentArrow = ArrowsPool[ArrowIndex];
      
            CurrentArrow.gameObject.SetActive(true);
            

            CurrentArrow.SetToRope(RopeTransform);

            BowTensionAudioSource.pitch = Random.Range(0.8f, 1.2f);
            BowTensionAudioSource.Play();
        }
        if(Input.GetMouseButtonUp(0) && Bow_in_Hand != null) {
            _pressed = false;
            StartCoroutine(RopeReturn());
            CurrentArrow.Shot(ArrowSpeed * Tension);
            Tension = 0;

            BowTensionAudioSource.Stop();
            BowWhistlingAudioSource.pitch = Random.Range(0.8f, 1.2f);
            BowWhistlingAudioSource.Play();
        }
        if (_pressed )
        {
            if(Tension < 1f)
            {
                Tension += Time.deltaTime;
            }
            RopeTransform.localPosition = Vector3.Lerp(RopeNearLocalPosition, RopeFarLocalPosition, Tension);
        }
    }

    IEnumerator RopeReturn() {  
        Vector3 StartLocalPositon = RopeTransform.localPosition;
        for(float f = 0; f<1f; f += Time.deltaTime / ReturnTime){
            RopeTransform.localPosition = Vector3.LerpUnclamped(StartLocalPositon, RopeNearLocalPosition, RopeReturnAnimation.Evaluate(f));
            yield return null;
        }
        RopeTransform.localPosition = RopeNearLocalPosition;
    
    }
}
