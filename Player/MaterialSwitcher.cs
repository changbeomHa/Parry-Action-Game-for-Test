using UnityEngine;
using System.Collections.Generic;

public class MaterialSwitcher : MonoBehaviour
{
    public Material newMaterial; // 교체할 새로운 머티리얼
    private Dictionary<SkinnedMeshRenderer, Material[]> originalMaterials; // 원래의 머티리얼을 저장할 딕셔너리

    void Start()
    {
        // 초기화
        originalMaterials = new Dictionary<SkinnedMeshRenderer, Material[]>();
    }

    // Skinned Mesh Renderer의 모든 머티리얼을 새로운 머티리얼로 변경하는 함수
    public void ChangeMaterials()
    {
        // 모든 Skinned Mesh Renderer 컴포넌트를 찾음
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var renderer in skinnedMeshRenderers)
        {
            // 원래의 머티리얼을 저장
            if (!originalMaterials.ContainsKey(renderer))
            {
                originalMaterials[renderer] = renderer.materials;
            }

            // 새로운 머티리얼 배열 생성
            Material[] newMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = newMaterial;
            }

            // Skinned Mesh Renderer의 머티리얼을 변경
            renderer.materials = newMaterials;
        }
    }

    // Skinned Mesh Renderer의 머티리얼을 원래대로 복구하는 함수
    public void RestoreMaterials()
    {
        // 저장된 원래 머티리얼로 복구
        foreach (var entry in originalMaterials)
        {
            entry.Key.materials = entry.Value;
        }

        // 원래 머티리얼 정보 초기화
        originalMaterials.Clear();
    }
}
