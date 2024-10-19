using UnityEngine;
using System.Collections.Generic;

public class MaterialSwitcher : MonoBehaviour
{
    public Material newMaterial; // ��ü�� ���ο� ��Ƽ����
    private Dictionary<SkinnedMeshRenderer, Material[]> originalMaterials; // ������ ��Ƽ������ ������ ��ųʸ�

    void Start()
    {
        // �ʱ�ȭ
        originalMaterials = new Dictionary<SkinnedMeshRenderer, Material[]>();
    }

    // Skinned Mesh Renderer�� ��� ��Ƽ������ ���ο� ��Ƽ����� �����ϴ� �Լ�
    public void ChangeMaterials()
    {
        // ��� Skinned Mesh Renderer ������Ʈ�� ã��
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var renderer in skinnedMeshRenderers)
        {
            // ������ ��Ƽ������ ����
            if (!originalMaterials.ContainsKey(renderer))
            {
                originalMaterials[renderer] = renderer.materials;
            }

            // ���ο� ��Ƽ���� �迭 ����
            Material[] newMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = newMaterial;
            }

            // Skinned Mesh Renderer�� ��Ƽ������ ����
            renderer.materials = newMaterials;
        }
    }

    // Skinned Mesh Renderer�� ��Ƽ������ ������� �����ϴ� �Լ�
    public void RestoreMaterials()
    {
        // ����� ���� ��Ƽ����� ����
        foreach (var entry in originalMaterials)
        {
            entry.Key.materials = entry.Value;
        }

        // ���� ��Ƽ���� ���� �ʱ�ȭ
        originalMaterials.Clear();
    }
}
