
//will make an object not be destroyed when a new scene is laoded
//MUST have unique id or more then one instance may happen

using UnityEngine;
using System.Collections.Generic;

public class DontDestroyOnLoadUtility : MonoBehaviour {

    public static List<string> cache = new List<string>();

    public string uniqueId = "";

    private void Awake () {

        if (cache.Contains(uniqueId)) {

            Destroy(gameObject);
            return;

        } else {

            cache.Add(uniqueId);
            DontDestroyOnLoad(gameObject);
        }
    }
}
