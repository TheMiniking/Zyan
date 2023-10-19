using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSequencer : MonoBehaviour {
	public GameObject cube1;

	public void Start(){


		var seq = LeanTween.sequence();
        _ = seq.append(() =>
        { // fire an event before start
            Debug.Log("firsts:" + Time.time);
        });
        _ = seq.append(1f); // delay everything one second
        _ = seq.append(() =>
        { // fire an event before start
            Debug.Log("I have started:" + Time.time);
        });
        _ = seq.append(LeanTween.move(cube1, Vector3.one * 10f, 1f)); // do a tween
        _ = seq.append((object obj) =>
        { // fire event after tween
            var dict = obj as Dictionary<string, string>;
            Debug.Log("We are done now obj value:" + dict["hi"]);
        }, new Dictionary<string, string>() { { "hi", "sup" } });
	}
}
