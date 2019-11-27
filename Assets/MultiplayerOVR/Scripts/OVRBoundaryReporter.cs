using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRBoundaryReporter : MonoBehaviour
{
    private OVRBoundary oVRBoundary;

    [SerializeField]
    private Color boundaryColor;

    [SerializeField]
    public Vector3 boundaryMinLimit;


    [SerializeField]
    public Vector3 boundaryMaxLimit;

    // Start is called before the first frame update
    void Start()
    {
         this.oVRBoundary = new OVRBoundary();
       // OVRBoundary. = new OVRBoundary.BoundaryLookAndFeel();
        //lookAndFeel.Color = this.boundaryColor;
        
        //this.oVRBoundary.SetLookAndFeel(lookAndFeel);

        //int index = 0;
        /*float minX = 1000;
        float maxX = -1000;
        float minZ = 1000;
        float maxZ = -1000;
        foreach (Vector3 v in this.oVRBoundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary)) {
            if (v.x < minX) minX = v.x;
            if (v.x > maxX) maxX = v.x;
            if (v.z < minZ) minZ = v.z;
            if (v.z > maxZ) maxZ = v.z;
        }

        this.boundaryMinLimit = new Vector3(minX, 0, minZ);
        this.boundaryMaxLimit = new Vector3(maxX, 0, maxZ);

        Debug.Log("Outer Boundary Min" + this.boundaryMinLimit );
        Debug.Log("Outer Boundary Max" + this.boundaryMaxLimit);
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("Boundary Play Area" + this.oVRBoundary.GetDimensions(OVRBoundary.BoundaryType.PlayArea));
        Debug.Log("Outer Boundary" + this.oVRBoundary.GetDimensions(OVRBoundary.BoundaryType.OuterBoundary));
        Debug.Log("Geometry Play Area: " + this.oVRBoundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea));
        Debug.Log("Geometry Outer Boundary: " + this.oVRBoundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea));*/

        //Debug.Log("closest point: " + this.oVRBoundary.Get)

    }


}
