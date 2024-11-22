using UnityEngine;

public class CustomeCharacter : MonoBehaviour
{
    // Control whether to set the layer of instantiated objects to "People"
    [Header("Layer Settings")]
    public bool setLayerToPeople = false;

    // Male
    [Header("Male Character 1")]
    public GameObject maleCharacterBody_1;
    [Space]
    public GameObject[] maleCharacter_1_Styles;

    [Space]
    [Header("Male Character 2")]
    public GameObject maleCharacterBody_2;
    [Space]
    public GameObject[] maleCharacter_2_Styles;

    [Header("Male Character Both Hats")]
    public GameObject[] maleCharacter_Hats;

    // Female
    [Space]
    [Header("Female Character 1")]
    public GameObject femaleCharacterBody_1;
    [Space]
    public GameObject[] femaleCharacter_1_Styles;

    [Space]
    [Header("Female Character 2")]
    public GameObject femaleCharacterBody_2;
    [Space]
    public GameObject[] femaleCharacter_2_Styles;

    [Header("Female Character Both Hats")]
    public GameObject[] femaleCharacter_Hats;

    private void Start()
    {
        GameObject body = null;
        Transform hatPivot = null;
        GameObject style = null;
        GameObject hat = null;

        if (Random.Range(0, 2) == 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                // Body
                body = Instantiate(maleCharacterBody_1, this.transform.position, this.transform.localRotation, this.transform);
                SetLayer(body);

                // Style
                style = Instantiate(maleCharacter_1_Styles[Random.Range(0, maleCharacter_1_Styles.Length)], this.transform.position, this.transform.localRotation, this.transform);
                SetLayer(style);

                // Hat
                hatPivot = body.transform.GetChild(0);
                hat = Instantiate(maleCharacter_Hats[Random.Range(0, maleCharacter_Hats.Length)], hatPivot.position, this.transform.localRotation, this.transform);
                SetLayer(hat);
            }
            else
            {
                // Body
                body = Instantiate(maleCharacterBody_2, this.transform.position, this.transform.localRotation, this.transform);
                SetLayer(body);

                // Style
                style = Instantiate(maleCharacter_2_Styles[Random.Range(0, maleCharacter_2_Styles.Length)], this.transform.position, this.transform.localRotation, this.transform);
                SetLayer(style);

                // Hat
                hatPivot = body.transform.GetChild(0);
                hat = Instantiate(maleCharacter_Hats[Random.Range(0, maleCharacter_Hats.Length)], hatPivot.position, this.transform.localRotation, this.transform);
                SetLayer(hat);
            }
        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                // Body
                body = Instantiate(femaleCharacterBody_1, this.transform.position, this.transform.localRotation, this.transform);
                SetLayer(body);

                // Style
                style = Instantiate(femaleCharacter_1_Styles[Random.Range(0, femaleCharacter_1_Styles.Length)], this.transform.position, this.transform.localRotation, this.transform);
                SetLayer(style);

                // Hat
                hatPivot = body.transform.GetChild(0);
                hat = Instantiate(femaleCharacter_Hats[Random.Range(0, femaleCharacter_Hats.Length)], hatPivot.position, this.transform.localRotation, this.transform);
                SetLayer(hat);
            }
            else
            {
                // Body
                body = Instantiate(femaleCharacterBody_2, this.transform.position, this.transform.localRotation, this.transform);
                SetLayer(body);

                // Style
                style = Instantiate(femaleCharacter_2_Styles[Random.Range(0, femaleCharacter_2_Styles.Length)], this.transform.position, this.transform.localRotation, this.transform);
                SetLayer(style);

                // Hat
                hatPivot = body.transform.GetChild(0);
                hat = Instantiate(femaleCharacter_Hats[Random.Range(0, femaleCharacter_Hats.Length)], hatPivot.position, this.transform.localRotation, this.transform);
                SetLayer(hat);
            }
        }

        // Optionally set the layer for the parent GameObject
        SetLayer(this.gameObject);

        // Remove this script from the GameObject
        Destroy(this);
    }

    private void SetLayer(GameObject obj)
    {
        if (setLayerToPeople)
        {
            int peopleLayer = LayerMask.NameToLayer("People");
            if (peopleLayer == -1)
            {
                Debug.LogError("Layer 'People' does not exist. Please add it to the Tags and Layers.");
                return;
            }
            SetLayerRecursively(obj, peopleLayer);
        }
    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
