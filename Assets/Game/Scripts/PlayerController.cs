using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
	private readonly string SCORE_TEXT = "Score: {0}";

	public int m_speed;

	public Text m_scoreText;
	public Text m_result;
	public Button m_restart;

	private int m_score = 0;
	private GameObject[] m_pickUpList;

	void Awake()
	{
		m_pickUpList = GameObject.FindGameObjectsWithTag("PickUp");
		Debug.Log ("m_pickUpList..." + m_pickUpList.Length);
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		GetComponent<Rigidbody>().AddForce(movement * m_speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider p_collider)
	{
		if(p_collider.gameObject.tag == "PickUp") 
		{
			p_collider.gameObject.SetActive(false);
			m_score += 1;
			SetScoreText();
			if (m_score == m_pickUpList.Length) 
			{
				m_result.gameObject.SetActive (true);
				m_restart.gameObject.SetActive(true);
			}
		}
	}

	public void OnRestart()
	{
		m_score = 0;
		SetScoreText();
		m_result.gameObject.SetActive (false);
		m_restart.gameObject.SetActive(false);
		transform.transform.position = new Vector3(0f, 0.5f, 0f);
		GetComponent<Rigidbody> ().Sleep ();

		for (int i = 0; i < m_pickUpList.Length; ++i) 
		{
			m_pickUpList[i].SetActive (true);
		}
	}

	private void SetScoreText()
	{
		m_scoreText.text = string.Format (SCORE_TEXT, m_score);
	}
}
