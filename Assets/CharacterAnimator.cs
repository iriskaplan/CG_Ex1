using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public TextAsset BVHFile; // The BVH file that defines the animation and skeleton
    public bool animate; // Indicates whether or not the animation should be running
    public bool interpolate; // Indicates whether or not frames should be interpolated
    [Range(0.01f, 2f)] public float animationSpeed = 1; // Controls the speed of the animation playback

    public BVHData data; // BVH data of the BVHFile will be loaded here
    public float t = 0; // Value used to interpolate the animation between frames
    public float[] currFrameData; // BVH channel data corresponding to the current keyframe
    public float[] nextFrameData; // BVH vhannel data corresponding to the next keyframe
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        BVHParser parser = new BVHParser();
        data = parser.Parse(BVHFile);
        CreateJoint(data.rootJoint, Vector3.zero);
        Matrix4x4 mat = RotateTowardsVector(new Vector3(2, 5, -8));
        Vector3 res = mat.MultiplyVector(Vector3.up);
        print(mat);
        print(res.x);
        print(res.y);
        print(res.z);
    }

    // Returns a Matrix4x4 representing a rotation aligning the up direction of an object with the given v
    public Matrix4x4 RotateTowardsVector(Vector3 v)
    {
        Vector3 v_dir = v.normalized;
        float theta_z = Mathf.Atan2(v_dir.x, v_dir.y);
        float theta_x = -1 * 
            (Mathf.Atan2(v_dir.z, Mathf.Sqrt(v_dir.y * v_dir.y + v_dir.x * v_dir.x)));

        float deg_z = -Mathf.Rad2Deg * theta_z;
        float deg_x = -Mathf.Rad2Deg * theta_x;
        Matrix4x4 r_z = MatrixUtils.RotateX(deg_z);
        Matrix4x4 r_x = MatrixUtils.RotateZ(deg_x);
        // Matrix4x4 r = r_x.MultiplyVector(v_dir);
        return r_x * r_z;
    }

    // Creates a Cylinder GameObject between two given points in 3D space
    public GameObject CreateCylinderBetweenPoints(Vector3 p1, Vector3 p2, float diameter)
    {
        // Your code here
        return null;
    }

    // Creates a GameObject representing a given BVHJoint and recursively creates GameObjects for it's child joints
    public GameObject CreateJoint(BVHJoint joint, Vector3 parentPosition)
    {
        joint.gameObject = new GameObject(joint.name);
        GameObject jointSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        jointSphere.transform.parent = joint.gameObject.transform;

        Matrix4x4 scaleJoint = MatrixUtils.Scale(new Vector3(2,2,2));
        Matrix4x4 scaleHead = MatrixUtils.Scale(new Vector3(8,8,8));
        if (joint.name != "Head")
        {
            MatrixUtils.ApplyTransform(jointSphere, scaleJoint);
        } else
        {
            MatrixUtils.ApplyTransform(jointSphere, scaleHead);
        }

        Vector3 jointPosition = parentPosition + joint.offset;
        Matrix4x4 translateJoint = MatrixUtils.Translate(jointPosition);
        MatrixUtils.ApplyTransform(joint.gameObject, translateJoint);
        if (!joint.isEndSite)
        {
            foreach (BVHJoint child in joint.children)
            {
                CreateJoint(child, jointPosition);
            }

        }
        return joint.gameObject;
    }

    // Transforms BVHJoint according to the keyframe channel data, and recursively transforms its children
    public void TransformJoint(BVHJoint joint, Matrix4x4 parentTransform)
    {
        // Your code here
    }

    // Returns the frame nunmber of the BVH animation at a given time
    public int GetFrameNumber(float time)
    {
        // Your code here
        return 0;
    }

    // Returns the proportion of time elapsed between the last frame and the next one, between 0 and 1
    public float GetFrameIntervalTime(float time)
    {
        // Your code here
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time * animationSpeed;
        if (animate)
        {
            int currFrame = GetFrameNumber(time);
            // Your code here
        }
    }
}
