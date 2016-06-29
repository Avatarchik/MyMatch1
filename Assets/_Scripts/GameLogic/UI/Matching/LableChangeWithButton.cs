using UnityEngine;
using System.Collections;

public class LableChangeWithButton : UIButton
{

    public UILabel[] ulabel;

    int num;

    void Awake()
    {
        ulabel = this.GetComponentsInChildren<UILabel>();

    }
    protected override void OnPress(bool isPressed)
    {
        num++;
        if (num%2==0)
        {
            ulabel[0].enabled = true;
            ulabel[1].enabled = false;
        }

        if (isEnabled && UICamera.currentTouch != null)
        {
            if (!mInitDone) OnInit();

            if (tweenTarget != null)
            {
                if (isPressed)
                {
                    SetState(State.Pressed, false);

                    ulabel[1].enabled = true;
                    ulabel[0].enabled = false;
                }
                else if (UICamera.currentTouch.current == gameObject)
                {
                    if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
                    {
                        SetState(State.Hover, false);
                    }
                    else if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == gameObject)
                    {
                        SetState(State.Hover, false);
                    }
                    else {
                        SetState(State.Normal, false);

                        ulabel[0].enabled = true;
                        ulabel[1].enabled = false;
                    }
                }
                else {
                    SetState(State.Normal, false);

                    ulabel[0].enabled = true;
                    ulabel[1].enabled = false;
                }
            }
        }
    }

    protected override void OnDragOut()
    {
        if (isEnabled)
        {
            if (!mInitDone) OnInit();
            if (tweenTarget != null)
            {
                SetState(State.Normal, false);
                ulabel[0].enabled = true;
                ulabel[1].enabled = false;
            }

        }
    }

    protected override void OnDisable()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        if (mInitDone && mState != State.Normal)
        {
            SetState(State.Normal, true);
            ulabel[0].enabled = true;
            ulabel[1].enabled = false;

            if (tweenTarget != null)
            {
                TweenColor tc = tweenTarget.GetComponent<TweenColor>();

                if (tc != null)
                {
                    tc.value = mDefaultColor;
                    tc.enabled = false;
                }
            }
        }
    }

    protected override void OnDragOver()
    {
        if (isEnabled)
        {
            if (!mInitDone) OnInit();
            if (tweenTarget != null)
            {
                SetState(State.Pressed, false);
                ulabel[1].enabled = true;
                ulabel[0].enabled = false;
            }

        }
    }




    //void OnPress()
    //{
    //    num++;
    //    if (num%2==1)
    //    {
    //        ulabel[1].enabled = true;
    //        ulabel[0].enabled = false;
    //    }

    //}
    //void OnClick()
    //{
    //    ulabel[0].enabled = true;
    //    ulabel[1].enabled = false;
    //}
    //void OnDragStart()
    //{
    //    ulabel[0].enabled = true;
    //    ulabel[1].enabled = false;
    //}

}
