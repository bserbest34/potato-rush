using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SI : MonoBehaviour
{
    private float lFFPX;
    private float mX;
    [SerializeField] private bool useRot;
    private CharacterController cC;
    [SerializeField] private bool useClampX;
    private Vector3 sA;
    public float _moveSpeed;
    [SerializeField] private float clampX;
    [SerializeField] private bool useGravity;
    private float gravity = 9.8f;
    public float spX;
    [SerializeField] private float spY;
    [SerializeField] private float spZ;
    private float verSp;

    private void Awake()
    {
        GameManager.Instance.finishControl = true;
        cC = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStarted)
        {
            cC.Move(transform.forward * Time.deltaTime * _moveSpeed);

            if (GameManager.Instance.finishControl) {
                SwerveHan();

                cC.Move(new Vector3(SwerveAou() * spX * Time.fixedDeltaTime,
                                            verSp * Time.deltaTime,
                                            spZ * Time.deltaTime));
                if (useRot) {
                    if (SwerveAou() != 0)
                        RotateWithFinger();
                    else
                        SetZeroRotate();
                }
                if (useClampX)
                    ClampPos();
                if (useGravity)
                    UseGra();
            }

        }
    }

    private void SwerveHan()
    {
        if (Input.GetMouseButtonDown(0))  {
            lFFPX = Input.mousePosition.x;
        } else if (Input.GetMouseButton(0)){
            mX = Input.mousePosition.x - lFFPX;
            lFFPX = Input.mousePosition.x;
        } else if (Input.GetMouseButtonUp(0))  {
            mX = 0f;
        }
        sA.x = mX;
    }
    public float SwerveAou()
    {
        return sA.x;
    }
    private void ClampPos()
    {
        var x = Mathf.Clamp(transform.position.x, -clampX, clampX);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    private void UseGra()
    {
        if (!cC.isGrounded)
            verSp -= gravity * Time.fixedDeltaTime;
        else
            verSp = spY;
    }

    private void RotateWithFinger()
    {
        var targetPos = new Vector3(SwerveAou(), transform.position.y, transform.position.z + 1);

        Quaternion rotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

    private void SetZeroRotate()
    {
        var targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

        Quaternion rotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

}