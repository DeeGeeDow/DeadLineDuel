using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainterSkill : MonoBehaviour, Skill
{
    [Header("References")]
    [SerializeField] PlayerShootController _myShooter;
    [SerializeField] GameEvent skillStarted;
    [SerializeField] GameEvent skillFinished;
    [SerializeField] Transform _inkPrefab;
    [Header("Skill Values")]
    [SerializeField] float _skillDuration;
    [SerializeField] float _skillShootInterval;
    [SerializeField] float _skillMaxRadius;
    [SerializeField] float _inkShootDelay;
    [SerializeField] float _inkShootInterval;

    List<Transform> _inks = new List<Transform>();
    Queue<Transform> _myInks = new Queue<Transform>();
    Vector3 brushPosition = Vector3.zero;

    const float BRUSH_LERP_CONST = 1.2f;
    const float BRUSH_SPEED = 4f;
    const float BRUSH_DIRECTION_LERP_CONST = 0.1f;

    public void onCast()
    {
        skillStarted.Raise(this, null);
        StartCoroutine(executeSkill());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(brushPosition + transform.position, 0.2f);
        Gizmos.color = Color.green;
        for (int i = 0; i < _inks.Count; i++)
        {
            if (_inks[i] != null)
            {
                Gizmos.DrawSphere(_inks[i].position, 0.1f);
            }
        }
        Gizmos.DrawWireSphere(transform.position, _skillMaxRadius);
    }

    IEnumerator executeSkill()
    {
        _inks.Clear();
        float maxRadiusSqr = _skillMaxRadius * _skillMaxRadius * 3f/4f * 3f/4f;
        Vector2 random = Random.insideUnitCircle * _skillMaxRadius;
        brushPosition = new Vector3(random.x, 0f, random.y);
        Vector2 brushDirection = Random.insideUnitCircle;
        float timePassed = 0f;
        float nextShoot = _skillShootInterval;
        while (timePassed < _skillDuration)
        {
            brushDirection = Vector2.Lerp(brushDirection + Random.insideUnitCircle, Vector2.zero, Time.deltaTime * BRUSH_DIRECTION_LERP_CONST);
            Vector2 brushVelocity = brushDirection.normalized;
            brushPosition += new Vector3(brushVelocity.x, 0f, brushVelocity.y) * Time.deltaTime * BRUSH_SPEED;
            if (brushPosition.sqrMagnitude > maxRadiusSqr)
            {
                brushPosition = Vector3.Lerp(brushPosition, Vector3.zero, Time.deltaTime * BRUSH_LERP_CONST);
            }

            timePassed += Time.deltaTime;
            nextShoot -= Time.deltaTime;
            if (nextShoot <= 0f)
            {
                nextShoot += _skillShootInterval;
                _inks.Add(tryInstantiate(brushPosition + transform.position));
            }
            yield return null;
        }

        if (_inkShootDelay > 0f)
            yield return new WaitForSeconds(_inkShootDelay);

        for (int i = 0; i < _inks.Count; i++)
        {
            _myShooter.ShootAt(_inks[i].position);
            _inks[i].gameObject.SetActive(false);
            if (_inkShootInterval > 0f)
                yield return new WaitForSeconds(_inkShootInterval);
        }

        skillFinished.Raise(this, true);
    }
    
    private Transform tryInstantiate(Vector3 position)
    {
        Transform newInk;
        if (_myInks.Count > 0)
        {
            newInk = _myInks.Dequeue();
            newInk.gameObject.SetActive(true);
            newInk.transform.position = position;
            return newInk;
        }
        newInk = Instantiate(_inkPrefab, position, Quaternion.identity);
        return newInk;
    }
}
