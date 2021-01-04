using UnityEngine;

public class ProfileController : MonoBehaviour
{
    private const string LinkedinProfileUrl = "https://www.linkedin.com/in/mahmoud-hamza96/";
    private const string GithubProfileUrl = "https://github.com/MahmoudmHamza/Unity3D-Game-Development-Projects";

    public void OnLinkedinButtonClicked()
    {
        Application.OpenURL(LinkedinProfileUrl);
    }

    public void OnGithubButtonClicked()
    {
        Application.OpenURL(GithubProfileUrl);
    }
}
