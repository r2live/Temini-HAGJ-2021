using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{

    FMOD.Studio.EventInstance Event;

    void PlayEvent(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
    }


    void EnemyAttack(string path)
    {
        Event = FMODUnity.RuntimeManager.CreateInstance(path);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Event, transform, GetComponent<Rigidbody2D>());
        Event.start();
        Event.release();
    }
}
