using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

namespace MyFrameWork
{
    public class GameUtility
    {

        /// <summary>
        /// �����ӽڵ�ű�
        /// </summary>
        public static T FindDeepChild<T>(GameObject _target, string _childName) where T : Component
        {
            Transform resultTrs = GameUtility.FindDeepChild(_target, _childName);
            if (resultTrs != null)
                return resultTrs.gameObject.GetComponent<T>();
            return (T)((object)null);
        }

        /// <summary>
        /// �����ӽڵ�
        /// </summary>
        public static Transform FindDeepChild(GameObject _target, string _childName)
        {
            Transform resultTrs = null;
            resultTrs = _target.transform.Find(_childName);
            if (resultTrs == null)
            {
                foreach (Transform trs in _target.transform)
                {
                    resultTrs = GameUtility.FindDeepChild(trs.gameObject, _childName);
                    if (resultTrs != null)
                        return resultTrs;
                }
            }
            return resultTrs;
        }

        /// <summary>
        /// ����ӽڵ�
        /// </summary>
        public static void AddChildToTarget(Transform target, Transform child)
        {
            child.parent = target;
            child.localScale = Vector3.one;
            child.localPosition = Vector3.zero;
            child.localEulerAngles = Vector3.zero;

            ChangeChildLayer(child, target.gameObject.layer);
        }

        /// <summary>
        /// �޸��ӽڵ�Layer  NGUITools.SetLayer();
        /// </summary>
        public static void ChangeChildLayer(Transform t, int layer)
        {
            t.gameObject.layer = layer;
            for (int i = 0; i < t.childCount; ++i)
            {
                Transform child = t.GetChild(i);
                child.gameObject.layer = layer;
                ChangeChildLayer(child, layer);
            }
        }

        /// <summary>
        /// ������Сdepth����Ŀ������Panel��ȣ���С����
        /// </summary>
        /// 
        private class CompareSubPanels : IComparer<UIPanel>
        {
            public int Compare(UIPanel left, UIPanel right)
            {
                return left.depth - right.depth;
            }
        }

        private static List<UIPanel> GetPanelSorted(GameObject target, bool includeInactive = false)
        {
            UIPanel[] panels = target.transform.GetComponentsInChildren<UIPanel>(includeInactive);
            if (panels.Length > 0)
            {
                List<UIPanel> lsPanels = new List<UIPanel>(panels);
                lsPanels.Sort(new CompareSubPanels());
                return lsPanels;
            }
            return null;
        }

        /// <summary>
        /// ������������СDepth����
        /// </summary>
        public static GameObject GetPanelDepthMaxMin(GameObject target, bool maxDepth, bool includeInactive)
        {
            List<UIPanel> lsPanels = GetPanelSorted(target, includeInactive);
            if (lsPanels != null)
            {
                if (maxDepth)
                    return lsPanels[lsPanels.Count - 1].gameObject;
                else
                    return lsPanels[0].gameObject;
            }
            return null;
        }

        /// <summary>
        /// ��Ŀ�����Collider����
        /// </summary>
        public static void AddColliderBgToTarget(GameObject target)
        {
            // ���UIPaneldepth��С����
            // ��֤��ӵ�Collider��������Ļ�м�
            Transform windowBg = GameUtility.FindDeepChild(target, "WindowBg");
            if (windowBg == null)
            {
                GameObject targetParent = GetPanelDepthMaxMin(target, false, true);
                if (targetParent == null)
                    targetParent = target;

                windowBg = (GameObject.Instantiate(Resources.Load("Test/UI/WindowBg")) as GameObject).transform;
                AddChildToTarget(targetParent.transform, windowBg);
            }

            Transform bg = GameUtility.FindDeepChild(target, "Mask");
            if (bg != null)
            {
                // add sprite or widget to ColliderBg gameobject
                UIWidget widget = bg.GetComponent<UIWidget>();

                // fill the screen
                UIStretch stretch = bg.gameObject.AddComponent<UIStretch>();
                stretch.style = UIStretch.Style.Both;
                // set relative size bigger
                stretch.relativeSize = new Vector2(1.5f, 1.5f);

                // set a lower depth
                widget.depth = -5;

                // set alpha
                widget.alpha = 0.6f;

                // add collider
                NGUITools.AddWidgetCollider(bg.gameObject);
            }
        }

    }
}


