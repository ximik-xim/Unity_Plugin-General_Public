using System;
using UnityEngine;

/// <summary>
/// Запрещает обьекту вращаться дальше указанных углов
/// </summary>
public class BlockRotationTargetGM : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _targetRbTester;

    [SerializeField]
    public TypeSetAngel IsSetAngelX = TypeSetAngel.None;
    
    [SerializeField]
    public TypeSetAngel IsSetAngelY = TypeSetAngel.SetRot;
    
    [SerializeField]
    public TypeSetAngel IsSetAngelZ = TypeSetAngel.None;

    [SerializeField]
    public Vector3 MinAngle = new Vector3(0, 295, 0);

    [SerializeField]
    public Vector3 MaxAngle = new Vector3(0, 65, 0);
    
    private void FixedUpdate()
    {
        // Получаем текущий угол по Y (0...360)
        Vector3 currentRotation = _targetRbTester.rotation.eulerAngles;
        bool isChanged = false;
        
        float angleX = currentRotation.x;
        if (IsSetAngelX == TypeSetAngel.SetRot)
        {
            angleX = GetAngel(currentRotation.x, MinAngle.x, MaxAngle.x);
            isChanged = true;
        }
        
        float angleY = currentRotation.y; 
        if (IsSetAngelY == TypeSetAngel.SetRot)
        {
            angleY = GetAngel(currentRotation.y, MinAngle.y, MaxAngle.y);
            isChanged = true;
        }
        
        float angleZ = currentRotation.z; 
        if (IsSetAngelZ == TypeSetAngel.SetRot)
        {
            angleZ = GetAngel(currentRotation.z, MinAngle.z, MaxAngle.z);
            isChanged = true;
        }

        // Нам нужно проверить, находится ли угол в "запретной зоне" (от 70 до 280)
        if (isChanged == true) 
        {
            // Применяем исправленный поворот
            _targetRbTester.rotation = Quaternion.Euler(angleX, angleY, angleZ);
            
            // Важно: обнуляем угловую скорость, чтобы объект не пытался "пробить" стенку
            _targetRbTester.angularVelocity = Vector3.zero;
        }
    }

    private float GetAngel(float angle, float minAngle, float maxAngle)
    {
        // Нам нужно проверить, находится ли угол в "запретной зоне"
        if (angle > maxAngle && angle < minAngle)
        {
            // Определяем, к какому краю мы ближе, чтобы "примагнитить" к нему
            float distToMax = Mathf.Abs(angle - maxAngle);
            float distToMin = Mathf.Abs(angle - minAngle);

            if (distToMax < distToMin)
            {
                angle = maxAngle;
            }
            else
            {
                angle = minAngle;
            }
        }

        return angle;
    }
    
    public enum TypeSetAngel
    {
        None,
        SetRot,
    }
}
