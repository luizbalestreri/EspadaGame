using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Camera MainCamera; // camera principal
    Gyroscope m_Gyro; // Giroscópio
    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;//Variavel para a tela nao desligar, coloquei aqui porque ainda nao fiz um script de controle do jogo
        MainCamera = Camera.main; // camera principal
        m_Gyro = Input.gyro; //giroscópio
        m_Gyro.enabled = true;
    }
    

    // Update is called once per frame
    void Update()
    {   
        //Volta gradualmente para rotacao inicial
        //transform.rotation = Quaternion.Lerp(transform.rotation, MainCamera.transform.rotation, velocidade);
        transform.rotation = MainCamera.transform.rotation;
        
        MainCamera.transform.Rotate (0, 0, Input.gyro.rotationRateUnbiased.z);    } //rotacao da camera de acordo com o giroscopio
}
