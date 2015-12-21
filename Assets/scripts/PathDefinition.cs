using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathDefinition : MonoBehaviour {
	public Transform[] Points;
    public bool isCameraPath = false;
    private bool loopPath = true;

	public IEnumerator<Transform> GetPathEnumerator(){
		if (Points == null || Points.Length < 1)
			yield break;

		var direction = 1;
		var index = 0;
		while (loopPath) {
            yield return Points[index];
			if(Points.Length == 1)
				continue;
            if (index <= 0)
                direction = 1;
            else if (index >= Points.Length - 1)
            {
                if (!isCameraPath)
                {
                    direction = -1;
                }
                else {
                    loopPath = false;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DFollow>().isPreviewing = false;
                }
            }
			index = index + direction;

		}
	}
}
