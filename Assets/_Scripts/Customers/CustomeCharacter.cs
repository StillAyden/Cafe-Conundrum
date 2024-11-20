using UnityEngine;

public class CustomeCharacter : MonoBehaviour
{
    [Header("Character 1")]
    public GameObject characterBody_1;
    [Space]
    public GameObject[] character_1_Styles;

    [Space]
    [Header("Character 2")]
    public GameObject characterBody_2;
    [Space]
    public GameObject[] character_2_Styles;

    [Header("Character Both Hats")]
    public GameObject[] character_Hats;
   
    private void Start()
    {
        GameObject body = null;
        Transform hatPivot = null;

        if (Random.Range(0,2) == 0)
        {
            //Body
            body = Instantiate(characterBody_1, this.transform.position, Quaternion.identity, this.transform);

            //Style
            Instantiate(character_1_Styles[Random.Range(0, character_1_Styles.Length)],this.transform.position,Quaternion.identity,this.transform);

            //Hat
            hatPivot = body.transform.GetChild(0);
            Instantiate(character_Hats[Random.Range(0, character_Hats.Length)], hatPivot.position, Quaternion.identity, this.transform);
        }
        else
        {
            //Body
            body = Instantiate(characterBody_2, this.transform.position, Quaternion.identity, this.transform);

            //Style
            Instantiate(character_2_Styles[Random.Range(0, character_2_Styles.Length)], this.transform.position, Quaternion.identity, this.transform);

            //Hat
            hatPivot = body.transform.GetChild(0);
            Instantiate(character_Hats[Random.Range(0, character_Hats.Length)], hatPivot.position, Quaternion.identity, this.transform);
        }


        // Remove this script from the GameObject
        Destroy(this);
    }

}
