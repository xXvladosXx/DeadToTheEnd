using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Combat.SwordActivators
{
    public class SwordActivator : MonoBehaviour
    {
        [SerializeField] private List<Material> _materials;
        private readonly List<Material> _mainMaterials = new List<Material>();

        private Renderer _renderer;
        private bool _startCount;
        private float _dissolveModifier;
        private static readonly int Dissolve = Shader.PropertyToID("_Dissolve");
        
        protected bool _isActive;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _mainMaterials.AddRange(_renderer.materials);
            _mainMaterials.Add(_materials[1]);
            SetValue(1);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                StartCoroutine(Dissolver());
            }
        }

        private IEnumerator Dissolver()
        {
            if (_isActive)
            {
                _renderer.material = _materials[1];
            }

            while (true)
            {
                _dissolveModifier += Time.deltaTime;

                if (!_isActive)
                {
                    var value = Mathf.Lerp(1, 0, _dissolveModifier);
                    SetValue(value);
                }
                else
                {
                    var value = Mathf.Lerp(0, 1, _dissolveModifier);
                    SetValue(value);
                }

                if (_dissolveModifier > .7f)
                {
                    _isActive = !_isActive;
                    SetValue(Convert.ToSingle(!_isActive));
                    if (_isActive)
                    {
                        _renderer.material = _materials.First();
                    }

                    _dissolveModifier = 0;
                    yield break;
                }

                yield return null;
            }
        }

        protected void SetValue(float value)
        {
            foreach (var material in _mainMaterials)
            {
                material.SetFloat(Dissolve, value);
            }
        }
    }
}