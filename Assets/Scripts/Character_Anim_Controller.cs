
using UnityEngine;

public class Character_Anim_Controller : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateEmotion(Dialog_Message.Emotion emotion)
    {
        switch (emotion)
        {
            case Dialog_Message.Emotion.idle:
                animator.SetInteger("Emotion", 0);
                break;
            case Dialog_Message.Emotion.concerned:
                animator.SetInteger("Emotion", 1);
                break;
            case Dialog_Message.Emotion.laugh:
                animator.Play("Laughing");
                animator.SetInteger("Emotion", 0);
                break;
            case Dialog_Message.Emotion.confused:
                animator.SetInteger("Emotion", 2);
                break;
            case Dialog_Message.Emotion.smug:
                animator.SetInteger("Emotion", 3);
                break;
            case Dialog_Message.Emotion.sad:
                animator.SetInteger("Emotion", 4);
                break;
        }
    }
}
