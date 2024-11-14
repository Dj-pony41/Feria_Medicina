using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed = 5f; // Velocidad de movimiento

	private Camera mainCamera; // Cámara principal para calcular la posición del mouse

	void Start()
	{
		mainCamera = Camera.main; // Obtiene la cámara principal
	}

	void Update()
	{
		MovePlayer();
		RotateTowardsMouse();
	}

	void MovePlayer()
	{
		float moveX = Input.GetAxis("Horizontal");
		float moveZ = Input.GetAxis("Vertical");

		Vector3 move = new Vector3(moveX, 0, moveZ);
		move = mainCamera.transform.TransformDirection(move);
		move.y = 0; // Ignorar la dirección vertical

		transform.Translate(move.normalized * moveSpeed * Time.deltaTime, Space.World);
	}

	void RotateTowardsMouse()
	{
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			Vector3 targetPosition = hit.point;
			targetPosition.y = transform.position.y; // Mantener la rotación solo en el eje Y

			transform.LookAt(targetPosition);
		}
	}
}
