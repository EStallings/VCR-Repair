using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
	{
		protected MeshRenderer[] highlightRenderers;
		protected MeshRenderer[] existingRenderers;
		protected GameObject highlightHolder;
		private static Material highlightMat;
		[Tooltip("An array of child gameObjects to not render a highlight for. Things like transparent parts, vfx, etc.")]
		public GameObject[] hideHighlight;

		void Start() {
			highlightMat = (Material)Resources.Load("Highlight", typeof(Material));
		}

		protected virtual bool ShouldIgnoreHighlight(Component component)
		{
			for (int ignoreIndex = 0; ignoreIndex < hideHighlight.Length; ignoreIndex++)
			{
				if (component.gameObject == hideHighlight[ignoreIndex])
					return true;
			}

			return false;
		}

		protected virtual void CreateHighlightRenderers()
		{
			highlightHolder = new GameObject("Highlighter");

			MeshFilter[] existingFilters = this.GetComponentsInChildren<MeshFilter>(true);
			existingRenderers = new MeshRenderer[existingFilters.Length];
			highlightRenderers = new MeshRenderer[existingFilters.Length];

			for (int filterIndex = 0; filterIndex < existingFilters.Length; filterIndex++)
			{
				MeshFilter existingFilter = existingFilters[filterIndex];
				MeshRenderer existingRenderer = existingFilter.GetComponent<MeshRenderer>();

				if (existingFilter == null || existingRenderer == null || ShouldIgnoreHighlight(existingFilter))
					continue;

				GameObject newFilterHolder = new GameObject("FilterHolder");
				newFilterHolder.transform.parent = highlightHolder.transform;
				MeshFilter newFilter = newFilterHolder.AddComponent<MeshFilter>();
				newFilter.sharedMesh = existingFilter.sharedMesh;
				MeshRenderer newRenderer = newFilterHolder.AddComponent<MeshRenderer>();

				Material[] materials = new Material[existingRenderer.sharedMaterials.Length];
				for (int materialIndex = 0; materialIndex < materials.Length; materialIndex++)
				{
					materials[materialIndex] = highlightMat;
				}
				newRenderer.sharedMaterials = materials;

				highlightRenderers[filterIndex] = newRenderer;
				existingRenderers[filterIndex] = existingRenderer;
			}
		}

		protected virtual void UpdateHighlightRenderers()
		{
			if (highlightHolder == null)
				return;

			for (int rendererIndex = 0; rendererIndex < highlightRenderers.Length; rendererIndex++)
			{
				MeshRenderer existingRenderer = existingRenderers[rendererIndex];
				MeshRenderer highlightRenderer = highlightRenderers[rendererIndex];

				if (existingRenderer != null && highlightRenderer != null)
				{
					highlightRenderer.transform.position = existingRenderer.transform.position;
					highlightRenderer.transform.rotation = existingRenderer.transform.rotation;
					highlightRenderer.transform.localScale = existingRenderer.transform.lossyScale;
					highlightRenderer.enabled = existingRenderer.enabled && existingRenderer.gameObject.activeInHierarchy;
				}
				else if (highlightRenderer != null)
					highlightRenderer.enabled = false;
			}
		}

		public void OnHoverBegin()
		{
			if (highlightHolder == null)
				CreateHighlightRenderers();
			UpdateHighlightRenderers();
		}

		public void OnHoverEnd()
		{
			Destroy(highlightHolder);
		}

		public void OnHoverStay()
		{
			UpdateHighlightRenderers();
		}
	}
