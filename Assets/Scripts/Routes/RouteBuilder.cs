using UnityEngine;

public class RouteBuilder : MonoBehaviour {

    public GameObject LineRoute;
    public GameObject routeManager;
    public GameObject Console;

    private GameObject myRoute;

    public void initiateLine()
    {
        myRoute = Instantiate(LineRoute,transform);
    }
    public void DestroyLine() {
        LineRenderer lr = myRoute.GetComponent<LineRenderer>();
        RouteManager rm = routeManager.GetComponent<RouteManager>();
        if (rm.getTargetPlanet().Equals(this.gameObject) || rm.getTargetPlanet().name.Equals("Background"))
        {
            Destroy(myRoute);

        }
        else
        {
            if (rm.planetDict[this.gameObject.name].Contains(rm.getTargetPlanet()))
            {
                Destroy(myRoute);
                //Debug.Log(rm.getTargetPlanet().transform.position);
            }
            else
            {
                Console = GameObject.Find("console");
                rm.CreateRoute(this.gameObject, rm.getTargetPlanet());
                //PlanetData pd = rm.getTargetPlanet().GetComponent<PlanetData>();
                lr.SetPosition(1, transform.InverseTransformPoint(rm.getTargetPlanet().transform.position));
                myRoute.GetComponent<RouteUpdater>().initialize(rm.getTargetPlanet());
                //SetText st = Console.GetComponent<SetText>();
                //st.newText("new Route Built from "+this.name+" to "+ rm.getTargetPlanet().name);
            }

        }
        /*RaycastHit2D hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (hit = Physics2D.Raycast(ray.origin, new Vector2(0, 0)))
            Debug.Log(hit.collider.name);
            */
    
    }

    public void DragLine()
    {
        LineRenderer lr = myRoute.GetComponent<LineRenderer>();
        lr.startWidth = 5;
        lr.endWidth = 5;
        //Vector3 MousePos = Input.mousePosition;
        Vector3 MousePos = GetWorldPositionOnPlane(Input.mousePosition, 0);
        //MousePos.z = 0;
        MousePos = transform.InverseTransformPoint(MousePos);
        lr.SetPosition(1, MousePos);
        //lr.SetPosition(0, tmp);
    }
    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
