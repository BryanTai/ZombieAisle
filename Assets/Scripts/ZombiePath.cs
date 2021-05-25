using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePath : MonoBehaviour
{
    private SimplePF2D.Path path;

    // Start is called before the first frame update
    void Start()
    {
        SimplePathFinding2D pf = GameObject.Find("Grid").GetComponent<SimplePathFinding2D>();
        path = new SimplePF2D.Path(pf);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouseworldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseworldpos.z = 0.0f;

            path.CreatePath(new Vector3(0.0f, 0.0f, 0.0f), mouseworldpos);
        }
    }
}
