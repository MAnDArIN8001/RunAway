using UnityEngine;

public class ParalaxController : MonoBehaviour
{
    [SerializeField] private Paralx[] _paralaxes;

    private void HandleSpeedChages(float newSpeed)
    {
        foreach (var paralax in _paralaxes)
        {
            paralax.ScrollSpeed = newSpeed;
        }
    }
}
