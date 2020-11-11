using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class area : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera cam;
    public List<Transform> areaList;
    private List<Transform> filter, render;
    void Start()
    {
        filter = new List<Transform>();
        render = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Physics.IgnoreLayerCollision(9,10);
        filter = new List<Transform>(areaList);

        //for (int i = 0; i < areaList.Count;i++) filter.Add(areaList[i]);

        for (int i = 0; i < cam.pixelHeight;)
        {
            for (int j = 0; j < cam.pixelWidth;)
            {
                Ray ray = cam.ScreenPointToRay(new Vector2(j,i));
                Debug.DrawRay(ray.origin, ray.direction * 40, Color.yellow);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("ChildCulling"))
                    {
                        Transform parent = hit.transform.parent;

                        /*for(int k = 0; k < parent.childCount; k++)
                        {
                            if(parent.GetChild(k).GetComponent<MeshRenderer>() != null)
                            {
                                if (parent.GetChild(0).GetComponent<MeshRenderer>().enabled) break;
                                parent.GetChild(k).GetComponent<MeshRenderer>().enabled = true;
                            }
                        }*/
                        //hit.transform.GetChild(0).gameObject.SetActive(true);
                        render.Add(parent);
                        filter.Remove(parent.transform.parent);
                    }
                }
                j += 10;
            }
            i += 10;
        }

        for(int i = 0; i < render.Count; i++)
        {
            for(int j = 0; j < render[i].childCount; j++)
            {
                if(render[i].GetChild(j).GetComponent<MeshRenderer>() != null)
                {
                    if (render[i].GetChild(render[i].childCount - 1).GetComponent<MeshRenderer>().enabled) break;
                    render[i].GetChild(j).GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }

        
        for(int i = 0; i < filter.Count; i++)
        {
            /*if (filter[i].GetComponent<Collider>().bounds.Contains(cam.transform.position))
            {
                filter[i].GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                filter[i].GetChild(0).gameObject.SetActive(false);
            }*/

            for(int j = 0; j < filter[i].GetChild(0).childCount; j++)
            {
                if(filter[i].GetChild(0).GetChild(j).GetComponent<MeshRenderer>() != null)
                {
                    filter[i].GetChild(0).GetChild(j).GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }

        filter.Clear();
        render.Clear();
    }
}
