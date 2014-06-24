using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
  public float m_WalkSpeed;
  public float m_RunSpeed;
  public CharacterController m_CharController;
  public AnimationClip m_AniWalk;
  public AnimationClip m_AniIdle;
  public AnimationClip m_AniRun;
  private AnimationClip m_CurrentWalkAni;
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
    if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button4))
    {
      m_Speed = m_RunSpeed;  
      m_CurrentWalkAni = m_AniRun;
    }
    else
    {
      m_Speed = m_WalkSpeed;
      m_CurrentWalkAni = m_AniWalk;      
    }
       
    if(Input.GetMouseButton(0) || Input.GetKey(KeyCode.Joystick1Button0))
    {
      if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Joystick1Button5))
      {
        m_Walk = false;
        animation.Play(m_AniIdle.name);
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
    else
    {
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
    if(Vector3.Distance(transform.position, m_Position) > 1.1 )
    {
      m_CharController.SimpleMove(transform.forward * m_Speed);
      animation.Play(m_CurrentWalkAni.name);
    }
    else
    {
      animation.Play(m_AniIdle.name);
    }
  }

}
