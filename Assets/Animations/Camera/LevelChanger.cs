using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    public void FadeToLevel()
    {
        animator.SetTrigger("DoTransition");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        
    }
}
