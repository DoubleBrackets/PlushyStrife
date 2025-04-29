using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField]
    private TMP_Text rankingText;

    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text weaponText;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private Image uiImage;

    public void Initialize(int ranking, string userName, string weaponName, int score, Texture2D weaponImage)
    {
        rankingText.text = ranking.ToString();
        nameText.text = userName;
        weaponText.text = weaponName;
        scoreText.text = "" + score;
        uiImage.sprite = Sprite.Create(weaponImage, new Rect(0, 0, weaponImage.width, weaponImage.height),
            new Vector2(0.5f, 0.5f));
    }
}