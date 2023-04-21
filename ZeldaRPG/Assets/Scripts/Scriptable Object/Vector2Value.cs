using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Vector2Value : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;
    [HideInInspector]
    public Vector2 runtimeValue;

    public void OnBeforeSerialize()
    {

    }
    public void OnAfterDeserialize()
    {
        runtimeValue = initialValue;
    }
}
