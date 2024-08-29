//using UnityEngine;

//public class CameraSwitcher : MonoBehaviour
//{
//    public Camera mainCamera;
//    public Camera secondaryCamera;
//    public Animator animator;
//    public string PlayercamTowardsMonster; // Name of the animation state to check

//    void Start()
//    {
//        mainCamera.enabled = true;
//        secondaryCamera.enabled = false;
//    }

//    void Update()
//    {
//        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

//        if (stateInfo.IsName(PlayercamTowardsMonster))
//        {
//            SwitchToSecondaryCamera();
//        }
//        else
//        {
//            SwitchToMainCamera();
//        }
//    }

//    private void SwitchToSecondaryCamera()
//    {
//        mainCamera.enabled = false;
//        secondaryCamera.enabled = true;
//    }

//    private void SwitchToMainCamera()
//    {
//        mainCamera.enabled = true;
//        secondaryCamera.enabled = false;
//    }
//}
