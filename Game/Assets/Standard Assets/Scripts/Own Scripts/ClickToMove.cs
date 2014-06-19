using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
  public float m_WalkSpeed;
  public float m_RunSpeed;
  public CharacterController m_CharController;
  private float m_Speed;
  private Vector3 m_Position;
  private bool m_Walk;


  // Use this for initialization
  void Start()
  {
    m_Position = transform.position;
    m_Walk = true;
  }
  
  // Update is called once per frame
  void Update()
  { 
    if(Input.GetKey(KeyCode.LeftShift))
    {
      m_Speed = m_RunSpeed;      
    }
    else
    {
      m_Speed = m_WalkSpeed;
    }
       
    if(Input.GetMouseButton(0))
    {
      if(Input.GetKey(KeyCode.LeftControl))
      {
        m_Walk = false;
      }
      else
      {
        m_Walk = true;
      }
      // locate where the player clicked on the terrain
      locatePosition();
    }

    lookToPosition();
    // move player to position
    if(m_Walk)
    {
      moveToPosition();
    }
  }

  void locatePosition()
  {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if(Physics.Raycast(ray, out hit, 1000))
    {
      m_Position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
      //Debug.Log(m_Position);
    }
  }

  void lookToPosition()
  {
    Quaternion newRotation = Quaternion.LookRotation(m_Position - transform.position);
    newRotation.x = 0f;
    newRotation.z = 0f;
    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime + 3);

  }

  void moveToPosition()
  {
    if(Vector3.Distance(transform.position, m_Position) > 1)
    {
      m_CharController.SimpleMove(transform.forward * m_Speed);
    }
  }

}
