using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

  public float m_Speed;
  public float m_AttackRange;
  public float m_AggroRange;
  public CharacterController m_Controller;
  public Transform m_Player;

	// Use this for initialization
	void Start ()
  {
	
	}
	
	// Update is called once per frame
	void Update () 
  {
	  if(inRange(m_AggroRange))
    {
      chase();
    }
	}

  bool inRange(float range)
  {
    float dist = Vector3.Distance(transform.position, m_Player.position);
    return (dist < range && dist > 2);
  }

  void chase()
  {
    transform.LookAt(m_Player.position);
    m_Controller.SimpleMove(transform.forward * m_Speed);
  }
}
