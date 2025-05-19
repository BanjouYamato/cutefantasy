using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingleTon<EffectManager>
{
    Dictionary<GameObject, List<GameObject>> effectManager = new();
    public GameObject GetEffect(GameObject effect, Vector2 vector2)
    {
        if (effectManager.ContainsKey(effect))
        {   
            foreach (GameObject e in effectManager[effect])
            {
                if (!e.activeInHierarchy)
                {   
                    ParticleSystem particle = e.GetComponent<ParticleSystem>();
                    e.transform.position = vector2;
                    e.SetActive(true);
                    particle.Clear();
                    particle.Play();
                    return e;
                }
            }
        }
        effectManager[effect] = new();
        GameObject newEffect = Instantiate(effect);
        newEffect.transform.position = vector2;
        effectManager[effect].Add(newEffect);
        newEffect.transform.SetParent(transform);
        return newEffect;
    }
}
