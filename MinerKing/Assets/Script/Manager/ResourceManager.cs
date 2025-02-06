using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : SingletonLazy<ResourceManager>
{
    private Dictionary<string, Object> resources = new Dictionary<string, Object>();

    public T GetResource<T>(string path) where T : Object
    {
        if (resources.TryGetValue(path, out Object obj))
        {
            return obj as T; // 이미 로드된 경우 캐시 반환
        }

        // 새로 로드
        T resource = Resources.Load<T>(path);

        if (resource != null)
        {
            resources[path] = resource;
        }
        else
        {
            Debug.LogError($"[ResourceManager] '{path}' 경로의 스프라이트를 찾을 수 없습니다.");
        }
        return resource;
    }
}
