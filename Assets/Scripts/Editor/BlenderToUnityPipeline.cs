using UnityEngine;
using UnityEditor;

public class BlenderToUnityPipeline
{
	static void ResetTransfrom (Transform t)
	{
		t.transform.position = Vector3.zero;
//		t.transform.rotation = Quaternion.Euler (new Vector3 (180, 0, 0));
		t.transform.localScale = Vector3.one;
	}

	static void AddMeshColliderAndRigidbody (Transform t)
	{
		if (t.gameObject.GetComponent<MeshCollider> () == null) {
			t.gameObject.AddComponent<MeshCollider> ();
		}
		if (t.gameObject.GetComponent<Rigidbody> () == null) {
			t.gameObject.AddComponent<Rigidbody> ();
		}
	}

	static void AddDestructionScript (Transform t)
	{
		if (t.gameObject.GetComponent<Destruction> () == null) {
			t.gameObject.AddComponent<Destruction> ();
		}
	}

	static void SetDestructionValues (Transform t)
	{
		Destruction destruction = t.gameObject.GetComponent<Destruction> ();
		destruction.startBroken = false;
		destruction.breakOnCollision = true;
	}

	static void SetKinematicAndConvex (Transform t)
	{
		t.gameObject.GetComponent<MeshCollider> ().convex = true;
		t.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
	}

	[MenuItem ("Tools/BlenderToUnityWithMesh")]
	static void NewMenuOption ()
	{
		Transform selected = Selection.activeTransform;
		ResetTransfrom (selected);
		Transform[] allChildren = selected.GetComponentsInChildren<Transform> ();

		foreach (Transform child in allChildren) {
			child.transform.localScale = Vector3.one;
			AddMeshColliderAndRigidbody (child);
			SetKinematicAndConvex (child);
		}

		selected.rotation = Quaternion.Euler (new Vector3 (90, 0, 0));
		foreach (Transform child in allChildren) {
			child.SetParent (selected.parent);
		}
		selected.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		foreach (Transform child in allChildren) {
			child.SetParent (selected);
		}
		selected.rotation = Quaternion.Euler (new Vector3 (90, 0, 0));

		AddDestructionScript (selected);
		SetDestructionValues (selected);

		AddMeshColliderAndRigidbody (selected);
		SetKinematicAndConvex (selected);
		Debug.LogWarning ("Add the mesh on the main object manually");
	}
}