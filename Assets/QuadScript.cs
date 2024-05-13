using UnityEngine;

public class QuadScript : MonoBehaviour
{
  Material mMaterial;
  MeshRenderer mMeshRenderer;

  float[] mPoints;
  public int mHitCount;

  void Start()
  {
    mMeshRenderer = GetComponent<MeshRenderer>();
    mMaterial = mMeshRenderer.material;

    mPoints = new float[32 * 3]; //32 points, each point has 3 values

  }

  public void GetRaycastHit(RaycastHit hit)
  {
    Debug.Log("Hit Texture coordinates = " + hit.textureCoord.x + "," + hit.textureCoord.y);
    addHitPoint(hit.textureCoord.x * 4 - 2, hit.textureCoord.y * 4 - 2);
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
