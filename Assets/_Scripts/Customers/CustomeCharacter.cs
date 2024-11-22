using UnityEngine;

public class CustomeCharacter : MonoBehaviour
{
    //Male
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

    //Female

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

        if (Random.Range(0, 2) == 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                //Body
                body = Instantiate(maleCharacterBody_1, this.transform.position, this.transform.localRotation, this.transform);

                //Style
                Instantiate(maleCharacter_1_Styles[Random.Range(0, maleCharacter_1_Styles.Length)], this.transform.position, this.transform.localRotation, this.transform);

                //Hat
                hatPivot = body.transform.GetChild(0);
                Instantiate(maleCharacter_Hats[Random.Range(0, maleCharacter_Hats.Length)], hatPivot.position, this.transform.localRotation, this.transform);
            }
            else
            {
                //Body
                body = Instantiate(maleCharacterBody_2, this.transform.position, this.transform.localRotation, this.transform);

                //Style
                Instantiate(maleCharacter_2_Styles[Random.Range(0, maleCharacter_2_Styles.Length)], this.transform.position, this.transform.localRotation, this.transform);

                //Hat
                hatPivot = body.transform.GetChild(0);
                Instantiate(maleCharacter_Hats[Random.Range(0, maleCharacter_Hats.Length)], hatPivot.position, this.transform.localRotation, this.transform);
            }
        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                //Body
                body = Instantiate(femaleCharacterBody_1, this.transform.position, this.transform.localRotation, this.transform);

                //Style
                Instantiate(femaleCharacter_1_Styles[Random.Range(0, femaleCharacter_1_Styles.Length)], this.transform.position, this.transform.localRotation, this.transform);

                //Hat
                hatPivot = body.transform.GetChild(0);
                Instantiate(femaleCharacter_Hats[Random.Range(0, femaleCharacter_Hats.Length)], hatPivot.position, this.transform.localRotation, this.transform);
            }
            else
            {
                //Body
                body = Instantiate(femaleCharacterBody_2, this.transform.position, this.transform.localRotation, this.transform);

                //Style
                Instantiate(femaleCharacter_2_Styles[Random.Range(0, femaleCharacter_2_Styles.Length)], this.transform.position, this.transform.localRotation, this.transform);

                //Hat
                hatPivot = body.transform.GetChild(0);
                Instantiate(femaleCharacter_Hats[Random.Range(0, femaleCharacter_Hats.Length)], hatPivot.position, this.transform.localRotation, this.transform);
            }
        }




        // Remove this script from the GameObject
        Destroy(this);
    }

}
