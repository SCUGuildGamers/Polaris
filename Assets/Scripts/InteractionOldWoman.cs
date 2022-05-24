using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionOldWoman : Interactable
{
    void Start(){
        Pair<string, string>[] Sentences = new Pair<string, string>[] {
            new Pair<string, string>("", "(This women appears to be crocheting a suspiciously net like object)"),
            new Pair<string, string>("Old Woman", "Eh-hee, hello there, kiddo. Would you like a sweet, sweetie?"),
            new Pair<string, string>("", "(She hands you a cookie before you get the chance to respond.)"),
            new Pair<string, string>("", "(Obtained the Bone-shaped Biscuit!)"),
            new Pair<string, string>("", "(Is this… a dog biscuit?)"),
            new Pair<string, string>("Ancient Edna", "Awww, aren’t you adorable? Go on and eat up, now. It’s store-bought with Grandma’s love.")};
        lines = new Dialogue(Sentences);
        Vector3 pos = transform.position;
        pos.y += 1.6f;
        indicatorplaceholder.transform.position = pos;
        indicatorplaceholder.gameObject.SetActive(false);
    }
}
