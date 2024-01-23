using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Transform t;

    [SerializeField] private float disappearTimer = 1f;
    [SerializeField] private float disappearSpeed = 3f;
    [SerializeField] private float floatingSpeed = 5f;
    private Color textColor;

    public static TextPopup Create(Vector3 position, string text)
    {
        TextPopup prefab = Resources.Load<TextPopup>("textPopup");

        TextPopup instance = Instantiate(prefab, position, Quaternion.identity);
        instance.Setup(text);
        return instance;
    }

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        t = transform;
        textColor = textMesh.color;
    }

    public void Setup(string text)
    {
        textMesh.SetText(text);
    }

    private void Update()
    {
        t.position += new Vector3(0, floatingSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
