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
  public float m_RotationSpeed;

  private float m_MinDistance;
  private float m_MaxDistance;

  // Use this for initialization
  void Start () 
  {
    m_MinDistance = 2.0f;
    m_MaxDistance = 20.0f;
    m_HeightDamping = 2.0f;
    m_RotationDamping = 3.0f;

    if(0 == m_RotationSpeed)
    {
      m_RotationSpeed = 200.0f;
    }
  }
  
  // Update is called once per frame
  void Update () 
  {
    //zoom in and out with mousewheel
    if(Input.GetAxis("Mouse ScrollWheel") < 0) // back
    {
      m_Distance += m_DeltaZoom;
    }
    else
    if(Input.GetAxis("Mouse ScrollWheel") > 0) // forward
    {
      m_Distance -= m_DeltaZoom;
    }

    //rotate with pressed mousewheel
    if (Input.GetMouseButton(2))
    {
      transform.RotateAround(m_Target.position, Vector3.up, Input.GetAxis("Mouse X")*m_RotationSpeed * Time.deltaTime);
    }
    //clamp distance if out of the borders
    m_Distance = Mathf.Clamp(m_Distance, m_MinDistance, m_MaxDistance);
    //get view direction of the camera
    Vector3 viewDir = transform.forward;
    viewDir *= (-m_Distance);
    //calculate new position
    Vector3 newPos = m_Target.position + viewDir;
    newPos.y = m_Target.position.y + m_Distance * m_HeightFactor;
    //set new position
    transform.position = newPos;
  }
}
