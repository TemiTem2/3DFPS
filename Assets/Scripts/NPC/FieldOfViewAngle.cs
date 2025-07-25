using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{
    [SerializeField] private float viewAngle;// 시야각
    [SerializeField] private float viewDistance;// 시야거리
    [SerializeField] private LayerMask targetMask;// 타겟 레이어

    private Pig pig;


    private void Start()
    {
        pig = GetComponent<Pig>();
    }
    void Update()
    {
        View();
    }

    private Vector3 BoundaryAngle(float _angle)
    {
        _angle +=transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }
    private void View()
    {
        Vector3 _leftBoundary = BoundaryAngle(-viewAngle / 2);
        Vector3 _rightBoundary = BoundaryAngle(viewAngle / 2);

        Debug.DrawRay(transform.position+transform.up, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.red);

        Collider[] _targets = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < _targets.Length; i++)
        {
            Transform _targetTf = _targets[i].transform;
            if (_targetTf.name == "Player")
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if(_angle <= viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position+transform.up, _direction, out _hit, viewDistance))
                    {
                        if (_hit.transform.name == "Player")
                        {
                            Debug.Log("플레이어가 돼지 시야 안에 있습니다");
                            Debug.DrawRay(transform.position + transform.up, _direction, Color.green);
                            pig.Run(_hit.transform.position);
                        }
                    }
                }
            }
        }
    }
}
