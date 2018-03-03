using UnityEngine;

public class ThrowBall : MonoBehaviour
{
	public GameObject ball;
	public float forceMultiplier;
	public UnityEngine.UI.Text text;
	Camera _camera;

	void Start ()
	{
		_camera = GetComponent<Camera> ();
	}

	void Update ()
	{
		text.text = "Throw Power: " + forceMultiplier;

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			forceMultiplier += forceMultiplier / 100;
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			forceMultiplier -= forceMultiplier / 100;
		}

		if (Input.GetButtonDown ("Fire1")) {
			Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
			GameObject newBall = Instantiate (ball, ray.origin, Quaternion.Euler (Vector3.zero));
			newBall.GetComponent<Rigidbody> ().AddForce (ray.direction * forceMultiplier);
		}
	}
}
