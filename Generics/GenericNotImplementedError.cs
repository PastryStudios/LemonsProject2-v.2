using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNotImplementedError<T>
{    public static T TryGet(T value, string name)
    {
        if (value != null)
        {
            return value;
        }
        Debug.LogError(typeof(T) + " not implented on " + name);
        return default;
    }
}
