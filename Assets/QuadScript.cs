using UnityEngine;

public class QuadScript : MonoBehaviour
{
  Material mMaterial;
  MeshRenderer mMeshRenderer;

  float[] mPoints;
  int mHitCount;

  float mDelay;

  void Start()
  {
    mDelay = 3;

    mMeshRenderer = GetComponent<MeshRenderer>();
    mMaterial = mMeshRenderer.material;

    mPoints = new float[32 * 3]; //32 points, each point has 3 values

  }

  void Update()
  {
    mDelay -= Time.deltaTime;
    if (mDelay <=0)
    {
      GameObject go = Instantiate(Resources.Load<GameObject>("Projectile")); //create a new projectile
      go.transform.position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-3f, -1f));

      mDelay = 0.5f;
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    foreach(ContactPoint cp in collision.contacts)
    {
      Debug.Log("Contact with object " + cp.otherCollider.gameObject.name);

      Vector3 StartOfRay = cp.point - cp.normal; //start the ray a little before the point of contact
      Vector3 RayDir = cp.normal; //point the ray in the direction of the normal

      Ray ray = new Ray(StartOfRay, RayDir);
      RaycastHit hit;

      bool hitit = Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("HeatMapLayer"));

      if (hitit)
      {
        Debug.Log("Hit Object " + hit.collider.gameObject.name);
        Debug.Log("Hit Texture coordinates = " + hit.textureCoord.x + "," + hit.textureCoord.y);
        addHitPoint(hit.textureCoord.x*4-2, hit.textureCoord.y*4-2); 
      }
      Destroy(cp.otherCollider.gameObject);
    }
  }

  public void addHitPoint(float xp,float yp)
  {
    mPoints[mHitCount * 3] = xp; //set the x value of the hit point
    mPoints[mHitCount * 3 + 1] = yp; //set the y value of the hit point
    mPoints[mHitCount * 3 + 2] = 3; //set the intensity value of the hit point

    mHitCount++;
    mHitCount %= 32;

    mMaterial.SetFloatArray("_Hits", mPoints); //send the array to the shader
    mMaterial.SetInt("_HitCount", mHitCount); //send the hit count to the shader
  }
}
