using UnityEngine;

public class Paralx : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;

    [SerializeField] private Vector2 _scrollDirection;

    private Material _mainMaterial;

    public float ScrollSpeed 
    {
        set
        {
            _scrollSpeed = value;
        }
    }

    private void Awake()
    {
        _mainMaterial = GetComponent<SpriteRenderer>().material;
    }

    private void FixedUpdate()
    {
        _mainMaterial.mainTextureOffset += _scrollDirection * _scrollSpeed;
    }
}
