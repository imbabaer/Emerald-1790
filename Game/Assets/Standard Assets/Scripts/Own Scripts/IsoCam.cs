using UnityEngine;
using System.Collections;

public class IsoCam : MonoBehaviour {
  public Transform m_Target;
  // The distance in the x-z plane to the target
  public float m_Distance;
  // the height we want the camera to be above the target
  public float m_HeightDamping;
  public float m_RotationDamping;
  public float m_HeightFactor;
  public float m_DeltaZoom;
  public float m_RotationStep;

  private float m_MinDistance;
  private float m_MaxDistance;

  // Use this for initialization
  void Start () 
  {
    m_MinDistance = 2.0f;
    m_MaxDistance = 20.0f;
    m_HeightDamping = 2.0f;
    m_RotationDamping = 3.0f;
  }
  
  // Update is called once per frame
  void Update () 
  {
    if(Input.GetAxis("Mouse ScrollWheel") < 0) // back
    {
      Debug.Log("BACK");
      m_Distance += m_DeltaZoom;
    }
    else
    if(Input.GetAxis("Mouse ScrollWheel") > 0) // forward
    {
      Debug.Log("FORWARD");
      m_Distance -= m_DeltaZoom;
    }

    m_Distance = Mathf.Clamp(m_Distance, m_MinDistance, m_MaxDistance);

    Vector3 viewDir = transform.forward;

    viewDir *= (-m_Distance);
    
    Vector3 newPos = m_Target.position + viewDir;

    newPos.y = m_Target.position.y + m_Distance * m_HeightFactor;
    transform.position = newPos;

    if(Input.GetKey(KeyCode.LeftArrow))
    {
      transform.RotateAround(m_Target.position, Vector3.up, m_RotationStep * Time.deltaTime);
    }
    else if(Input.GetKey(KeyCode.RightArrow))
    {
      transform.RotateAround(m_Target.position, Vector3.up, -m_RotationStep * Time.deltaTime);
    }

  }
}
